namespace Zwedze.CivilizationHelper.DistrictManagement;

internal sealed class DistrictPlacement(DistrictKey district)
{
    public DistrictKey District { get; } = district;
    public bool IsCompleted { get; private set; }

    public void MarkCompleted()
    {
        IsCompleted = true;
    }
}
