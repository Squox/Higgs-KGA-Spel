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

    public PlayerData(PlayerActions player, Gamemanager gamemanager)
    {
        CurrentLevel = gamemanager.LastLevel;
        HighestLevel = gamemanager.HighestLevel;
        Health = player.Health;

        Position = new float[3];
        Position[0] = player.transform.position.x;
        Position[1] = player.transform.position.y;
        Position[2] = player.transform.position.z;
    }
	
}
