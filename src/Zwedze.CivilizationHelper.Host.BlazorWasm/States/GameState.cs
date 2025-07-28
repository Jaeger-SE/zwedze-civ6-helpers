using System.Collections.Immutable;
using Zwedze.CivilizationHelper.DistrictManagement;

namespace Zwedze.CivilizationHelper.Host.BlazorWasm.States;

public record GameState
{
    public static readonly GameState Init = new()
    {
        Districts = ImmutableArray<DistrictState>.Empty,
        OpenedDistrictKey = null
    };

    public required ImmutableArray<DistrictState> Districts { get; init; }

    public required DistrictKey? OpenedDistrictKey { get; init; }
}
