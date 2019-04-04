using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int CurrentLevel;
    public int HighestLevel;
    public int Health;
    public float[,] Position = new float[5, 3];

    public PlayerData(PlayerController player = null, int level = 0)
    {
        CurrentLevel = Gamemanager.LastLevel;
        HighestLevel = Gamemanager.HighestLevel;
        Health = Gamemanager.PlayerHealth;

        if (player != null && level != 0)
        {            
            Position[level, 0] = player.transform.position.x;
            Position[level, 1] = player.transform.position.y;
            Position[level, 2] = player.transform.position.z;
        }
        else
        {
            for (int l = 0; l < Position.GetLength(0); l++)
                for (int p = 0; p < Position.GetLength(1); p++)
                    Position[l, p] = 0f;
        }           
    }
}
