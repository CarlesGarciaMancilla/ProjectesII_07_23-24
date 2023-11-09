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

    void Start()
    {

    }

    public void OnclickButton(string button)
    {
        switch (button)
        {
            case "play":
                SceneManager.LoadScene("Nivel1");
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
