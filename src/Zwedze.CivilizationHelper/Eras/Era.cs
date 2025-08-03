namespace Zwedze.CivilizationHelper.Eras;

internal sealed class Era(int key, string name)
{
    public EraKey Key { get; } = (EraKey)key;
    public string Name { get; } = name;
}
