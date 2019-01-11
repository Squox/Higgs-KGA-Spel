using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    private bool a = true;
    public static Gamemanager instance;
    
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


    public void RestartGame()
    {
         do 
          {
            Debug.Log("1");
             if (Input.GetKey(KeyCode.Escape))
             {
                Debug.Log("2");
                a = false;
             }


             if (Input.GetKey(KeyCode.R))
             {
                Debug.Log("3");
                a = false;
                 SceneManager.LoadScene(SceneManager.GetActiveScene().name);
             }
          } while (a == true);

    }


}
