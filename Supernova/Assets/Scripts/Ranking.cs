using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    public TextMeshProUGUI rankingNameText;
    public TextMeshProUGUI rankingScoreText;
    void Start()
    {
        DisplayScores();
    }
    void DisplayScores()
    {
        // GMからランキングデータを取得
        var topScores = GM.Instance.GetTopScores();
        rankingNameText.text = string.Join("\n", topScores.Select(s => $"{s.playerName}"));
        rankingScoreText.text = string.Join("\n", topScores.Select(s => $"{s.playerScore}"));
        // Debug.Log(GM.Instance.PlayerName);
    }
    public void BackToTitle()
    {
        // タイトル画面に戻る
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
