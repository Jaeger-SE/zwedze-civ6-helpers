using NSubstitute;
using Zwedze.CivilizationHelper.DistrictManagement;
using Zwedze.CivilizationHelper.DistrictManagement.Discount;

namespace Zwedze.CivilizationHelper.UnitTest;

internal class DistrictDiscountTest
{
    private DistrictDiscount _dut;
    private IPlayerDistricts _playerDistricts;
    private IUnlockedDistricts _unlockedDistricts;

    [SetUp]
    public void Setup()
    {
        _playerDistricts = Substitute.For<IPlayerDistricts>();
        _unlockedDistricts = Substitute.For<IUnlockedDistricts>();
        _dut = new DistrictDiscount(_playerDistricts, _unlockedDistricts);
    }

    [Test]
    public void Unlocked4_2HolySite_2Campus_1GovernmentPlaza_WantToPlaceCommercialHub()
    {
        // Arrange
        _unlockedDistricts.Count.Returns(4);
        _playerDistricts.GetTotalCompletedDistrictsCount().Returns(5);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.CommercialHub).Returns(0);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.HolySite).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.Campus).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.GovernmentPlaza).Returns(1);
        // Act
        var hasDiscount = _dut.HasDiscountFor(SpecialityDistricts.CommercialHub);
        // Assert
        Assert.That(hasDiscount, Is.True);
    }

    [Test]
    public void Unlocked4_2HolySite_2Campus_1GovernmentPlaza_1CommercialHub_WantToPlaceCommercialHub()
    {
        // Arrange
        _unlockedDistricts.Count.Returns(4);
        _playerDistricts.GetTotalCompletedDistrictsCount().Returns(6);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.CommercialHub).Returns(1);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.HolySite).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.Campus).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.GovernmentPlaza).Returns(1);
        // Act
        var hasDiscount = _dut.HasDiscountFor(SpecialityDistricts.CommercialHub);
        // Assert
        Assert.That(hasDiscount, Is.True);
    }

    [Test]
    public void Unlocked4_2HolySite_2Campus_1GovernmentPlaza_2CommercialHub_WantToPlaceCommercialHub()
    {
        // Arrange
        _unlockedDistricts.Count.Returns(4);
        _playerDistricts.GetTotalCompletedDistrictsCount().Returns(7);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.CommercialHub).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.HolySite).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.Campus).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.GovernmentPlaza).Returns(1);
        // Act
        var hasDiscount = _dut.HasDiscountFor(SpecialityDistricts.CommercialHub);
        // Assert
        Assert.That(hasDiscount, Is.False);
    }

    [Test]
    public void Unlocked4_2HolySite_2Campus_1GovernmentPlaza_WantToPlaceHolySite()
    {
        // Arrange
        _unlockedDistricts.Count.Returns(4);
        _playerDistricts.GetTotalCompletedDistrictsCount().Returns(5);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.HolySite).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.Campus).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.GovernmentPlaza).Returns(1);
        //Act
        var hasDiscount = _dut.HasDiscountFor(SpecialityDistricts.HolySite);
        // Assert
        Assert.That(hasDiscount, Is.False);
    }

    [Test]
    public void Unlocked4_2HolySite_2Campus_1GovernmentPlaza_WantToPlaceCampus()
    {
        // Arrange
        _unlockedDistricts.Count.Returns(4);
        _playerDistricts.GetTotalCompletedDistrictsCount().Returns(5);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.HolySite).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.Campus).Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.GovernmentPlaza).Returns(1);
        // Act
        var hasDiscount = _dut.HasDiscountFor(SpecialityDistricts.Campus);
        // Assert
        Assert.That(hasDiscount, Is.False);
    }

    [Test]
    public void Unlocked1_WantToPlaceCampus()
    {
        // Arrange
        _unlockedDistricts.Count.Returns(1);
        // Act
        var hasDiscount = _dut.HasDiscountFor(SpecialityDistricts.Campus);
        // Assert
        Assert.That(hasDiscount, Is.False);
    }

    [Test]
    public void Unlocked3_2Campus_WantToPlaceCampus()
    {
        // Arrange
        _unlockedDistricts.Count.Returns(3);
        _playerDistricts.GetTotalCompletedDistrictsCount().Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.Campus).Returns(2);
        // Act
        var hasDiscount = _dut.HasDiscountFor(SpecialityDistricts.Campus);
        // Assert
        Assert.That(hasDiscount, Is.False);
    }

    [Test]
    public void Unlocked3_2Campus_WantToPlaceHolySite()
    {
        // Arrange
        _unlockedDistricts.Count.Returns(3);
        _playerDistricts.GetTotalCompletedDistrictsCount().Returns(2);
        _playerDistricts.GetPlacedCountFor(SpecialityDistricts.Campus).Returns(2);
        // Act
        var hasDiscount = _dut.HasDiscountFor(SpecialityDistricts.HolySite);
        // Assert
        Assert.That(hasDiscount, Is.False);
    }
}
