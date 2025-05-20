using UnityEngine;
using UnityEngine.SceneManagement;  // Para cargar el menú principal (opcional)
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
        // Asegúrate de que el menú de pausa y el de accesibilidad estén desactivados al principio
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
                ResumeGame();  // Reanudar juego si ya está pausado
            else
                PauseGame();  // Pausar juego si no lo está
        }
    }

    // Método para pausar el juego
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;  // Pausar el tiempo
        pauseMenuPanel.SetActive(true);  // Activar el menú de pausa
    }

    // Método para reanudar el juego
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  // Reanudar el tiempo
        pauseMenuPanel.SetActive(false);  // Desactivar el menú de pausa
    }

    // Método para abrir el menú de accesibilidad
    void OpenAccessibilityMenu()
    {
        pauseMenuPanel.SetActive(false);  // Desactivamos el menú de pausa
        accessibilityMenuPanel.SetActive(true);  // Activamos el menú de accesibilidad
    }

    // Método para salir (opcional)
    void ExitGame()
    {
        // Volver al menú principal o salir del juego
       // SceneManager.LoadScene("MainMenu");  // Esto depende de cómo tengas organizada tu escena
       Application.Quit();
    }
}