using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance { get; set; }

    public GameObject[] allObstaclePrefabs;
    public GameObject starPointPrefab;

    public Transform[] spawnPositions;
    public Transform pointPosition;

    [SerializeField] private int activePrefabs;
    [SerializeField] private int initialActiveIndex;

    public Transform player;
    public Camera mainCamera;

    private List<GameObject> activeObjectObstacle = new List<GameObject>();
    private float cameraHeight;
    private float obstacleSpawnDistance;
    private float starSpawnDistance;
    private Transform botPosition;
    private Transform topPosition;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        cameraHeight = mainCamera.orthographicSize * 2f;
        obstacleSpawnDistance = cameraHeight * 2f;
        starSpawnDistance = cameraHeight;

        activeObjectObstacle.Clear();

        for (int i = 0; i < initialActiveIndex && i < allObstaclePrefabs.Length; ++i)
            activeObjectObstacle.Add(allObstaclePrefabs[i]);

        activePrefabs = initialActiveIndex;

        ResetAndSpawnInitial();
    }

    void ResetAndSpawnInitial()
    {
        int prefabIndex = Random.Range(0, activeObjectObstacle.Count);
        Vector3 prefabPosition = spawnPositions[0].position;

        GameObject newObstacle = Instantiate(activeObjectObstacle[prefabIndex], prefabPosition, Quaternion.identity);
        botPosition = newObstacle.transform;

        prefabIndex = Random.Range(0, activeObjectObstacle.Count);
        prefabPosition.y += starSpawnDistance;

        newObstacle = Instantiate(activeObjectObstacle[prefabIndex], prefabPosition, Quaternion.identity);
        topPosition = newObstacle.transform;

        Instantiate(starPointPrefab, pointPosition.position, Quaternion.identity);
    }

    public void SpawnNextObstacle()
    {
        if (activeObjectObstacle.Count == 0) return;

        int prefabIndex = Random.Range(0, activeObjectObstacle.Count);
        Transform spawnPosition;

        switch (prefabIndex)
        {
            case 3:
                spawnPosition = spawnPositions[1];
                break;
            case 4:
                spawnPosition = spawnPositions[2];
                break;
            case 7:
                spawnPosition = spawnPositions[3];
                break;
            default:
                spawnPosition = spawnPositions[0];
                break;

        }

        GameObject prefab = activeObjectObstacle[prefabIndex];
        Vector3 prefabPosition = new Vector3(spawnPosition.position.x, botPosition.position.y + obstacleSpawnDistance, 0f);

        GameObject newObstacle = Instantiate(prefab, prefabPosition, Quaternion.identity);
        botPosition = topPosition;
        topPosition = newObstacle.transform;
    }

    public void SpawnNextStar(Vector3 oldStar)
    {
        Vector3 pos = new Vector3(oldStar.x, oldStar.y + starSpawnDistance, oldStar.z);

        Instantiate(starPointPrefab, pos, Quaternion.identity);
    }

    public void ExpandObstacle()
    {
        if (activePrefabs < allObstaclePrefabs.Length)
        {
            activeObjectObstacle.Add(allObstaclePrefabs[activePrefabs]);
            ++activePrefabs;
        }
    }
}
