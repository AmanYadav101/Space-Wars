using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCluster : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int enemyCount = 10;
    public float moveDuration = 2f;
    public float formationDuration = 2f;
    public float radius = 2f;
    PolygonCollider2D polygonCollider2D;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 centerPosition;

    // Start is called before the first frame update
    void Start()
    {
        polygonCollider2D = enemyPrefab.GetComponent<PolygonCollider2D>();
        polygonCollider2D.isTrigger = true;

        centerPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        StartCoroutine(SpawnAndAnimateEnemies());
    }

    private void Awake()
    {
    }

    IEnumerator SpawnAndAnimateEnemies()
    {
        // Spawn enemies at the top of the viewport
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.1f, Camera.main.nearClipPlane));
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemies.Add(enemy);
        }

        // Move all enemies to the center
        foreach (GameObject enemy in enemies)
        {
            enemy.transform.DOMove(centerPosition, moveDuration);
        }

        yield return new WaitForSeconds(moveDuration);

        // Form a circular shape
        for (int i = 0; i < enemies.Count; i++)
        {
            float angle = i * Mathf.PI * 2 / enemies.Count;
            Vector3 targetPosition = centerPosition + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            enemies[i].transform.DOMove(targetPosition, formationDuration);

        }

        yield return new WaitForSeconds(formationDuration);
        // Start shooting after formation is complete
        foreach (GameObject enemy in enemies)
        {

            enemy.GetComponent<Enemy_Manager>().StartShooting();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
