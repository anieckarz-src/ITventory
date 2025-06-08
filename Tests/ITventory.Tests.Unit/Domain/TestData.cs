using ITventory.Domain.Enums;
using ITventory.Domain;

public static class TestData
{
    public static Country DefaultCountry => new Country("Poland", "PL", Region.Europe);

    public static Location DefaultLocation = new Location(
        "Sandomierz Float",
        DefaultCountry.Id,
        "27-600",
        "Sandomierz",
        51.44,
        24.55,
        TypeOfPlant.Factory
    );

    public static Employee DefaultUser1 = Employee.CreateMinimal("jonsnow123",
            Guid.NewGuid().ToString());

    public static Employee DefaultUser2 = Employee.CreateMinimal("tyrion123", Guid.NewGuid().ToString());
          


    public static Office DefaultOffice => new Office("Stoczniowa", "3", DefaultLocation.Id, 51.35, 24.50);

    public static (Employee leader, Department department) CreateDepartmentWithLeader()
    {
        var department = new Department("Internal Services", Guid.Empty); // tymczasowo pusty guid jako leader id
        var leader = Employee.CreateMinimal(
            "jonsnow123",
            Guid.NewGuid().ToString());

        department.SetManager(leader.Id);

        return (leader, department);
    }
}
