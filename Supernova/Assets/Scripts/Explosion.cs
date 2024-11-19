using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private List<GameObject> planetObjects = new List<GameObject>();

    void Start()
    {
        // Planetタグのオブジェクトをリストに追加
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject planet in planets)
        {
            planetObjects.Add(planet);
        }

        // Planetオブジェクトのタイプを検索しスコアを加算
        AddScoreFromPlanets();

        // リスト内のPlanetオブジェクトを破壊
        DestroyPlanetsInList();

        // 2秒後にこのExplosionゲームオブジェクトを削除する
        Destroy(gameObject, 2f);

        // 爆発音を再生
        AudioManager.instance.explosionSound();
    }

    private void AddScoreFromPlanets()
    {
        foreach (GameObject planet in planetObjects)
        {
            if (planet.TryGetComponent<Planet>(out Planet planetComponent))
            {
                // Planetオブジェクトのタイプに応じてスコアを加算
                GM.Instance.AddScore(planetComponent.planetType == PLANETS_TYPE.nine ? 256 : (int)planetComponent.planetType);
            }
        }
    }

    private void DestroyPlanetsInList()
    {
        foreach (GameObject planet in planetObjects)
        {
            if (planet != null)
            {
                Destroy(planet);
            }
        }
        // リストをクリア
        planetObjects.Clear();
    }
}
