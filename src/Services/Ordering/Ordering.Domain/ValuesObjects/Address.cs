namespace Ordering.Domain.ValuesObjects;

//public record Address (string FirstName,
//    string LastName,
//    string EmailAdress,
//    string AddressLine,
//    string Country,
//    string State,
//    string ZipCode)
public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAdress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;
}

