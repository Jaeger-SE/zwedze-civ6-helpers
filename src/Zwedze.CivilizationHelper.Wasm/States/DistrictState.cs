using Zwedze.CivilizationHelper.DistrictManagement;

namespace Zwedze.CivilizationHelper.Wasm.States;

public record DistrictState
{
    public static DistrictState Init = new DistrictState
    {
        Key = new DistrictKey("null"),
        CompletedCount = 0,
        IsDiscounted = false,
        IsUnlocked = false,
        PlacedCount = 0,
        CanRemove = false,
        CanUnplace = false
    };

    public required DistrictKey Key { get; init; }
    public required int PlacedCount { get; init; }
    public required int CompletedCount { get; init; }
    public required bool IsDiscounted { get; init; }
    public required bool IsUnlocked { get; init; }
    public required bool CanRemove { get; init; }
    public required bool CanUnplace { get; init; }
}
