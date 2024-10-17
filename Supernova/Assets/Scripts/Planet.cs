using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLANETS_TYPE
{
    one = 1,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine,
}

public class Planet : MonoBehaviour
{
    public GameObject nextPlanet;
    public GameObject[] planetPrefabs;
    public PLANETS_TYPE planetType;

    private int[] planetScores = new int[]
    {
        0, // インデックス0は使用しない
        1, // one   のスコア
        2, // two   のスコア
        4, // three のスコア
        8, // four  のスコア
        16, // five  のスコア
        32, // six   のスコア
        64, // seven のスコア
        128, // eight のスコア
        256  // nine  のスコア
    };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Planet planet))
        {
            // 同じenum同士が衝突した場合
            if (planet.planetType == planetType)
            {
                // nine同士がぶつかった場合、デバッグログを出力
                if (planetType != PLANETS_TYPE.nine)
                {
                    AudioManager.instance.planetSound();
                }
                Destroy(gameObject);
                collision.gameObject.GetComponent<Planet>().nextPlanet = null;

                // 衝突した惑星の位置を使って中間地点を計算
                Vector2 midPoint = (transform.position + collision.transform.position) / 2;

                //衝突した惑星の速度を取得
                Vector3 velocity1 = GetComponent<Rigidbody2D>().velocity;
                Vector3 velocity2 = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

                //平均速度を計算
                Vector3 averageVelocity = (velocity1 + velocity2) / 2;

                // 次の惑星をインスタンス化する位置を中間地点に設定
                if (nextPlanet)
                {
                    GameObject newPlanet = Instantiate(nextPlanet, midPoint, transform.rotation);
                    // 新しくインスタンス化した惑星に速度を適用
                    newPlanet.GetComponent<Rigidbody2D>().velocity = averageVelocity;
                }

                // シングルトンのGameManagerを使ってスコアを加算
                GM.Instance.AddScore(planetScores[(int)planetType]);
            }
        }
    }
}
