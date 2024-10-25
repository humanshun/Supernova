using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class GM : MonoBehaviour
{
    // シングルトンインスタンス
    public static GM Instance { get; private set; }

    // 現在のプレイヤーのスコアを保持
    public static int score = 0;
    public static string PlayerName;

    // スコアを表示するためのUI (TextMeshPro)
    [SerializeField] private TextMeshProUGUI scoreText;

    // プレイヤー名とスコアを保持するランキングリスト
    private List<(string playerName, int playerScore)> scores = new List<(string, int)>();

    private void Awake()
    {
        // シングルトンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン遷移でオブジェクトが破壊されないようにする
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合、重複を避けるために破壊
        }
    }

    private void Start()
    {
        // シーンがロードされたときのイベントを登録
        SceneManager.sceneLoaded += OnSceneLoaded; 
        LoadScores(); // 保存されているスコアを読み込む
    }

    private void OnDestroy()
    {
        // イベントの登録を解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // シーンが切り替わったときにスコアテキストを探す
        Restart();
    }

    public void Restart()
    {
        // "Score"オブジェクトを探してスコアテキストを取得
        GameObject scoreObject = GameObject.Find("Score");

        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
            UpdateScoreText(); // スコアテキストを更新
        }
    }

    // スコアを加算するメソッド
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // スコアを文字列に変換して表示
        }
    }

    // プレイヤーのスコアを保存するメソッド
    public void SaveScore(string playerName)
    {
        // 現在のプレイヤー名とスコアをランキングリストに追加
        scores.Add((playerName, score));

        // スコアを降順でソートして、上位10件のみを保持
        scores = scores.OrderByDescending(s => s.playerScore).Take(10).ToList();

        // スコアデータを文字列形式に変換し、PlayerPrefsに保存
        string scoreData = string.Join(",", scores.Select(s => $"{s.playerName}:{s.playerScore}"));
        PlayerPrefs.SetString("Scores", scoreData);
        PlayerPrefs.Save();
    }

    // 保存されたスコアを読み込むメソッド
    private void LoadScores()
    {
        // PlayerPrefsから保存されているスコアデータを取得
        string savedScores = PlayerPrefs.GetString("Scores", "");
        if (!string.IsNullOrEmpty(savedScores))
        {
            // 文字列を分割して、スコアリストに変換
            scores = savedScores.Split(',')
            .Select(s => s.Split(':')) // "プレイヤー名:スコア"形式を分割
            .Where(parts => parts.Length == 2 && int.TryParse(parts[1], out _)) // スコア部分が整数に変換できるかチェック
            .Select(parts => (parts[0], int.Parse(parts[1]))) // プレイヤー名とスコアをタプルに変換
            .ToList(); // リストに変換
        }
    }

    public List<(string playerName, int playerScore)> GetTopScores()
    {
        // スコアを降順でソートし、リストを返す
        return scores.OrderByDescending(s => s.playerScore).ToList();
    }
}
