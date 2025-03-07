using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnRadius = 0.5f; // Raio para verificar colisões

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 spawnPosition;
            int attempts = 0;
            do
            {
                spawnPosition = GetRandomPositionInView(mainCamera);
                attempts++;
            } while (Physics2D.OverlapCircle(spawnPosition, spawnRadius) != null && attempts < 10);

            if (attempts < 10)
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Não foi possível encontrar uma posição de spawn válida para o inimigo.");
            }
        }
        else
        {
            Debug.LogError("Main camera não foi encontrada");
        }
    }

    private Vector3 GetRandomPositionInView(Camera camera)
    {
        float x = Random.Range(0f, 1f);
        float y = Random.Range(0f, 1f);
        Vector3 randomViewportPoint = new Vector3(x, y, camera.nearClipPlane);
        return camera.ViewportToWorldPoint(randomViewportPoint);
    }
}
