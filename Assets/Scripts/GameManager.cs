using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public Transform player;
    public GameObject missilePrefab;

    [Header("Game State")]
    public int playerHP = 5;
    
    [Header("Spawning & Difficulty")]
    public float spawnInterval = 3f;
    public float spawnDistanceAhead = 12f; // Distance above the player (off-screen)
    
    private int missilesToSpawn = 1;
    private float survivalTimer = 0f;
    private float spawnTimer = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (player == null) return;

        survivalTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        // Difficulty Scaling: Add a missile every 10 seconds
        if (survivalTimer >= 10f)
        {
            missilesToSpawn++;
            survivalTimer = 0f;
            Debug.Log("Difficulty Increased! Missiles per wave: " + missilesToSpawn);
        }

        // Spawner loop
        if (spawnTimer >= spawnInterval)
        {
            SpawnMissiles();
            spawnTimer = 0f;
        }
    }

    void SpawnMissiles()
    {
        for (int i = 0; i < missilesToSpawn; i++)
        {
            // Calculate a point ahead of the player, randomized slightly on the X axis
            Vector3 spawnPos = player.position + new Vector3(Random.Range(-5f, 5f), spawnDistanceAhead, 0f);
            
            GameObject missile = Instantiate(missilePrefab, spawnPos, Quaternion.identity);
            
            // Assign the player target to the newly spawned missile
            missile.GetComponent<HomingMissile>().target = player;
        }
    }

    public void TakeDamage()
    {
        playerHP--;
        Debug.Log("Player Hit! HP remaining: " + playerHP);

        if (playerHP <= 0)
        {
            // Restart the game by reloading the active scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}