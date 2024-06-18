using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuIngame : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnclickButton(string button)
    {
        switch (button)
        {
            case "menu":
                SceneManager.LoadScene("menu");
                break;
        }


    }
}
