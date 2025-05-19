using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Usa TMPro si quieres
using TMPro;

public class IABriefing : MonoBehaviour
{
    public GameObject panel;          // El panel que contiene el texto, para activar/desactivar
    public TMP_Text messageText;          // Texto donde mostrar la frase
    public float typingSpeed = 0.05f; // Velocidad de tipeo letra a letra
    public float displayDuration = 5f; // Tiempo que se mantiene visible el panel

    [TextArea]
    public string[] goodPrompts;      // Frases correctas (5)
    [TextArea]
    public string[] brokenPrompts;    // Frases con fallo

    private bool isDisplaying = false;

    void Start()
    {
        panel.SetActive(false);
        ShowBriefing();
    }

    public void ShowBriefing()
    {
        if (isDisplaying) return;

        isDisplaying = true;
        panel.SetActive(true);

        // Decidir si mostramos prompt roto (25%) o bueno (75%)
        bool showBroken = Random.value < 0.25f;

        string[] sourceArray = showBroken ? brokenPrompts : goodPrompts;

        string prompt = sourceArray[Random.Range(0, sourceArray.Length)];

        StartCoroutine(TypeAndHide(prompt));
    }

    IEnumerator TypeAndHide(string message)
    {
        messageText.text = "";

        foreach (char c in message)
        {
            // Para prompts rotos, a veces cambia letras por símbolos extraños para efecto "error"
            if (message == brokenPrompts[0]) // Ejemplo: podrías identificar por el string o añadir flag en otro sistema
            {
                // Aquí podrías meter lógica para insertar símbolos aleatorios
            }

            messageText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(displayDuration);

        panel.SetActive(false);
        isDisplaying = false;
    }
}