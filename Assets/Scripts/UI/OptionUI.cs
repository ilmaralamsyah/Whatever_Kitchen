using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{

    public static OptionUI Instance { get; private set; }

    [SerializeField] private Button audioSettingButton;
    [SerializeField] private Button controllerSettingButton;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        Instance = this;

        audioSettingButton.onClick.AddListener(() =>
        {
            Hide();
            AudioSettingUI.Instance.Show();
        });

        controllerSettingButton.onClick.AddListener(() =>
        {
            ControllerSettingUI.Instance.Show();
            Hide();
        });

        backButton.onClick.AddListener(() =>
        {

            PauseGameUI.Instance.Show();
            Hide();
        });

    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;

        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        audioSettingButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
