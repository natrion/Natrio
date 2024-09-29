
using UnityEngine;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SL 
{
    public static bool SaveNormal = false;
    public static string getFileName()
    {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + "gamedata.fun";
    }

    public static void saveGame(gamedata gd)
    {
        if (SaveNormal == true)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(getFileName(), FileMode.Create);

            formatter.Serialize(stream, gd);
            stream.Close();
        }
        else
        {          
            PlayerPrefs.SetString("gameData", PackGamedata(gd));            
        }    
    }
    public static gamedata loadGame()
    {
        if (SaveNormal == true)
        {
            if (File.Exists(getFileName()))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(getFileName(), FileMode.Open);
                gamedata gd = formatter.Deserialize(stream) as gamedata;

                stream.Close();
                return gd;
            }
            else
            {
                Debug.LogError("save file not found in " + getFileName());
                return null;
            }
        }
        else
        {
            if (PlayerPrefs.HasKey("gameData"))
            {               
                return UnpackGamedata(PlayerPrefs.GetString("gameData"));
            }
            else
            {
                return null;
            }    
        }         
    }


    public static string PackGamedata(gamedata _message)
    {
        using (MemoryStream input = new MemoryStream())
        {
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(input, _message);
            input.Seek(0, SeekOrigin.Begin);

            using (MemoryStream output = new MemoryStream())
            using (DeflateStream deflateStream = new DeflateStream(output, CompressionMode.Compress))
            {
                input.CopyTo(deflateStream);
                deflateStream.Close();

                return Convert.ToBase64String(output.ToArray());
            }
        }
    }

    public static gamedata UnpackGamedata(string _packed)
    {
        using (MemoryStream input = new MemoryStream(Convert.FromBase64String(_packed)))
        using (DeflateStream deflateStream = new DeflateStream(input, CompressionMode.Decompress))
        using (MemoryStream output = new MemoryStream())
        {
            deflateStream.CopyTo(output);
            deflateStream.Close();
            output.Seek(0, SeekOrigin.Begin);

            BinaryFormatter bformatter = new BinaryFormatter();
            gamedata message = (gamedata)bformatter.Deserialize(output);
            return message;
        }
    }
}
