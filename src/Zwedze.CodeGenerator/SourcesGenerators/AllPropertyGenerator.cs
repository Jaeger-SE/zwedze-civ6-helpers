using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Zwedze.CodeGenerator.SourcesGenerators;

[Generator(LanguageNames.CSharp)]
public class AllPropertyGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var contractClassesProvider = context.SyntaxProvider.CreateSyntaxProvider(
            (node, _) =>
            {
                var classDeclaration = node as ClassDeclarationSyntax;
                return classDeclaration is not null && classDeclaration.AttributeLists.Any(x => x.Attributes.Any(a => a.Name.ToString() == "GenerateAllProperty"));
            },
            (ctx, _) => (ClassDeclarationSyntax)ctx.Node);

        var compilationAndClass = context.CompilationProvider.Combine(contractClassesProvider.Collect());

        context.RegisterSourceOutput(
            compilationAndClass,
            (sourceProductionContext, contractClass) => Execute(contractClass.Right, contractClass.Left, sourceProductionContext)
        );
    }

    private static void Execute(ImmutableArray<ClassDeclarationSyntax> classDeclarationSyntaxes, Compilation compilation, SourceProductionContext context)
    {
        foreach (var classDeclaration in classDeclarationSyntaxes)
        {
            var semanticModel = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
            GeneratePropertiesFile(context, semanticModel, classDeclaration);
        }
    }

    private static void GeneratePropertiesFile(SourceProductionContext context, SemanticModel semanticModel, ClassDeclarationSyntax classDeclarationSyntax)
    {
        var fieldDeclarations = classDeclarationSyntax.Members
            .OfType<FieldDeclarationSyntax>();

        var variableNames = new Dictionary<ITypeSymbol, List<string>>();
        foreach (var fieldDeclaration in fieldDeclarations)
        {
            var variable = fieldDeclaration.Declaration.Variables.Single();
            if (semanticModel.GetDeclaredSymbol(variable) is not IFieldSymbol symbol)
            {
                continue;
            }

            if (variable.Parent is not VariableDeclarationSyntax parent)
            {
                continue;
            }

            var parentType = semanticModel.GetTypeInfo(parent.Type).Type;
            if (parentType is null)
            {
                continue;
            }
            if (variableNames.TryGetValue(parentType, out var tagVariableNames))
            {
                tagVariableNames.Add(symbol.Name);
            }
            else
            {
                variableNames.Add(parentType, [symbol.Name]);
            }
        }

        foreach (var variableName in variableNames)
        {
            // Create the source code builder using a string builder
            var classSourceBuilder = new StringBuilder();

            // Add usings
            foreach (var usingStatement in classDeclarationSyntax.SyntaxTree.GetCompilationUnitRoot().Usings)
            {
                classSourceBuilder.AppendLine(usingStatement.ToString());
            }
            classSourceBuilder.AppendLine();

            // Add namespace
            BaseNamespaceDeclarationSyntax? classNamespace = classDeclarationSyntax.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
            classNamespace ??= classDeclarationSyntax.Ancestors().OfType<FileScopedNamespaceDeclarationSyntax>().FirstOrDefault();
            if (classNamespace is null)
            {
                return;
            }
            classSourceBuilder.AppendLine($"namespace {classNamespace.Name};");

            // Add partial class and AllFormats property
            classSourceBuilder
                .AppendLine($"{classDeclarationSyntax.Modifiers} {classDeclarationSyntax.Keyword} {classDeclarationSyntax.Identifier}")
                .AppendLine("{");
            classSourceBuilder.Append(GenerateAllProperty(variableName.Key, variableName.Value));
            classSourceBuilder.AppendLine("}");

            // Adding the source into a new file
            context.AddSource($"{classDeclarationSyntax.Identifier}_AllProperty_{variableName.Key}.Generated.cs", classSourceBuilder.ToString());
        }
    }

    private static StringBuilder GenerateAllProperty(ITypeSymbol typeSymbol, IEnumerable<string> tagVariableNames)
    {
        var typeName = typeSymbol.Name;
        var typeKind = typeSymbol.TypeKind;
        if (typeKind == TypeKind.Interface && typeName.StartsWith("i", StringComparison.OrdinalIgnoreCase))
        {
            typeName = typeName.Substring(1);
        }

        var type = typeSymbol.Name;
        var tagVariableNamesJoined = string.Join(", ", tagVariableNames);
        return new StringBuilder()
            .AppendLine($"    public static {type}[] AllKnown{typeName}s => new {type}[]")
            .AppendLine("    {")
            .AppendLine($"        {tagVariableNamesJoined}")
            .AppendLine("        ")
            .AppendLine("    };");
    }
}
