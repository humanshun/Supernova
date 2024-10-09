using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPlanet : MonoBehaviour
{
    [SerializeField] private GameObject[] planetPrefabs;
    [SerializeField] private Transform spawnPoint;
    private GameObject nextPlanet;
    public int nextPlanetNumber;
    
    public void NextPlanetInstance()
    {
        if (nextPlanet != null)
        {
            Destroy(nextPlanet);
        }
        nextPlanetNumber = Random.Range(0, planetPrefabs.Length);
        nextPlanet = Instantiate(planetPrefabs[nextPlanetNumber], spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = nextPlanet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}
