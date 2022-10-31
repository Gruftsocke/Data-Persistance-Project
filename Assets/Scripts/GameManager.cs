using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class UserData
    {
        public string username = string.Empty;
        public int score = 0;
    }

    [Serializable]
    class SaveGameData
    {
        public string username = string.Empty;
        public List<UserData> data = new List<UserData>();
    }

    public static GameManager Current { get; private set; }

    private SaveGameData saveGame = new SaveGameData();

    public string Username { get => saveGame.username; set => saveGame.username = value; }
    public List<UserData> HighscoreList { get => saveGame.data; set => saveGame.data = value; }

    private void Awake()
    {
        if (Current)
        {
            Destroy(gameObject);
            return;
        }

        Current = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savegame.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            
            saveGame = JsonUtility.FromJson<SaveGameData>(json);

        }
    }

    public void SaveData()
    {
        if (saveGame.username == string.Empty)
            return;

        string json = JsonUtility.ToJson(saveGame);
        File.WriteAllText(Application.persistentDataPath + "/savegame.json", json);
    }

    public void AddNewHighscore(string user, int score)
    {
        saveGame.data.Add(new UserData() { username = user, score = score }) ;
    }
}
