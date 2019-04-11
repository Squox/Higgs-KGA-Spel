using UnityEngine;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;
    [SerializeField] private Button lvl1Btn;
    [SerializeField] private Button lvl2Btn;
    [SerializeField] private Button lvl3Btn;
    [SerializeField] private Button lvl4Btn;

    private void Start()
    {
        updateLevelButtonColor();
    }

    public void Exit()
    {
        StartCoroutine(Gamemanager.LoadAsyncronously("Selection menue", loadingScreen, slider, progressText));
    }

    public void LoadFirstLevel()
    {
        if (Gamemanager.HighestLevel >= 1)
            StartCoroutine(Gamemanager.LoadAsyncronously("First Level", loadingScreen, slider, progressText));
    }
    public void LoadSecondLevel()
    {
        if (Gamemanager.HighestLevel >= 2)
            StartCoroutine(Gamemanager.LoadAsyncronously("Second Level", loadingScreen, slider, progressText));
    }
    public void LoadThirdLevel()
    {
        if (Gamemanager.HighestLevel >= 3)
            StartCoroutine(Gamemanager.LoadAsyncronously("Third Level", loadingScreen, slider, progressText));
    }
    public void LoadFourthLevel()
    {
        if (Gamemanager.HighestLevel >= 4)
            StartCoroutine(Gamemanager.LoadAsyncronously("Fourth Level", loadingScreen, slider, progressText));
    }

    private void updateLevelButtonColor()
    {
        if (Gamemanager.HighestLevel == 1)
        {
            lvl1Btn.GetComponent<Image>().color = Color.green;
            lvl2Btn.GetComponent<Image>().color = Color.red;
            lvl3Btn.GetComponent<Image>().color = Color.red;
            lvl4Btn.GetComponent<Image>().color = Color.red;
        }
        else if (Gamemanager.HighestLevel == 2)
        {
            lvl1Btn.GetComponent<Image>().color = Color.green;
            lvl2Btn.GetComponent<Image>().color = Color.green;
            lvl3Btn.GetComponent<Image>().color = Color.red;
            lvl4Btn.GetComponent<Image>().color = Color.red;
        }
        else if (Gamemanager.HighestLevel == 3)
        {
            lvl1Btn.GetComponent<Image>().color = Color.green;
            lvl2Btn.GetComponent<Image>().color = Color.green;
            lvl3Btn.GetComponent<Image>().color = Color.green;
            lvl4Btn.GetComponent<Image>().color = Color.red;
        }
        else if (Gamemanager.HighestLevel == 4)
        {
            lvl1Btn.GetComponent<Image>().color = Color.green;
            lvl2Btn.GetComponent<Image>().color = Color.green;
            lvl3Btn.GetComponent<Image>().color = Color.green;
            lvl4Btn.GetComponent<Image>().color = Color.green;
        }
        else
        {
            lvl1Btn.GetComponent<Image>().color = Color.red;
            lvl2Btn.GetComponent<Image>().color = Color.red;
            lvl3Btn.GetComponent<Image>().color = Color.red;
            lvl4Btn.GetComponent<Image>().color = Color.red;
        }
    }
}
