using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace _code_store
{
	/*
     
SCREEN:

Login: 123.456.789-00 
Pass: @Mudar


Login( login, pass ) 
{
	var hashs = Sanitizacao(login);
	var user = GetByHash(hashs);
	
	var result = Identity.PasswordAsync(user.userName, pass, ..., ...);
	
	if(!result.HasClaim("login") ) {
		return ERRO ( Usuário Inativo );
	}
	
	return View();
	
}

Sanitizacao(login) {

	//login = 123.456.789-00 
	//sanitizado1 = EMPTY;

	var types = GetTypeHash();

	if(types.PorCpf) {
		sanitizado1 = RetirarPontoEspacoLetras(login);
		//sanitizado1 = 12345678900;
	}
	if(types.PorEmail) {
		sanitizado2 = VerificarArroba(login);
		//sanitizado2 = INVALIDO;
	}
	
	if(types.username) {
		sanitizado3 = ValidacaoDeLogin(login);
		//sanitizado3 = 123.456.789-00
	}
	
	
	
	hash1 = sha256(sanitizado1);
	//hash1 = #$%
	
	hash2 = sha256(sanitizado3)
	//hash2 = *&¨%$%$#$%¨%
	
	RETURN [ hash1, hash2];
}

GetByHash( [hashs] ): AspNetUser {
	var userId = TabelaUserHashs.GetUserIdByHash( [hashs] );
	return TabelaUser.GetUserById(123);
}



UserHash:
Id | UserId | Hash
1     123      #$%
2     123      ¨%$


TypeHash:
Id | Name 	| Active 
1    CPF    	 0
2    UserName 	 1
3    Email	     0


UserClaims:
Id | UserId | Claim
1     123      login
2     123      sei_la
     */

	public class MappingHashLoginStore
    {
        public MappingHashLoginStore()
        {
            List<HashType> repositoryHashType = new List<HashType>
            {
                new HashType{ Id = Guid.NewGuid(), Name = "CPF", Active = true },
                new HashType{ Id = Guid.NewGuid(), Name = "Email", Active = false },
                new HashType{ Id = Guid.NewGuid(), Name = "UserName", Active = false },
            };
            Dictionary<string, string> HashTypeMethod = new Dictionary<string, string>
            {
                ["CPF"] = "ByCpf",
                ["Email"] = "ByEmail",
                ["UserName"] = "ByUserName"
            };

            string input = "123.456.789-00";

            /* */
            /* */

            Test01(repositoryHashType, HashTypeMethod, input);
        }


        private static void Test01(List<HashType> repositoryHashType, Dictionary<string, string> HashTypeMethod, string input)
        {
            List<string> Hashs = new List<string>();

            Type sanitization = typeof(SanitizationMethods);

            foreach (var hashType in repositoryHashType.Where(x => x.Active))
            {
                if (HashTypeMethod.TryGetValue(hashType.Name, out string methodName))
                {
                    object? result = sanitization.GetMethod(methodName)?.Invoke(null, new[] { input });
                    if (result != null && !string.IsNullOrEmpty(result.ToString()))
                    {
                        Hashs.Add(result.ToString());
                    }
                }
            }

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(Hashs, options: new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }
    }



    public class HashType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }

    public static class SanitizationMethods
    {
        public static string ByCpf(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            var cpf = string.Concat(input.Where(ch => char.IsDigit(ch)));

            return SetHash(cpf);
        }

        public static string ByEmail(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            return SetHash(input.ToLowerInvariant());
        }

        public static string ByUserName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            return SetHash(input.ToLowerInvariant());
        }


        private static string SetHash(string plainText)
        {
            using SHA256Managed sha256 = new SHA256Managed();
            var _value = Encoding.UTF8.GetBytes(plainText);
            var hashValue = sha256.ComputeHash(_value);
            return Convert.ToBase64String(hashValue);
        }
    }
}