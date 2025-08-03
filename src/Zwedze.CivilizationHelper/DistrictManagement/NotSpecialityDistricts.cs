namespace Zwedze.CivilizationHelper.DistrictManagement;

/// <summary>
///     These districts are not considered "speciality" districts in Civ VI.
/// </summary>
/// <remark>
///     These districts are intentionally excluded from the main logic. They are listed here to make it clear that their
///     omission is deliberate, not accidental.
/// </remark>
public static class NotSpecialityDistricts
{
    public static readonly DistrictKey CityCenter = new("city-center");
    public static readonly DistrictKey Aqueduct = new("aqueduct");
    public static readonly DistrictKey Neighborhood = new("neighborhood");
    public static readonly DistrictKey Canal = new("canal");
    public static readonly DistrictKey Dam = new("dam");
    public static readonly DistrictKey Spaceport = new("spaceport");
}
