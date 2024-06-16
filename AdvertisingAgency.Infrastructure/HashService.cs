using AdvertisingAgency.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace AdvertisingAgency.Infrastructure;

internal sealed class HashService : IHashService, IDisposable
{
    private readonly SHA256 _hashFunction = SHA256.Create();

    public void Dispose() => _hashFunction.Dispose();

    public string HashPassword(string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] hasedPassword = _hashFunction.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hasedPassword);
    }
}
