using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    [SerializeField] private List<UiPanel> panels = new();
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Image sprintSlider;
    private bool cursorActive;
    private bool allPanelClosed;
    private Color sprintSliderColor;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (UiPanel panel in panels)
            if (!panel.DefaultActive)
                panel.Panel.gameObject.SetActive(false);
            else
                panel.Panel.gameObject.SetActive(true);

        sprintSliderColor = sprintSlider.color;

        SetCursorActivation(true);
        OpenPanel("MainMenu");
    }

    public void OpenPanel(string id)
    {
        foreach (UiPanel panel in panels)
            if (panel.Id == id)
                panel.Panel.SetActive(true);
    }

    public void ClosePanel(string id)
    {
        foreach (UiPanel panel in panels)
            if (panel.Id == id)
                panel.Panel.SetActive(false);


        allPanelClosed = true;
        foreach (UiPanel panel in panels)
            if (panel.Panel.activeSelf && !panel.DefaultActive)
                allPanelClosed = false;

        if (allPanelClosed)
            SetCursorActivation(false);
    }

    public void SetCursorActivation(bool active)
    {
        if (!active)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cameraMovement.SetCameraMovement(true);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cameraMovement.SetCameraMovement(false);
        }
    }

    public void UpdateSprintTime(float value)
    {
        sprintSlider.fillAmount = value / 100;
        if (sprintSlider.fillAmount < 0.5f && sprintSlider.fillAmount > 0.2f)
            sprintSlider.DOColor(Color.yellow, 0.5f);
        else if (sprintSlider.fillAmount < 0.2f)
            sprintSlider.DOColor(Color.red, 0.5f);
        else
            sprintSlider.color = sprintSliderColor;
    }
}

[System.Serializable]
public class UiPanel
{
    public string Id;
    public GameObject Panel;
    public bool DefaultActive;
}