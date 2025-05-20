using UnityEngine;
using UnityEngine.SceneManagement;  // Para cargar el men� principal (opcional)
using UnityEngine.UI;

public class PausaMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject accessibilityMenuPanel;
    public Button accessibilityButton;
    public Button resumeButton;
    public Button exitButton;

    private bool isPaused = false;

    void Start()
    {
        // Aseg�rate de que el men� de pausa y el de accesibilidad est�n desactivados al principio
        pauseMenuPanel.SetActive(false);
        accessibilityMenuPanel.SetActive(false);

        // Asignar las funciones de los botones
        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(ExitGame);
        accessibilityButton.onClick.AddListener(OpenAccessibilityMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))  
        {
            if (isPaused)
                ResumeGame();  // Reanudar juego si ya est� pausado
            else
                PauseGame();  // Pausar juego si no lo est�
        }
    }

    // M�todo para pausar el juego
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;  // Pausar el tiempo
        pauseMenuPanel.SetActive(true);  // Activar el men� de pausa
    }

    // M�todo para reanudar el juego
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  // Reanudar el tiempo
        pauseMenuPanel.SetActive(false);  // Desactivar el men� de pausa
    }

    // M�todo para abrir el men� de accesibilidad
    void OpenAccessibilityMenu()
    {
        pauseMenuPanel.SetActive(false);  // Desactivamos el men� de pausa
        accessibilityMenuPanel.SetActive(true);  // Activamos el men� de accesibilidad
    }

    // M�todo para salir (opcional)
    void ExitGame()
    {
        // Volver al men� principal o salir del juego
       // SceneManager.LoadScene("MainMenu");  // Esto depende de c�mo tengas organizada tu escena
       Application.Quit();
    }
}