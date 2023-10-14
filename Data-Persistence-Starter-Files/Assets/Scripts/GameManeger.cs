using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static GameManeger Instance;
    public int matchScore;
    public Player playerName = new Player();
    public ScoreTable scoreTable = new ScoreTable();
    public Score bestScore = new Score();
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();

    }


    public void SaveName()
    {
        Player player = new Player();
        player.Name = playerName.Name;

        SaveFile(player, "/plyerFile.json");

    }
    public void LoadName()
    {
        Player player = new Player();
        player = LoadFile<Player>("/plyerFile.json");
    }
    
    public void SaveScore()
    {
        scoreTable = LoadFile<ScoreTable>("/scoreFile.json");
        Score score = new Score();
        score.player = playerName;
        score.points = matchScore;

        if(scoreTable == null)
        {
            scoreTable = new ScoreTable();
            List<Score> listScore = new List<Score>();
            listScore.Add(score);
            scoreTable.scorePlayer = listScore;
        }
        else
        {
            scoreTable.scorePlayer.Add(score);
        }      


        SaveFile(scoreTable, "/scoreFile.json");
    }
    public void LoadScore()
    {
        scoreTable = LoadFile<ScoreTable>("/scoreFile.json");

    }
    private void SaveFile(object obj, string file)
    {
        string json = JsonUtility.ToJson(obj);

        File.WriteAllText(Application.persistentDataPath + file, json);
    }

    
    private T LoadFile<T>(string file)
    {
        string path = Application.persistentDataPath + file;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            
            return JsonUtility.FromJson<T>(json);
        }
        return default(T);
    }



    public void FindBestScore()
    {       
        if(scoreTable != null)
        {
            foreach (Score score in scoreTable.scorePlayer)
            {
                bestScore = score.points > bestScore.points ? score : bestScore;
            }        

        }
    }

    public string Top20()
    {
        string top20 = "Top 20";
        int max = scoreTable.scorePlayer.Count > 20 ? 20 : scoreTable.scorePlayer.Count;
        List<Score> scoreOrdenado = scoreTable.scorePlayer.OrderByDescending(x => x.points).ToList();
        for (int i = 0; i < max; i++)
        {
            top20 += $"\n{scoreOrdenado[i].player.Name}: {scoreOrdenado[i].points} pontos";
        }
        
        return top20;
    }

    [System.Serializable]
    public class Player
    {
        public string Name;
    }

    [System.Serializable]
    public class Score
    {
        public Player player;
        public int points;
    }
    [System.Serializable]
    public class ScoreTable
    {
        public List<Score> scorePlayer ;
    }


}
