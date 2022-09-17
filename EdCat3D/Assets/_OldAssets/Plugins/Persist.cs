
using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public static class Persist
{
	public static string _profilenum = "";
	
    public static void serializeObjectToFile( object obj, string filename )
    {
        // Get the path to the file to save to
        string path = Persist.pathForDocumentsFile( filename );

        // Create a StreamWriter to write the XML to disk
        StreamWriter sw = new StreamWriter( path );
        XmlSerializer serializer = new XmlSerializer( obj.GetType() );
		serializer.Serialize( sw, obj );
        sw.Close();
    }
	
	
    public static object deserializeObjectFromFile( string filename, Type type )
    {
        // Get the path to the file to save to
        string path = Persist.pathForDocumentsFile( filename );
        object obj;

        StreamReader sr = new StreamReader( path );
        XmlSerializer serializer = new XmlSerializer( type );
        obj = serializer.Deserialize( sr );
		sr.Close();

        return obj;
    }
	
	public static bool _isExistsFile(string fname)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			WWW filew = new WWW(fname);
			if ((filew != null)&&(filew.error == null)) return true;
			return false;
		}
		else
		{
			if (System.IO.File.Exists(fname)) return true;
			return false;
		}
	}

	public static void writeStringToFile( string str, string filename )
	{
        // Get the path to the file to save to
        string path = Persist.pathForDocumentsFile( filename );
		
        StreamWriter sw = new StreamWriter( path );
        sw.Write( str );
        sw.Close();
	}

	public static string readStringFromFile( string filename )
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			WWW filew = new WWW("jar:file://" + Application.dataPath + "!/assets/" + filename);
			if (filew != null)
			{
				while (!filew.isDone) {}
				Debug.Log(filew.text);
				return filew.text;
			}
			else
			{
				Debug.Log("AAAAAAAAAAAAAAA");
				return "";
			}
		}
		else
		{
	        // Get the path to the file to save to
	        string path = Persist.pathForDocumentsFile( filename );
			
	        StreamReader sr = new StreamReader( path );
	        string str = sr.ReadToEnd();
	        sr.Close();
			
			return str;
		}
	}
	
	
	public static void writeStringToFilePersist( string str, string filename )
	{
        // Get the path to the file to save to
		
		System.IO.Directory.CreateDirectory(Persist.pathForDocumentsFilePersist(""));
        string path = Persist.pathForDocumentsFilePersist( filename );

        StreamWriter sw = new StreamWriter( path );
        sw.Write( str );
        sw.Close();
	}
	
	public static void writeStringToFilePersistJust( string str, string filename )
	{
        // Get the path to the file to save to
		
		System.IO.Directory.CreateDirectory(Persist.pathForDocumentsFilePersistJust(""));
        string path = Persist.pathForDocumentsFilePersistJust( filename );

        StreamWriter sw = new StreamWriter( path );
        sw.Write( str );
        sw.Close();
	}
	
	
	public static string readStringFromFilePersist( string filename )
	{
        // Get the path to the file to save to
        string path = Persist.pathForDocumentsFilePersist( filename );
		
        StreamReader sr = new StreamReader( path );
        string str = sr.ReadToEnd();
        sr.Close();
		
		return str;
	}
	
	public static string readStringFromFilePersistJust( string filename )
	{
        // Get the path to the file to save to
        string path = Persist.pathForDocumentsFilePersistJust( filename );
		
        StreamReader sr = new StreamReader( path );
        string str = sr.ReadToEnd();
        sr.Close();
		
		return str;
	}
	
//    public static string pathForDocumentsFile( string filename ) 
//    { 
//        // Application.dataPath returns
//        // /var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/myappname.app/Data
//#if !UNITY_IPHONE 
//		string path = Path.Combine( Application.dataPath, "" );
//		//Debug.Log( path );
//#else
//        string path = Application.dataPath.Substring( 0, Application.dataPath.Length - 5 );
//#endif
//		
//        // Strip application name 
//        path = path.Substring( 0, path.LastIndexOf( '/' ) );
//		
//        return Path.Combine( Path.Combine( path, "" ), filename );
//    }
	
    public static string pathForDocumentsFile( string filename ) 
    { 
        string path = Application.dataPath;
		if (Application.platform == RuntimePlatform.Android)
		{
				path = "jar:file://" + path + "!/assets/" + filename;
				return path;
		}
        return Path.Combine( Path.Combine( path, "" ), filename );
    }
	
    public static string pathForDocumentsFilePersist( string filename ) 
    {
        string path = Application.persistentDataPath + "/" + _profilenum;
		if ((Application.platform == RuntimePlatform.WindowsPlayer)||(Application.platform == RuntimePlatform.WindowsEditor))
		{
				string[] someArr = path.Split("/"[0]);
				string[] someAnother = new string[someArr.Length];
				int counter = 0;
				foreach (string oneOf in someArr)
				{
					if (oneOf == "LocalLow")
					{
						someAnother[counter] = "Roaming";
					}
					else
					{
						someAnother[counter] = oneOf;
					}
					counter++;
				}
			
				path = "";
				foreach (string someOneOf in someAnother)
				{
					path = path + someOneOf + "/";
				}
		}
        return Path.Combine( Path.Combine( path, "" ), filename );
    }

    public static string pathForDocumentsFilePersistJust( string filename ) 
    {
        string path = Application.persistentDataPath;
		if ((Application.platform == RuntimePlatform.WindowsPlayer)||(Application.platform == RuntimePlatform.WindowsEditor))
		{
				string[] someArr = path.Split("/"[0]);
				string[] someAnother = new string[someArr.Length];
				int counter = 0;
				foreach (string oneOf in someArr)
				{
					if (oneOf == "LocalLow")
					{
						someAnother[counter] = "Roaming";
					}
					else
					{
						someAnother[counter] = oneOf;
					}
					counter++;
				}
			
				path = "";
				foreach (string someOneOf in someAnother)
				{
					path = path + someOneOf + "/";
				}
		}
        return Path.Combine( Path.Combine( path, "" ), filename );
    }

}