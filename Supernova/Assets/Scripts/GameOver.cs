using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private RankingManager rankingManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // 点滅を開始
            StartCoroutine(Blink(spriteRenderer, 10, 0.2f));
        }

        // Rigidbody2Dコンポーネントを取得
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 重力の影響を無効にする
            rb.gravityScale = 0;
            // 速度もゼロにする
            rb.velocity = Vector2.zero;
            // 回転を固定する
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }



        // Collider2Dを無効にする
        Collider2D collider = other.GetComponent<Collider2D>();

        if (collider != null)
        {
            collider.enabled = false;
        }

        // 2秒たってからシーン切り替え
        StartCoroutine(SceneChange());
    }

    private IEnumerator Blink(SpriteRenderer spriteRenderer, int blinkCount, float blinkInterval)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            // 表示を切り替える
            spriteRenderer.enabled = !spriteRenderer.enabled;
            // 指定した時間待機
            yield return new WaitForSeconds(blinkInterval);
        }
        // 最後に表示を有効にしておく
        spriteRenderer.enabled = true;
    }

    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(2);

        // プレイヤー名とスコアを取得し、ランキングに追加
        string playerName = GM.PlayerName; // 静的プロパティにアクセス
        rankingManager.rankingData.AddPlayer(playerName, GM.score); // GM.score にも静的にアクセス

        // JSONデータをファイルに保存
        string json = JsonUtility.ToJson(rankingManager.rankingData);
        rankingManager.SaveJsonToFile(json);

        // アウトゲームシーンに遷移
        SceneManager.LoadScene("GameOver");
    }

}
