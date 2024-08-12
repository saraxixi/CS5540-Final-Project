using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : SingletonMono<SaveManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void SavePlayerData()
    {
        Save(GameManager.Instance.playerState.characterData, GameManager.Instance.playerState.characterData.name);
    }

    public void LoadPlayerData()
    {
        Load(GameManager.Instance.playerState.characterData, GameManager.Instance.playerState.characterData.name);
    }

    public void Save(Object data, string key)
    {
        var jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }

    public void Load(Object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        { 
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        }
    }
}
