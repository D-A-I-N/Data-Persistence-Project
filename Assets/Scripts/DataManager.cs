using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int HighScore;
    public string HighScorePlayerName;
    public string PlayerName;

    [SerializeField] private InputField playerNameInputField;

    private void Awake()
    {
        Instance = this;
        playerNameInputField = GameObject.FindObjectOfType<InputField>();
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        PlayerName = playerNameInputField.text;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public int HighScore;
        public string HighScorePlayerName;
    }

    public void Save()
    {
        SaveData data = new SaveData();

        data.HighScore = HighScore;
        data.HighScorePlayerName = HighScorePlayerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighScore = data.HighScore;
            HighScorePlayerName = data.HighScorePlayerName;
        }
    }   
}
