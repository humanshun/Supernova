using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPlanet : MonoBehaviour
{
    [SerializeField] private GameObject[] planetPrefabs;
    [SerializeField] private Transform spawnPoint; // ここはCanvasの子オブジェクトにする
    private GameObject nextPlanet;
    public int nextPlanetNumber;
    
    public void NextPlanetInstance()
    {
        if (nextPlanet != null)
        {
            Destroy(nextPlanet);
        }
        nextPlanetNumber = Random.Range(0, planetPrefabs.Length);
        nextPlanet = Instantiate(planetPrefabs[nextPlanetNumber], spawnPoint.position, Quaternion.identity, spawnPoint);
        
        // オプションで、座標を調整する（Canvas上での相対位置）
        nextPlanet.transform.localPosition = Vector3.zero; // これにより、spawnPointの位置に合わせる
        
        Rigidbody2D rb = nextPlanet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true; // 惑星が動かないようにする
        }
    }
}
