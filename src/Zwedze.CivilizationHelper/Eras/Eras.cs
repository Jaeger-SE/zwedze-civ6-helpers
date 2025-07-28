using System.Collections.Immutable;
using Zwedze.CivilizationHelper.DistrictManagement;

namespace Zwedze.CivilizationHelper.Eras;

internal sealed class Era
{
    public Era(int key, string name)
    {
        Key = (EraKey)key;
        Name = name;
    }

    public EraKey Key { get; }
    public string Name { get; }
}

internal static class Eras
{
    public static readonly Era Ancient = new(1, "Ancient");
    public static readonly Era Classical = new(2, "Classical");
    public static readonly Era Medieval = new(3, "Medieval");
    public static readonly Era Renaissance = new(4, "Renaissance");
    public static readonly Era Industrial = new(5, "Industrial");
    public static readonly Era Modern = new(6, "Modern");
    public static readonly Era Atomic = new(7, "Atomic");
    public static readonly Era Information = new(8, "Information");
    public static readonly Era Future = new(9, "Future");
}

public static class DistrictEra
{
    public static IDictionary<EraKey, ImmutableArray<DistrictKey>> List
    {
        get
        {
            return new Dictionary<EraKey, ImmutableArray<DistrictKey>>
                {
                    { Eras.Ancient.Key, [DistrictKeys.Campus, DistrictKeys.HolySite, DistrictKeys.Encampment, DistrictKeys.Preserve, DistrictKeys.GovernmentPlaza] },
                    { Eras.Classical.Key, [DistrictKeys.TheaterSquare, DistrictKeys.CommercialHub, DistrictKeys.Harbor, DistrictKeys.EntertainmentComplex, DistrictKeys.DiplomaticQuarter] },
                    { Eras.Medieval.Key, [DistrictKeys.IndustrialZone] },
                    { Eras.Industrial.Key, [DistrictKeys.WaterPark] },
                    { Eras.Modern.Key, [DistrictKeys.Aerodrome] }
                };
        }
    }
}
