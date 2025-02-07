using System.Collections.Generic;
using UnityEngine;

public class WasteSpawner : MonoBehaviour
{
    [SerializeField] private Trash[] wastePrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 1f;

    private List<Trash> wasteWillSpawn = new List<Trash>();
    private float timer;

    private void Update()
    {
        wasteWillSpawn.Clear();
        foreach (Trash waste in wastePrefabs)
        {
            if (GlobalVariables.score >= waste.scoreToSpawn)
            {
                wasteWillSpawn.Add(waste);
            }
        }

        SpawnWaste();
    }

    private void SpawnWaste()
    {
        if (wasteWillSpawn.Count == 0) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            int randomIndex = Random.Range(0, wasteWillSpawn.Count);
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            Trash waste = Instantiate(wasteWillSpawn[randomIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            waste.transform.SetParent(spawnPoints[randomSpawnPointIndex]);
        }
    }
}