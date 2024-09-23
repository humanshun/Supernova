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
    public GameObject nextPlanet;
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
                // 相手のPlanetのみを破壊
                Destroy(gameObject);

                collision.gameObject.GetComponent<Planet>().nextPlanet = null;

                if (nextPlanet)
                {
                    Instantiate(nextPlanet, transform.position, transform.rotation);
                }
            }
        }
    }
}
