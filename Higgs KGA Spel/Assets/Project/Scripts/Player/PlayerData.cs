using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int CurrentLevel;
    public int HighestLevel;
    public int Health;
    public float[] Position;

    public PlayerData(PlayerController player = null)
    {
        CurrentLevel = Gamemanager.LastLevel;
        HighestLevel = Gamemanager.HighestLevel;
        Health = Gamemanager.PlayerHealth;

        Position = new float[3];
        if (player != null)
        {            
            Position[0] = player.transform.position.x;
            Position[1] = player.transform.position.y;
            Position[2] = player.transform.position.z;
        }
        else
        {
            Position[0] = 0;
            Position[1] = 0;
            Position[2] = 0;
        }           
    }
}
