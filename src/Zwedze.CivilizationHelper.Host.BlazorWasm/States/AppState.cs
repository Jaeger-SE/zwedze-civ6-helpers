namespace Zwedze.CivilizationHelper.Host.BlazorWasm.States;

public record AppState
{
    public static readonly AppState Init = new()
    {
        Game = GameState.Init
    };

    public required GameState Game { get; init; }
}
