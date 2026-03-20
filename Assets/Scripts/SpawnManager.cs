using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> obstaclePrefab = new List<GameObject>(); 
    //public GameObject obstaclePrefab;
    public Vector3 spawnPos = new(25, 0, 0);

    
    public float startDelay = 2;
    public float repeatRate = 2;

    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Instantiate(obstaclePrefab, new Vector3(25, 0, 0), obstaclePrefab.transform.rotation);

        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);

        GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        int prefabIndex = Random.Range(0, obstaclePrefab.Count);
        Instantiate(obstaclePrefab[prefabIndex], spawnPos, obstaclePrefab[prefabIndex].transform.rotation);
    }
}
