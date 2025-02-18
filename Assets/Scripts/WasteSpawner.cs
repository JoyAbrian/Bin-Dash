using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteSpawner : MonoBehaviour
{
    [SerializeField] private Trash[] wastePrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float baseSpawnInterval = 1f;
    [SerializeField] private float minSpawnInterval = 0.2f;
    [SerializeField] private float initialSpawnDelay = 2f;

    private List<Trash> wasteWillSpawn = new List<Trash>();
    private float timer;
    private float currentSpawnInterval;
    private bool canSpawn = false;

    private void Start()
    {
        StartCoroutine(StartSpawningAfterDelay());
    }

    private IEnumerator StartSpawningAfterDelay()
    {
        yield return new WaitForSeconds(initialSpawnDelay);
        canSpawn = true;
    }

    private void Update()
    {
        if (!canSpawn) return;

        AddNewWaste();
        currentSpawnInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval - (GlobalVariables.score * 0.0001f));
        SpawnWaste();
    }

    private void AddNewWaste()
    {
        wasteWillSpawn.Clear();
        foreach (Trash waste in wastePrefabs)
        {
            if (GlobalVariables.score >= waste.scoreToSpawn)
            {
                wasteWillSpawn.Add(waste);
            }
        }
    }

    private void SpawnWaste()
    {
        if (wasteWillSpawn.Count == 0 || spawnPoints.Length < 2) return;

        timer += Time.deltaTime;
        if (timer >= currentSpawnInterval)
        {
            timer = 0f;
            int randomIndex = Random.Range(0, wasteWillSpawn.Count);

            Vector3 randomPosition = Vector3.Lerp(spawnPoints[0].position, spawnPoints[1].position, Random.Range(0f, 1f));
            randomPosition.z = 0f;

            Trash waste = Instantiate(wasteWillSpawn[randomIndex], randomPosition, Quaternion.identity);
            waste.transform.SetParent(transform);
        }
    }
}