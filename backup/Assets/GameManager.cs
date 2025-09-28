using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float HeatLevel { get; private set; } = 0f;
    public float BaseSpawnInterval = 5f;
    public float MinSpawnInterval = 1f;
    public float HeatDecayRate = 4f; // Heat removed per second


    private float spawnTimer = 0f;

    [SerializeField]
    private GameObject enemyPrefab;

    public Transform playerTransform;
    private Camera mainCamera;
    private void Awake() 
    {
        Debug.Log("GameManager Awake");
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroying duplicate GameManager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        mainCamera = Camera.main;
        spawnTimer = BaseSpawnInterval;
    }

    private void Update()
    {
        Debug.Log("GameManager Update: HeatLevel " + HeatLevel + ", spawnTimer " + spawnTimer);

        HeatLevel = Mathf.Max(0f, HeatLevel - HeatDecayRate * Time.deltaTime);
        float spawnInterval = Mathf.Lerp(BaseSpawnInterval, MinSpawnInterval, HeatLevel / 100f);
        spawnTimer -= Time.deltaTime;

        if (enemyPrefab == null) Debug.LogWarning("Enemy prefab is null!");
        if (playerTransform == null) Debug.LogWarning("Player transform is null!");

        if (enemyPrefab != null && playerTransform != null && spawnTimer <= 0f)
        {
            Debug.Log("Spawning enemy");
            SpawnEnemyAtScreenEdge();
            spawnTimer = Mathf.Max(spawnInterval, 5f);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("GameManager destroyed");
    }



    // Add heat from entities
    public void AddHeat(float amount)
    {
        HeatLevel += amount;
        HeatLevel = Mathf.Clamp(HeatLevel, 0f, 100f);
    }

    // Spawns an enemy at a random edge of the player's screen
    private void SpawnEnemyAtScreenEdge()
    {
        if (enemyPrefab == null || playerTransform == null) return;

        Vector3 spawnViewportPos = Vector3.zero;
        int edgeIndex = Random.Range(0, 4);
        switch (edgeIndex)
        {
            case 0: spawnViewportPos = new Vector3(0f, Random.Range(0f, 1f), 10f); break; // Left
            case 1: spawnViewportPos = new Vector3(1f, Random.Range(0f, 1f), 10f); break; // Right
            case 2: spawnViewportPos = new Vector3(Random.Range(0f, 1f), 1f, 10f); break; // Top
            case 3: spawnViewportPos = new Vector3(Random.Range(0f, 1f), 0f, 10f); break; // Bottom
        }
        Vector3 spawnWorldPos = mainCamera.ViewportToWorldPoint(spawnViewportPos);
        spawnWorldPos.z = 0f; // Ensures spawn on correct plane for 2D
        Instantiate(enemyPrefab, spawnWorldPos, Quaternion.identity);
    }
}
