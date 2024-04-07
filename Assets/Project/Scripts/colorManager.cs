using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class colorManager : MonoBehaviour
{
    public SpriteRenderer imageToChange; // Asigna tu componente Image en el inspector
    public Color[] colors; // Asigna los colores que quieras en el inspector
    public float changeInterval = 1f; // Intervalo de tiempo entre cambios de color

    private void Start()
    {
        if (imageToChange == null)
            imageToChange = GetComponent<SpriteRenderer>();

        if (imageToChange != null)
            StartCoroutine(ChangeColorInLoop());
        else
            Debug.LogError("Image component not found on the GameObject.", this);
    }

    private IEnumerator ChangeColorInLoop()
    {
        int colorIndex = 0;
        while (true) // Bucle infinito, cambia los colores continuamente
        {
            imageToChange.color = colors[colorIndex];
            yield return new WaitForSeconds(changeInterval); // Espera un intervalo antes de cambiar al siguiente color

            // Incrementa el índice y vuelve a 0 si alcanza el final del arreglo
            colorIndex = (colorIndex + 1) % colors.Length;
        }
    }
}