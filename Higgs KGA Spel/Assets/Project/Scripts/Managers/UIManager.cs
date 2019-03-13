using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private static GameObject life1;
    private static GameObject life2;
    private static GameObject life3;
    private static GameObject shot1;
    private static GameObject shot2;
    private static GameObject shot3;

    public float FadeTimer = 0f;
    private float alphaLevel = 0f;

    private void Awake()
    {
        MakeSingelton();   
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public static void InitializeUI()
    {
        life1 = GameObject.FindGameObjectWithTag("Life 1");
        life2 = GameObject.FindGameObjectWithTag("Life 2");
        life3 = GameObject.FindGameObjectWithTag("Life 3");
        shot1 = GameObject.FindGameObjectWithTag("Shot 1");
        shot2 = GameObject.FindGameObjectWithTag("Shot 2");
        shot3 = GameObject.FindGameObjectWithTag("Shot 3");

        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        shot1.SetActive(true);
        shot2.SetActive(true);
        shot3.SetActive(true);
    }

    private void MakeSingelton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }  

    public static void ManageLives(int livesLeft)
    {
        if (livesLeft == 2)
        {
            life1.SetActive(false);
            life2.SetActive(true);
            life3.SetActive(true);
        }
        else if (livesLeft == 1)
        {
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(true);
        }
        else if (livesLeft == 0)
        {
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(false);
        }
        else
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
        }
    }

    public static void ManageShots(int shotCount, bool powerShot)
    {
        if (shot1 != null && shot2 != null && shot3 != null)
        {
            if (shotCount == 1)
            {
                shot3.SetActive(false);
                shot2.SetActive(true);
                shot1.SetActive(true);
            }
            else if (shotCount == 2)
            {
                shot3.SetActive(false);
                shot2.SetActive(false);
                shot1.SetActive(true);
            }
            else if (shotCount == 3)
            {
                shot3.SetActive(false);
                shot2.SetActive(false);
                shot1.SetActive(false);
            }
            else if (shotCount == 0 && !powerShot)
            {
                shot1.SetActive(true);
                shot2.SetActive(true);
                shot3.SetActive(true);
            }
        }        
    }

    public static IEnumerator FadeIn(SpriteRenderer spriteRenderer, float fadeTime)
    {
        float alphaLevel = 0;
        float fadeTimer = 0;

        spriteRenderer.enabled = true;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

        while (alphaLevel < 1)
        {
            fadeTimer++;
            alphaLevel = fadeTimer / fadeTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, alphaLevel);

            yield return null;
        }

        fadeTimer = 0;
        alphaLevel = 0;
    }

    public static IEnumerator FadeOut(SpriteRenderer spriteRenderer, float fadeTime)
    {
        float alphaLevel = spriteRenderer.color.a;
        float fadeTimer = fadeTime;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        while (alphaLevel > 0)
        {
            fadeTimer--;
            alphaLevel = fadeTimer / fadeTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, alphaLevel);

            yield return null;
        }

        spriteRenderer.enabled = false;
    }
}
