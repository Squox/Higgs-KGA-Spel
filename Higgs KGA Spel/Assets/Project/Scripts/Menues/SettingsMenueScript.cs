using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsMenueScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicText;

    public static PlayerController Player;

    private void Start()
    {
        if (Audiomanager.MusicOn)
        {
            musicButton.GetComponent<Image>().color = Color.green;
            musicText.text = "Music On";
        }
        else
        {
            musicButton.GetComponent<Image>().color = Color.red;
            musicText.text = "Music Off";
        }
    }

    public void ToggleMusic()
    {
        Audiomanager.MusicOn = !Audiomanager.MusicOn;

        if (Audiomanager.MusicOn)
        {
            musicButton.GetComponent<Image>().color = Color.green;
            musicText.text = "Music On";
        }
        else
        {
            musicButton.GetComponent<Image>().color = Color.red;
            musicText.text = "Music Off";
        }           
    }

    public void Exit()
    {
        Gamemanager.SavePlayer();
        Gamemanager.LoadScene("Start menu");
    }

    public void DeleteSaves()
    {
        SaveSystem.DeleteSaves();
        Gamemanager.SavePlayer();
    }
}
