using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤー名を取得し、スコアを保存
        string playerName = GM.Instance.PlayerName; // シングルトンからプレイヤー名を取得
        GM.Instance.SaveScore(playerName);

        // アウトゲームシーンに遷移
        SceneManager.LoadScene("GameOver");
    }
}
