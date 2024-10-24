using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private DataManager dataManager;
    public int playerScore = 0; // プレイヤーのスコアを管理する変数

    private void Start()
    {
        // DataManagerコンポーネントを取得
        dataManager = FindObjectOfType<DataManager>();

        // ここでプレイヤーのスコアを取得する処理を追加
        // 例: playerScore = GM.Instance.CurrentScore;
    }

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

        // スコアを保存
        SavePlayerScore();

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

    private void SavePlayerScore()
    {
        if (dataManager != null)
        {
            // プレイヤー名とスコアを取得して保存
            string playerName = dataManager.data.userNames[0]; // デフォルトのプレイヤー名を取得
            dataManager.SetPlayerName(playerName); // 必要ならばプレイヤー名を更新

            // スコアをランキングに追加
            dataManager.data.rank[0] = playerScore; // 例として0番目にスコアを保存
            dataManager.Save(dataManager.data); // JSONに保存
        }
    }

    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(2);

        // アウトゲームシーンに遷移
        SceneManager.LoadScene("GameOver");
    }
}
