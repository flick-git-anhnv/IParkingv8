using System;
using System.Security.Cryptography;
using System.Text;
namespace Kztek.LPR
{
	public class CryptorEngineLPR
	{
        public static string key = "@#$NXT SOFTWARE$#@";
        public static void SetKey(string securityKey)
        {
            if (!securityKey.Contains("NXT SOFTWARE"))
            {
                CryptorEngineLPR.key = "NXT SOFTWARE " + securityKey;
                return;
            }
            CryptorEngineLPR.key = securityKey;
        }

		public static string Encrypt(string toEncrypt, bool useHashing)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
				byte[] array;
				if (useHashing)
				{
					MD5CryptoServiceProvider expr_14 = new MD5CryptoServiceProvider();
					array = expr_14.ComputeHash(Encoding.UTF8.GetBytes(CryptorEngineLPR.key));
					expr_14.Clear();
				}
				else
				{
					array = Encoding.UTF8.GetBytes(CryptorEngineLPR.key);
				}
				TripleDESCryptoServiceProvider expr_46 = new TripleDESCryptoServiceProvider();
				expr_46.Key = array;
				expr_46.Mode = CipherMode.ECB;
				expr_46.Padding = PaddingMode.PKCS7;
				byte[] array2 = expr_46.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
				expr_46.Clear();
				result = Convert.ToBase64String(array2, 0, array2.Length);
			}
			catch
			{
				result = "";
			}
			return result;
		}
		public static string Decrypt(string cipherString, bool useHashing)
		{
			string result;
			try
			{
				byte[] array = Convert.FromBase64String(cipherString);
				byte[] array2;
				if (useHashing)
				{
					MD5CryptoServiceProvider expr_0F = new MD5CryptoServiceProvider();
					array2 = expr_0F.ComputeHash(Encoding.UTF8.GetBytes(CryptorEngineLPR.key));
					expr_0F.Clear();
				}
				else
				{
					array2 = Encoding.UTF8.GetBytes(CryptorEngineLPR.key);
				}
				TripleDESCryptoServiceProvider expr_41 = new TripleDESCryptoServiceProvider();
				expr_41.Key = array2;
				expr_41.Mode = CipherMode.ECB;
				expr_41.Padding = PaddingMode.PKCS7;
				byte[] bytes = expr_41.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
				expr_41.Clear();
				result = Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				result = "";
			}
			return result;
		}
		public static string Encrypt(string toEncrypt, bool useHashing, string securityKey)
		{
			string result;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
				byte[] array;
				if (useHashing)
				{
					MD5CryptoServiceProvider expr_14 = new MD5CryptoServiceProvider();
					array = expr_14.ComputeHash(Encoding.UTF8.GetBytes(securityKey));
					expr_14.Clear();
				}
				else
				{
					array = Encoding.UTF8.GetBytes(securityKey);
				}
				TripleDESCryptoServiceProvider expr_3E = new TripleDESCryptoServiceProvider();
				expr_3E.Key = array;
				expr_3E.Mode = CipherMode.ECB;
				expr_3E.Padding = PaddingMode.PKCS7;
				byte[] array2 = expr_3E.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
				expr_3E.Clear();
				result = Convert.ToBase64String(array2, 0, array2.Length);
			}
			catch
			{
				result = "";
			}
			return result;
		}
		public static string Decrypt(string cipherString, bool useHashing, string securityKey)
		{
			string result;
			try
			{
				byte[] array = Convert.FromBase64String(cipherString);
				byte[] array2;
				if (useHashing)
				{
					MD5CryptoServiceProvider expr_0F = new MD5CryptoServiceProvider();
					array2 = expr_0F.ComputeHash(Encoding.UTF8.GetBytes(securityKey));
					expr_0F.Clear();
				}
				else
				{
					array2 = Encoding.UTF8.GetBytes(securityKey);
				}
				TripleDESCryptoServiceProvider expr_39 = new TripleDESCryptoServiceProvider();
				expr_39.Key = array2;
				expr_39.Mode = CipherMode.ECB;
				expr_39.Padding = PaddingMode.PKCS7;
				byte[] bytes = expr_39.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
				expr_39.Clear();
				result = Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				result = "";
			}
			return result;
		}
		public static string RemoveUseLess(string st)
		{
			for (int i = st.Length - 1; i >= 0; i--)
			{
				char c = char.ToUpper(st[i]);
				if ((c < 'A' || c > 'Z') && (c < '1' || c > '9'))
				{
					st = st.Remove(i, 1);
				}
			}
			return st;
		}
		public static string InverseByBase(string st, int MoveBase)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < st.Length; i += MoveBase)
			{
				int length;
				if (i + MoveBase > st.Length - 1)
				{
					length = st.Length - i;
				}
				else
				{
					length = MoveBase;
				}
				stringBuilder.Append(CryptorEngineLPR.InverseString(st.Substring(i, length)));
			}
			return stringBuilder.ToString();
		}
		public static string InverseString(string st)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = st.Length - 1; i >= 0; i--)
			{
				stringBuilder.Append(st[i]);
			}
			return stringBuilder.ToString();
		}
		public static string ConvertToLetterDigit(string st)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < st.Length; i++)
			{
				char c = st[i];
				if (!char.IsLetterOrDigit(c))
				{
					stringBuilder.Append(Convert.ToInt16(c).ToString());
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
