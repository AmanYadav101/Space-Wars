using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;


public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    private bool isTripleShot;

    private GameObject projectileClone;
    [SerializeField] private float shootCooldown = .5f;
    private float lastShotTime;
    public GameObject enemyPrefab;
    private GameObject enemyClone;
    public Camera mainCamera;
    public int max_Count = 10;
    float spawnTime = 5f;


    public GameObject ToptoBottomPrefab;
    public GameObject RighttoLeftPrefab;
    public GameObject L2RLoopPrefab;
    public GameObject NewLefttoRight;
    public GameObject boss1Prefab;
    public GameObject boss2Prefab;
    float randomFloatTime;

    //Enemy Cluster
    public int enemyCount = 10;
    public float moveDuration = 2f;
    public float formationDuration = 2f;
    public float radius = 2f;
    PolygonCollider2D polygonCollider2D;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 centerPosition;
    private Vector3 centerToCenterPostion;


    
    private void Start()
    {
        lastShotTime = -shootCooldown; //lastShotTime = -shootCooldown so that the player is able to shoot the projectile at 0 secs.
                                       //if lastShotTime is initizlized with 0, then we wont be able to shoot at the start of the game. We will have to wait for 1 sec before shooting.

        isTripleShot = false;// false from start
        mainCamera = Camera.main;
        polygonCollider2D = enemyPrefab.GetComponent<PolygonCollider2D>();
        if (gameObject.tag == "LefttoRight")
        {
            polygonCollider2D.isTrigger = true;
        }
        centerPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        centerToCenterPostion = Camera.main.ViewportToWorldPoint(new Vector3(0.5f,.7f,Camera.main.nearClipPlane));
        //StartCoroutine(SpawnAndAnimateEnemies());
        //StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnSequence());
    }


    void Update()
    {

        FireProjectile();
    }

    void FireProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= lastShotTime + shootCooldown && isTripleShot)//Time.time is how much time has been elapsed till the start of the game.
                                                                                                          //Fires 3 Projectiles if isTripleShot is set to true. 
        {
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0), player.transform.rotation);
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x + 0.50f, player.transform.position.y + .25f, 0), player.transform.rotation);
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x - 0.50f, player.transform.position.y + .25f, 0), player.transform.rotation);
            lastShotTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= lastShotTime + shootCooldown)
        {
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0), player.transform.rotation);

            lastShotTime = Time.time;

        }
    }
    public bool GetIsTripleShot()
    {
        return isTripleShot;
    }
    public void SetIsTripleShot(bool istriple)
    {
        isTripleShot = istriple;
    }

    /*   IEnumerator SpawnEnemies()
       {
           for (int i = 0; i < max_Count; i++)
           {
               yield return StartCoroutine(waitTime());
           }
       }
       IEnumerator waitTime()
       {
           yield return new WaitForSeconds(1.5f);
           Vector3 spawnPosition = GetRandomTopPosition();
           enemyClone = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
       }*/

    IEnumerator SpawnSequence()
    {
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return new WaitForSeconds(3f);

        SpawnBoss(boss1Prefab);

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("LefttoRight").Length == 0);

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return new WaitForSeconds(3f);

        SpawnBoss(boss2Prefab);
    }
    void SpawnBoss(GameObject bossPrefab)
    {
        Vector3 spawnPosition = centerToCenterPostion; // Start from above the screen
        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

        /*// Create a sequence for the animations
        DG.Tweening.Sequence bossSequence = DOTween.Sequence();

        // Add scale animation
        boss.transform.localScale = Vector3.zero;
        bossSequence.Append(boss.transform.DOScale(Vector3.one, 1f));

        // Add movement animation
        bossSequence.Append(boss.transform.DOMove(centerPosition, 1f));*/
    }



    void SpawnRandomEnemy()
    {
        int randomNumber = Random.Range(0, 3);
        Vector3 spawnPosition = GetRandomTopPosition();
        Vector3 middleRightPosition = GetMiddleRightPosition();
        Vector3 middleLeftPosition = GetMiddleRightPosition();

        switch (randomNumber)
        {
            case 0:
                enemyClone = Instantiate(ToptoBottomPrefab, spawnPosition, Quaternion.identity);
                break;
            case 1:
                enemyClone = Instantiate(RighttoLeftPrefab, middleRightPosition, Quaternion.identity);
                break;
            case 2:
                enemyClone = Instantiate(L2RLoopPrefab, spawnPosition, Quaternion.identity);
                break;
            case 3:
                enemyClone = Instantiate(NewLefttoRight, middleLeftPosition, Quaternion.identity);
                break;
        }
    }
    IEnumerator EnemySpawner(float duration)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            randomFloatTime = Random.Range(2f,3f);
            yield return new WaitForSeconds(randomFloatTime);
            SpawnRandomEnemy();
            
        }
    }
    Vector3 GetRandomTopPosition()
    {
        float randomX = Random.Range(0.1f, 0.9f);
        Vector3 randomPosition = mainCamera.ViewportToWorldPoint(new Vector3(randomX, 0.98f, mainCamera.nearClipPlane));
        randomPosition.z = 0;
        return randomPosition;
    }
    Vector3 GetMiddleRightPosition()
    {
        float randomY = Random.Range(0.5f, 0.9f);
        Vector3 randomPosition = mainCamera.ViewportToWorldPoint(new Vector3(1, randomY, mainCamera.nearClipPlane));
        randomPosition.z = 0;
        return randomPosition;
    }
    Vector3 GetMiddleLeftPosition()
    {
        float randomY = Random.Range(0.5f, 0.9f);
        Vector3 randomPosition = mainCamera.ViewportToWorldPoint(new Vector3(0, randomY, mainCamera.nearClipPlane));
        randomPosition.z = 0;
        return randomPosition;
    }
}
