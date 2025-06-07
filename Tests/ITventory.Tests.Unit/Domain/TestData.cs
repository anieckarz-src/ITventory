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

    public static Employee DefaultUser1 = Employee.Create("jonsnow123",
            "Jon",
            "Snow",
            Area.IT,
            "IT Specialist",
            Seniority.Manager,
            Guid.Empty,
            DefaultCountry.Id,
            DefaultOffice.Id,
            new DateOnly(2022, 1, 1),
            new DateOnly(1989, 5, 10));

    public static Employee DefaultUser2 = Employee.Create("tyrion123",
           "Tyrion",
           "Lannister",
           Area.Manufacturing,
           "Clerk",
           Seniority.Regular,
           Guid.Empty,
           DefaultCountry.Id,
           DefaultOffice.Id,
           new DateOnly(2020, 1, 1),
           new DateOnly(1985, 5, 10));


    public static Office DefaultOffice => new Office("Stoczniowa", "3", DefaultLocation.Id, 51.35, 24.50);

    public static (Employee leader, Department department) CreateDepartmentWithLeader()
    {
        var department = new Department("Internal Services", Guid.Empty); // tymczasowo pusty guid jako leader id
        var leader = Employee.Create(
            "jonsnow123",
            "Jon",
            "Snow",
            Area.IT,
            "IT Specialist",
            Seniority.Manager,
            DefaultCountry.Id,              // countryId
            department.Id,           // departmentId
            DefaultOffice.Id,
            new DateOnly(2022, 1, 1),
            new DateOnly(1989, 5, 10)
        );

        department.SetManager(leader.Id);

        return (leader, department);
    }
}
