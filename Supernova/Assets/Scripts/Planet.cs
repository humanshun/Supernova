using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PLANETS_TYPE
{
    sui = 1,
    ka,
    kin,
    chi,
    kai,
    ten,
    dou,
    moku,
    taiyou,
}

public class Planet : MonoBehaviour
{
    // 各Planetのプレハブが入った配列
    public GameObject[] planetPrefabs;
    public PLANETS_TYPE planetType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Planet planet))
        {
            // 同じenum同士が衝突した場合
            if (planet.planetType == planetType)
            {
                // 現在のplanetTypeの一つ上を取得
                PLANETS_TYPE nextPlanetType = GetNextPlanetType(planetType);

                // 新しいPlanetを生成
                CreateNewPlanet(nextPlanetType);

                // このオブジェクトを破壊
                Destroy(gameObject);
            }
        }
    }

    // 次のPLANETS_TYPEを取得するヘルパー関数
    private PLANETS_TYPE GetNextPlanetType(PLANETS_TYPE currentType)
    {
        int nextTypeIndex = (int)currentType + 1;

        // enumの範囲を超えないようにチェック
        if (nextTypeIndex > (int)PLANETS_TYPE.taiyou)
        {
            nextTypeIndex = (int)PLANETS_TYPE.taiyou; // taiyou以上は存在しないのでtaiyouに固定
        }

        return (PLANETS_TYPE)nextTypeIndex;
    }

    // 新しいPlanetオブジェクトを生成する関数
    private void CreateNewPlanet(PLANETS_TYPE planetType)
    {
        // プレハブが設定されているか、planetTypeの範囲内にあるかチェック
        int prefabIndex = (int)planetType - 1; // enumのインデックスに対応するため -1

        if (planetPrefabs != null && prefabIndex >= 0 && prefabIndex < planetPrefabs.Length)
        {
            GameObject newPlanet = Instantiate(planetPrefabs[prefabIndex], transform.position, Quaternion.identity);
            Planet newPlanetScript = newPlanet.GetComponent<Planet>();

            // 新しいPlanetにplanetTypeをセット
            if (newPlanetScript != null)
            {
                newPlanetScript.planetType = planetType;
            }
        }
        else
        {
            Debug.LogError("プレハブが設定されていないか、インデックスが範囲外です。");
        }
    }
}
