using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] public GameObject[] obstaclePrefabs;
    [SerializeField] public Transform obstacleParent;
    public float obstaclesSpawnTime = 2f;
    [Range(0, 1)] public float obstacleSpawnTimeFactor = .1f;
    public float obstacleSpeed = 1f;
    [Range(0, 1)] public float obstacleSpeedFactor = .2f;

    public float _obstaclesSpawnTime;
    public float _obstacleSpeed;
    public float timeAlive;

    public float timeUntilObstacleSpawn;

    private void Start()
    {
        GameManager.Instance.onGameOver.AddListener(ClearObstacles);
        GameManager.Instance.onPlay.AddListener(ResetParameters);
    }

    void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            timeAlive += Time.deltaTime;
            SpawnLoop();
        }
    }

    void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if(timeUntilObstacleSpawn >= _obstaclesSpawnTime)
        {
            Spawn();

            CalculateParameters();

            timeUntilObstacleSpawn = 0;
        }
    }

    private void ClearObstacles()
    {
        foreach(Transform child in obstacleParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void ClearObstaclesInEditMode()
    {
        for (int i = obstacleParent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(obstacleParent.GetChild(i).gameObject);
        }
    }

    public void ResetParameters()
    {
        timeAlive = 1f;
        _obstacleSpeed = obstacleSpeed;
        _obstaclesSpawnTime = obstaclesSpawnTime;
    }

    public void CalculateParameters()
    {
        _obstaclesSpawnTime = obstaclesSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        _obstacleSpeed = obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        spawnedObstacle.transform.parent = obstacleParent;
        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = Vector2.left * _obstacleSpeed;
    }
}
