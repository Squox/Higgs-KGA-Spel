using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/Player.file";

    public static void SavePlayer(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream.Length <= 0)
            {
                return null;
            }

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("File doesn't exist.");
            return null;
        }
    }

    public static void DeleteSaves()
    {
        Gamemanager.LastLevel = 0;
        Gamemanager.HighestLevel = 0;
        Gamemanager.LastCheckpointPosition = Vector3.zero;
        Gamemanager.PlayerHealth = Gamemanager.PlayerMaxHealth;
    }
}