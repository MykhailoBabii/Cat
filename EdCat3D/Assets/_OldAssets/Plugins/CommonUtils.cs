
using UnityEngine;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;

public class CommonUtils
{

    public static string Md5(string value)
    {
        MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
        byte[] data = Encoding.ASCII.GetBytes(value);
        data = x.ComputeHash(data);
        string ret = "";
        for (int i = 0; i < data.Length; i++)
            ret += data[i].ToString("x2").ToLower();
        return ret;
    }

	public static string base64encode(string inputText)
	{
		byte[] bytesToEncode = Encoding.UTF8.GetBytes (inputText);
		string encodedText = Convert.ToBase64String (bytesToEncode);
		return encodedText;
	}

	public static string base64decode(string encodedText)
	{
		byte[] decodedBytes = Convert.FromBase64String (encodedText);
		string decodedText = Encoding.UTF8.GetString (decodedBytes);
		return decodedText;
	}

}

