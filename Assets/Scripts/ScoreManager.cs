using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    // Start is called before the first frame update
    public string playerName;
    public int score;
    public TMPro.TMP_InputField TMP_nameInputField;
    public Text viewBestScore;

    //public string playerName;
    public string bestPlayerName;
    public int bestScore;
        
    private void Awake()
    {
        LoadScore();
        viewBestScore.text = "Best Scorer : " + bestScore + " / " + bestPlayerName;

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetName()
    {
        this.playerName = TMP_nameInputField.text;
        Debug.Log(playerName);
    }

    [System.Serializable]
    public class PlayerData
    {
        public int score;
        public string playerName;
    }

    public void SaveScore(int score)
    {
        PlayerData data = new PlayerData
        {
            score = score,
            playerName = bestPlayerName
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            bestScore = data.score;
            bestPlayerName = data.playerName;
        }
    }
}
