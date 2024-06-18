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
    public Image panel;


    void Start()
    {
        panel.CrossFadeAlpha(0, 1.0f, false);
        canvasOptions.SetActive(false);
        canvasLevels.SetActive(false);

    }

    public IEnumerator FadeOut()
    {
        Debug.Log("llama a la corrutina");
        panel.CrossFadeAlpha(1, 0.5f, false);
        Debug.Log("siguela corrutina");
        yield return new WaitForSeconds(0.5f);

    }
    public IEnumerator Level1()
    {
        Debug.Log("llama a la corrutina");
        panel.CrossFadeAlpha(1, 0.5f, false);
        Debug.Log("siguela corrutina");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Nivel1");
    }

    public IEnumerator Level2()
    {
        Debug.Log("llama a la corrutina");
        panel.CrossFadeAlpha(1, 0.5f, false);
        Debug.Log("siguela corrutina");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Nivel2");
    }

    public IEnumerator Level3()
    {
        Debug.Log("llama a la corrutina");
        panel.CrossFadeAlpha(1, 0.5f, false);
        Debug.Log("siguela corrutina");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Nivel3");
    }

    public IEnumerator Level4()
    {
        Debug.Log("llama a la corrutina");
        panel.CrossFadeAlpha(1, 0.5f, false);
        Debug.Log("siguela corrutina");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Nivel4");
    }
    public IEnumerator Level5()
    {
        Debug.Log("llama a la corrutina");
        panel.CrossFadeAlpha(1, 0.5f, false);
        Debug.Log("siguela corrutina");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Nivel5");
    }
    public IEnumerator Level6()
    {
        Debug.Log("llama a la corrutina");
        panel.CrossFadeAlpha(1, 0.5f, false);
        Debug.Log("siguela corrutina");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Nivel6");
    }
    public IEnumerator Level7()
    {
        Debug.Log("llama a la corrutina");
        panel.CrossFadeAlpha(1, 0.5f, false);
        Debug.Log("siguela corrutina");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Nivel7 1");
    }
    public IEnumerator Menu()
    {
        Debug.Log("llama a la corrutina");
        panel.CrossFadeAlpha(1, 0.5f, false);
        Debug.Log("siguela corrutina");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("menu");
    }


    public void OnclickButton(string button)
    {
        switch (button)
        {
            case "play":
                StartCoroutine(Level1());
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
                StartCoroutine(Menu());
                break;
            case "back":
                canvasTitle.SetActive(true);
                canvasOptions.SetActive(false);
                canvasLevels.SetActive(false);
                break;
            case "level1":
                StartCoroutine(Level1());            
                break;
            case "level2":
                StartCoroutine(Level2());

                break;
            case "level3":
                StartCoroutine(Level3());
                break;
            case "level4":
                StartCoroutine(Level4());
                break;
            case "level5":
                StartCoroutine(Level5());
                break;
            case "level6":
                StartCoroutine(Level6());
                break;
            case "level7":
                StartCoroutine(Level7());
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
