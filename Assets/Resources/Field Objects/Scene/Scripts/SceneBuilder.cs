using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBuilder : MonoBehaviour
{
    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<SpawnPoint>() != null)
                _spawnPoints.Add(transform.GetChild(i).GetComponent<SpawnPoint>());
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    public List<Enemy> Spawn(int currentWave)
    {
        List<Enemy> enemiesSpawned = new List<Enemy>();

        foreach (var spawnPoint in _spawnPoints)
        {
            // Checa se não é nulo, e então adiciona
            var enemy = spawnPoint.Spawn(currentWave);

            if (enemy != null)
                enemiesSpawned.Add(enemy);
            else
                Destroy(enemy);
        }

        return enemiesSpawned;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
