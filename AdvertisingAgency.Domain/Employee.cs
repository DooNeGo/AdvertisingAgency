namespace AdvertisingAgency.Domain;

public readonly record struct EmployeeId(Guid Value) : IStronglyTypedId<Guid>
{
    public static readonly EmployeeId Empty = new(Guid.Empty);

    public static EmployeeId Create() => new(Guid.NewGuid());
}

public sealed class Employee
{
    public Employee(FullName fullName, string phoneNumber, Position position)
    {
        Id = EmployeeId.Create();
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Position = position;
    }

    private Employee() { }

    public EmployeeId Id { get; }

    public FullName FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = string.Empty;

    public Position Position { get; set; } = null!;
}
