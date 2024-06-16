using AdvertisingAgency.Domain;

namespace AdvertisingAgency.Application.Dto;

public sealed record UserDto
(
    ClientId Id,
    string UserName,
    string PhoneNumber,
    string CompanyName,
    FullName FullName,
    Country Country
);