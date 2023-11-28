using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace OAuthService;

public class DevKeys
{

	public DevKeys(IWebHostEnvironment env)
	{
		RsaKey = RSA.Create();
		var path = Path.Combine(env.ContentRootPath, "crypto_key");
		if (File.Exists(path))
		{
			var rsaKey = RSA.Create();
			rsaKey.ImportRSAPrivateKey(File.ReadAllBytes(path), out _);
		}
		else
		{
			var privateKey = RsaKey.ExportRSAPrivateKey();
			File.WriteAllBytes(path, privateKey);
		}
	}

	public RSA RsaKey { get; }
	public RsaSecurityKey RsaSecurityKey => new RsaSecurityKey(RsaKey);
}
