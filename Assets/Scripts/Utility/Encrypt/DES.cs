using System;
using System.IO;
using System.Security.Cryptography;

namespace Encrypt
{
	public static class DES
	{
		private const int DESENCRYPT_CHAR_BYTE = 8;

		private const int DESENCRYPT_CHAR_NUMBER = 8;

		private const byte DESENCRYPT_MASK = 0x80;

		public static byte[] DES_KEY = new byte[8]{0xE1,0xA8,0xC7,0xA6,0x3C,0xDC,0x72,0x1C};

		public static byte[] DES_IV = new byte[8]{0,0,0,0,0,0,0,0};

		public static byte[] ConvertArrayToDesData(byte[] arr)
		{
			int index = 0;
			byte[] buf = new byte[64];
			for (int i = 0; i < DESENCRYPT_CHAR_NUMBER; i++)
			{
				byte n = arr[i];
				for (int j = 0; j < DESENCRYPT_CHAR_BYTE; j++)
				{
					buf[index] = (byte)((n & DESENCRYPT_MASK >> j) != 0 ? 1 : 0);
					index++;
				}
			}
			return buf;
		}

		public static byte[] Encrypt(byte[] inData)
		{
			try
			{
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				dESCryptoServiceProvider.Key = DES.DES_KEY;
				dESCryptoServiceProvider.IV = DES.DES_IV;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					try
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write))
						{
							BinaryWriter binaryWriter = new BinaryWriter(cryptoStream);
							binaryWriter.Write(inData);
							binaryWriter.Close();
						}
						byte[] result = memoryStream.ToArray();
						return result;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message + ex.StackTrace);
						byte[] result = null;
						return result;
					}
				}
			}
			catch (Exception ex2)
			{
				Console.WriteLine(ex2.Message + ex2.StackTrace);
			}
			return null;
		}
	}
}

