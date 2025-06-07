using ITventory.Domain;
using ITventory.Domain.Enums;
using Shouldly;

namespace ITventory.Tests.Unit;

public class LicenseTest
{
    SoftwareLicense license = SoftwareLicense.Create(LicenseType.PerUser, "ABC123", new DateOnly(2026, 1, 1), 3);
    Employee user1 = TestData.DefaultUser1;
    Employee user2 = TestData.DefaultUser2;

    [Fact]
    public void CreateLicense_Should_SetPropertiesCorrectly()
    {

        license.ShouldNotBeNull();
        license.LicenseKey.ShouldBe("ABC123");
        license.UseCount.ShouldBe(0);
        license.AssignedUsers.ShouldBeEmpty();
    }

    [Fact]
    public void AssignLicense_Should_AssignLicense()
    {
        license.AssignToUser(user1);

        license.AssignedUsers.Count.ShouldBe(1);
        license.AssignedUsers[0].ShouldNotBeNull();
    }

    [Fact]

    public void AssignLicense_Should_NotAllowAssignmentTwice()
    {
        license.AssignToUser(user1);
        license.AssignToUser(user2);
        license.AssignedUsers.Count.ShouldBe(2);

        Should.Throw<InvalidOperationException>(() => license.AssignToUser(user1));

    }

    [Fact]
    public void AssignLicenseOverMax_Should_NotAllowAssignment()
    {
        SoftwareLicense license = SoftwareLicense.Create(LicenseType.PerUser, "dup123", new DateOnly(2027, 1, 1), 1);
        license.AssignToUser(user1);
        license.AssignedUsers.Count().ShouldBe(1);

        Should.Throw<InvalidOperationException>(()=>  license.AssignToUser(user2));

    }

}
