using UnityEngine;
using UnityEngine.UI;

public static class LevelSetup
{
    public static void SetUpLevel(int levelIndex, GameObject loadingScreen, Slider slider, Text progressText, AudioSource audioSource, float volume = 0)
    {      
        Gamemanager.LoadingScreen = loadingScreen;
        Gamemanager.Slider = slider;
        Gamemanager.ProgressText = progressText;
        Gamemanager.LastLevel = levelIndex;

        Audiomanager.PlayMusic(audioSource, volume);
    }

    public static void SetPlayerValues(GameObject cp0)
    {
        if (Gamemanager.LastCheckpointPosition == cp0.transform.position)
            Gamemanager.DeathCounter = 0;
        else if (Gamemanager.DeathCounter > 3)
        {
            Gamemanager.LastCheckpointPosition = cp0.transform.position;
            Gamemanager.DeathCounter = 0;
            Gamemanager.CheckPointCounter = 0;
        }
    }

    public static void LoadPlayer(int levelIndex, GameObject[] cps)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();

        Gamemanager.LoadPlayer();

        int counter = 0;

        foreach (GameObject cp in cps)
        {
            if (cp.transform.position != Gamemanager.LastCheckpointPosition)
                counter++;
            else
                continue;

            if (counter == cps.Length)
                Gamemanager.LastCheckpointPosition = cps[0].transform.position;
        }

        if (Gamemanager.PlayerDead)
        {
            player.transform.position = Gamemanager.LastCheckpointPosition;
            Gamemanager.PlayerHealth = Gamemanager.PlayerMaxHealth;
            Gamemanager.SavePlayer(playerController);
            Gamemanager.PlayerDead = false;
        }
        else if (Gamemanager.HighestLevel < levelIndex)
        {
            player.transform.position = cps[0].transform.position;
            Gamemanager.PlayerHealth = Gamemanager.PlayerMaxHealth;
            Gamemanager.LastCheckpointPosition = cps[0].transform.position;
            Gamemanager.HighestLevel = levelIndex;
            Gamemanager.SavePlayer(playerController);
        }
        else
        {
            player.transform.position = Gamemanager.PlayerPosition;
            PlayerController.Health = Gamemanager.PlayerHealth;
        }
    }
}
