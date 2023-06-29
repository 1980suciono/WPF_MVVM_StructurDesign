using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WPF_MVVM_StructurDesign.Utilities
{
	public struct LoginHelper
	{
		public static bool LoginStatus = false;
		public static string Message = "";
		public static string CurrentUser = "";
		public static string ConnectionString = "";
		public static bool ConnectionStatus = false;
	};
	public class LoginUtil
	{
		//ini kode untuk form login
		//dictionary digunakan utk menampung banyak user
		public LoginUtil() : this(new Dictionary<string, string>())
		{
		}

		public LoginUtil(Dictionary<string, string> dict)
		{
			Dict = dict;
		}

		private Dictionary<string, string> dict;
		public Dictionary<string, string> Dict
		{
			get; set;
		}

		public bool Verification(string key, string value)
		{
			value = Sha1Hash(value);
			for (int i = 0; i < Dict.Count; i++)
			{
				if (Dict.ElementAt(i).Key.Equals(key))
				{
					if (Dict.ElementAt(i).Value.Equals(value))
					{
						LoginHelper.Message = "login success";
						LoginHelper.CurrentUser = Dict.ElementAt(i).Key;
						LoginHelper.LoginStatus = true;

					}
					LoginHelper.Message = "password salah!";
					return LoginHelper.LoginStatus;//menghidari 2 username yg sama
				}

			}
			LoginHelper.Message = "silahkan registrasi";
			return LoginHelper.LoginStatus;
		}

		public void LogOut()
		{
			LoginHelper.CurrentUser = "";
			LoginHelper.LoginStatus = false;
			LoginHelper.Message = "logut success";
		}

		public void Registrasi(string[] valuePair)
		{
			valuePair = Sha1HashPair(valuePair);
			if (!Dict.ContainsKey(valuePair[0]))
			{
				if (!valuePair[0].Equals(string.Empty) &&
				!valuePair[1].Equals(string.Empty))
				{
					
					Dict.Add(valuePair[0], valuePair[1]);
					LoginHelper.Message = "registrasi success";
				}
				else LoginHelper.Message = "registrasi gagal";

			}
			else LoginHelper.Message = "username sudah terdaftar";
			
		}

		public string[] Sha1HashPair(string[] valuePair)
        {
			valuePair[1] = Sha1Hash(valuePair[1]);
			return new string[] { valuePair[0], valuePair[1] };
        }

		public string Sha256Hash(string strVal)
		{
			SHA256 sha256Hash = SHA256.Create();

			byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(strVal));
			string hexStr = "";
			foreach (byte b in bytes)
			{
				hexStr += b.ToString("x2");
			}
			return hexStr;
		}
		public string Sha1Hash(string strVal)
		{
			SHA1 sha1Hash = SHA1.Create();

			byte[] bytes = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(strVal));
			string hexStr = "";
			foreach (byte b in bytes)
			{
				hexStr += b.ToString("x2");
			}
			return hexStr;


		}
	}
}

