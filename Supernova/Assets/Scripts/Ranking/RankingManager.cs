using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class RankingManager : MonoBehaviour
{
    public RankingData rankingData = new RankingData();
    public List<PlayerData> playerList = new List<PlayerData>();  // playerListを追加
    public TextMeshProUGUI rankingText;  // TextMeshProUGUIへの参照

    void Start()
    {
        // ClearRanking();
        // JSONからランキングデータをロード
        LoadJsonFromFile();

        // playerListにrankingDataのプレイヤーデータをコピー
        playerList = rankingData.players;

        // TextMeshProにランキングデータを表示
        DisplayRanking();

        // 10位以下を削除し、JSONを更新
        UpdateAndSaveRanking();
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
    public void UpdateAndSaveRanking()
    {
        // スコアで降順にソートし、10位以下を削除
        playerList = playerList
            .OrderByDescending(player => player.score)  // スコア順にソート
            .Take(10)  // 上位10件だけを取得
            .ToList();

        // rankingData.playersも更新
        rankingData.players = playerList;

        // JSONに変換して保存
        string json = JsonUtility.ToJson(rankingData, true);
        SaveJsonToFile(json);
    }
    public void ClearRanking()
    {
        // ランキングデータをクリア
        rankingData.players.Clear();
        playerList.Clear();

        // JSONに変換して保存
        string json = JsonUtility.ToJson(rankingData, true);
        SaveJsonToFile(json);

        // TextMeshProの表示もクリア
        rankingText.text = "";
    }
}