using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    [SerializeField] private List<UiPanel> panels = new();
    [SerializeField] private CameraMovement cameraMovement;
    private bool cursorActive;
    private bool allPanelClosed;
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
}

[System.Serializable]
public class UiPanel
{
    public string Id;
    public GameObject Panel;
    public bool DefaultActive;
}