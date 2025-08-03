namespace Zwedze.CivilizationHelper.DistrictManagement;

/// <summary>
///     These districts are considered "speciality" districts in Civ VI.
/// </summary>
/// <remarks>
///     These districts are mainly used for bo-discount calculations.
/// </remarks>
public static class SpecialityDistricts
{
    public static readonly DistrictKey Campus = new("campus");
    public static readonly DistrictKey TheaterSquare = new("theatre-square");
    public static readonly DistrictKey HolySite = new("holy-site");
    public static readonly DistrictKey Encampment = new("encampment");
    public static readonly DistrictKey CommercialHub = new("commercial-hub");
    public static readonly DistrictKey Harbor = new("harbor");
    public static readonly DistrictKey IndustrialZone = new("industrial-zone");
    public static readonly DistrictKey Preserve = new("preserve");
    public static readonly DistrictKey EntertainmentComplex = new("entertainment-complex");
    public static readonly DistrictKey WaterPark = new("water-park");
    public static readonly DistrictKey Aerodrome = new("aerodrome");
    public static readonly DistrictKey GovernmentPlaza = new("government-plaza");
    public static readonly DistrictKey DiplomaticQuarter = new("diplomatic-quarter");
}
