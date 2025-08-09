using System.Security.Cryptography;
using System.Text;

namespace GlicareApp.Repositories.Services;

public class EncryptionService
{
    private readonly byte[] _key;

    public EncryptionService(string key)
    {
        using var sha256 = SHA256.Create();
        _key = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
    }
}