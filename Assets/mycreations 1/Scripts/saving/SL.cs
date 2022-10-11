
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SL 
{
    private static string getFileName()
    {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + "gamedata.fun";
    }

    public static void saveGame(gamedata gd)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(getFileName() , FileMode.Create);

        formatter.Serialize(stream, gd);
        stream.Close();
    }
    public static gamedata loadGame()
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
}
