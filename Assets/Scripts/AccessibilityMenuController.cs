using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AccessibilityMenuController : MonoBehaviour
{
    [Header("UI References")]
    public Toggle shakeToggle;
    public TMP_Dropdown fontDropdown;
    public TMP_Dropdown promptDurationDropdown;
    public Button backButton;

    [Header("Prompt Display Settings")]
    // Aquí puedes conectar con tu sistema de prompts para actualizar la duración y fuente
    public IABriefing iaBriefing;  // Referencia al script que maneja los prompts
    public TMP_Text promptTextSample; // Texto de ejemplo para cambiar la fuente en el menú

    // Valores de duración en segundos para los prompts según selección
    private readonly int[] promptDurations = { 5, 7, 10 };

    public GameObject pauseMenuPanel;
    public GameObject accessibilityMenuPanel;

    void Start()
    {
        // Configuramos las opciones UI con valores por defecto
        shakeToggle.isOn = PlayerPrefs.GetInt("ShakeEnabled", 1) == 1;

        fontDropdown.ClearOptions();
        fontDropdown.AddOptions(new System.Collections.Generic.List<string> { "Futurista", "Normal" });
        fontDropdown.value = PlayerPrefs.GetInt("FontType", 0);

        promptDurationDropdown.ClearOptions();
        promptDurationDropdown.AddOptions(new System.Collections.Generic.List<string> { "Rápido", "Medio", "Lento" });
        promptDurationDropdown.value = PlayerPrefs.GetInt("PromptDuration", 1);

        // Asignar listeners
        shakeToggle.onValueChanged.AddListener(OnShakeToggleChanged);
        fontDropdown.onValueChanged.AddListener(OnFontDropdownChanged);
        promptDurationDropdown.onValueChanged.AddListener(OnPromptDurationChanged);
        backButton.onClick.AddListener(OnBackButtonClicked);

        // Aplicar valores iniciales
        ApplyShakeSetting(shakeToggle.isOn);
        ApplyFontSetting(fontDropdown.value);
        ApplyPromptDuration(promptDurationDropdown.value);
    }

    private void OnShakeToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("ShakeEnabled", isOn ? 1 : 0);
        ApplyShakeSetting(isOn);
    }

    private void ApplyShakeSetting(bool enabled)
    {
        // Aquí pondrías la lógica para activar/desactivar el shake globalmente
        Debug.Log("Shake Enabled: " + enabled);
        // Por ejemplo, un GameManager podría leer PlayerPrefs y controlar el shake
    }

    private void OnFontDropdownChanged(int index)
    {
        PlayerPrefs.SetInt("FontType", index);
        ApplyFontSetting(index);
    }

    private void ApplyFontSetting(int index)
    {
        if (promptTextSample == null) return;

        if (index == 0)
        {
            // Fuente futurista (ejemplo: Orbitron, asegurate que esté en Resources o asignada)
            TMP_FontAsset futuristFont = Resources.Load<TMP_FontAsset>("Audiowide-Regular SDF");
            if (futuristFont != null)
                promptTextSample.font = futuristFont;
        }
        else
        {
            // Fuente normal (ejemplo Arial)
            TMP_FontAsset normalFont = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
            if (normalFont != null)
                promptTextSample.font = normalFont;
        }

        // También deberías actualizar el font en el IABriefing si usas ese sistema
        if (iaBriefing != null)
            iaBriefing.SetFont(promptTextSample.font);
    }

    private void OnPromptDurationChanged(int index)
    {
        PlayerPrefs.SetInt("PromptDuration", index);
        ApplyPromptDuration(index);
    }

    private void ApplyPromptDuration(int index)
    {
        if (iaBriefing != null)
            iaBriefing.SetDisplayDurationFromIndex(promptDurations[index]);
    }

    private void OnBackButtonClicked()
    {
        pauseMenuPanel.SetActive(true);  
        accessibilityMenuPanel.SetActive(false);  
    }
}