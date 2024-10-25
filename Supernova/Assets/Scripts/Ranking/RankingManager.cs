using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;  // Linqの拡張メソッドを使用するために追加

public class RankingManager : MonoBehaviour
{
    public RankingData rankingData = new RankingData();
    public List<PlayerData> playerList = new List<PlayerData>();  // playerListを追加
    public TextMeshProUGUI rankingText;  // TextMeshProUGUIへの参照

    void Start()
    {
        // JSONからランキングデータをロード
        LoadJsonFromFile();

        // もしロードしたデータが空であればサンプルデータを追加
        if (rankingData.players.Count == 0)
        {
            // サンプルデータの追加
            AddSampleData();
        }

        // playerListにrankingDataのプレイヤーデータをコピー
        playerList = rankingData.players;

        // TextMeshProにランキングデータを表示
        DisplayRanking();
    }

    public void DisplayRanking()
    {
        // スコアで降順にソート
        List<PlayerData> sortedPlayers = playerList
            .OrderByDescending(player => player.score)  // Linqの拡張メソッド
            .Take(10)  // 上位10位まで取得
            .ToList();

        string displayText = "";

        // 上位10位を表示
        foreach (var player in sortedPlayers)
        {
            displayText += $"{player.playerName}: {player.score}\n";
        }

        // TextMeshProに表示
        rankingText.text = displayText;
    }

    public void SaveJsonToFile(string json)
    {
        // JSONファイルに保存する処理 (パスは適宜変更)
        System.IO.File.WriteAllText(Application.dataPath + "/rankingData.json", json);
    }

    void LoadJsonFromFile()
    {
        string path = Application.dataPath + "/rankingData.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            rankingData = JsonUtility.FromJson<RankingData>(json);
        }
        else
        {
            Debug.LogWarning("Ranking data file not found.");
        }
    }

    // サンプルデータを追加するメソッド
    void AddSampleData()
    {
        rankingData.AddPlayer("Alice", 100);
        rankingData.AddPlayer("Bob", 150);
        rankingData.AddPlayer("Charlie", 200);
        rankingData.AddPlayer("Dave", 90);
        rankingData.AddPlayer("Eve", 120);
        rankingData.AddPlayer("Frank", 180);
        rankingData.AddPlayer("Grace", 160);
        rankingData.AddPlayer("Hank", 130);
        rankingData.AddPlayer("Ivy", 110);
        rankingData.AddPlayer("Jack", 170);
        rankingData.AddPlayer("Kenny", 140);
    }
}