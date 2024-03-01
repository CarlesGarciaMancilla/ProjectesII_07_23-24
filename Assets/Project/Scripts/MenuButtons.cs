using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public Button play;
    public Button exit;
    public GameObject canvasTitle;
    public GameObject canvasLevels;
    public GameObject canvasOptions;


    void Start()
    {
        canvasOptions.SetActive(false);
        canvasLevels.SetActive(false);
    }

    public void OnclickButton(string button)
    {
        switch (button)
        {
            case "play":
                SceneManager.LoadScene("Nivel1");
                break;
            case "levels":
                canvasTitle.SetActive(false);
                canvasOptions.SetActive(false);
                canvasLevels.SetActive(true);
                break;
            case "options":
                canvasTitle.SetActive(false);
                canvasOptions.SetActive(true);
                canvasLevels.SetActive(false);
                break;
            case "menu":
                SceneManager.LoadScene("menu");
                break;
            case "back":
                canvasTitle.SetActive(true);
                canvasOptions.SetActive(false);
                canvasLevels.SetActive(false);
                break;
            case "level1":
                SceneManager.LoadScene("Nivel1");
                break;
            case "level2":
                SceneManager.LoadScene("Nivel2");
                break;
            case "level3":
                SceneManager.LoadScene("Nivel3");
                break;
            case "level4":
                SceneManager.LoadScene("Nivel4");
                break;
            case "level5":
                SceneManager.LoadScene("Nivel5");
                break;
            case "level6":
                SceneManager.LoadScene("Nivel6");
                break;
            case "level7":
                SceneManager.LoadScene("Nivel7");
                break;
            case "exit":
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
               Application.Quit();
#endif

                break;

        }


    }
}
