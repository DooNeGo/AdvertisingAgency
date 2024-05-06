namespace AdvertisingAgency.Domain;

public readonly record struct EmployeeId(Guid Value)
{
    public static readonly EmployeeId Empty = default;

    public static EmployeeId CreateNew() => new(Guid.NewGuid());
}

public class Employee
{
    public Employee(string fullName, string phoneNumber, DateOnly birthday, Position position)
    {
        Id = EmployeeId.Empty;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Birthday = birthday;
        Position = position;
    }

    private Employee() { }

    public EmployeeId Id { get; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public DateOnly Birthday { get; set; }

    public Position Position { get; set; } = null!;
}
