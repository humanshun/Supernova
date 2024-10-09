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
        10, // one   のスコア
        20, // two   のスコア
        30, // three のスコア
        40, // four  のスコア
        50, // five  のスコア
        60, // six   のスコア
        70, // seven のスコア
        80, // eight のスコア
        90  // nine  のスコア
    };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Planet planet))
        {
            // 同じenum同士が衝突した場合
            if (planet.planetType == planetType)
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<Planet>().nextPlanet = null;

                if (nextPlanet)
                {
                    Instantiate(nextPlanet, transform.position, transform.rotation);
                }

                // シングルトンのGameManagerを使ってスコアを加算
                GM.Instance.AddScore(planetScores[(int)planetType]);
            }
        }
    }
}
