using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 追加

public class GM : MonoBehaviour
{
    public static GM Instance { get; private set; } // シングルトンインスタンス
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        // 他のインスタンスが存在する場合はこのオブジェクトを破棄
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンが切り替わっても破棄しない
            SceneManager.sceneLoaded += OnSceneLoaded; // シーンがロードされたときのイベントを登録
        }
        else
        {
            Destroy(gameObject);
        }
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
        scoreText.text = score.ToString();
    }
}
