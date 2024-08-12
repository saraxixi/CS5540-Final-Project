using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public GameObject settingPanel;
    public Button save;
    public Button load;
    private bool isOpen = false;

    void Start()
    {
        save.onClick.AddListener(SaveData);
        load.onClick.AddListener(LoadData);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenAndCloseUI();
        }
    }

    public void OpenAndCloseUI()
    {
        isOpen = !isOpen;
        settingPanel.SetActive(isOpen);
        settingPanel.SetActive(isOpen);
    }

    public void SaveData()
    { 
        SaveManager.Instance.SavePlayerData();
    }

    public void LoadData()
    { 
        SaveManager.Instance.LoadPlayerData();
    }
}
