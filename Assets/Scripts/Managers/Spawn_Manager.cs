using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnManager : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject player;
    public GameObject projectile;
    private bool isTripleShot;
    public GameObject incomingBossText;

    private GameObject projectileClone;
    [SerializeField] private float shootCooldown = .5f;
    private float lastShotTime;
    public GameObject enemyPrefab;
    private GameObject enemyClone;
    public Camera mainCamera;
    public int max_Count = 10;
    float spawnTime = 20f;
    

    public GameObject ToptoBottomPrefab;
    public GameObject RighttoLeftPrefab;
    public GameObject L2RLoopPrefab;
    public GameObject NewLefttoRight;
    public GameObject boss1Prefab;
    public GameObject boss2Prefab;
    public GameObject level1BossPrefab;
    public GameObject level2BossPrefab;
    public GameObject level3BossPrefab;
    public GameObject level4BossPrefab;
    public GameObject level5BossPrefab;
    public GameObject level6BossPrefab;
    public GameObject level7BossPrefab;
    public GameObject level8BossPrefab_1;
    public GameObject level8BossPrefab_2;
    public GameObject level9BossPrefab_1;
    public GameObject level9BossPrefab_2;
    public GameObject level9BossPrefab_3;
    public GameObject level10BossPrefab_1;
    public GameObject level10BossPrefab_2;
    public GameObject level10BossPrefab_3;
    float randomFloatTime;
    GameManager gameManager;

    public GameObject pauseMenuUI;
    public GameObject LevelFinsihUI;
    private List<IEnumerator> runningCoroutines = new List<IEnumerator>();
  /*  //Enemy Cluster
    public int enemyCount = 10;
    public float moveDuration = 2f;
    public float formationDuration = 2f;
    public float radius = 2f;*/
    PolygonCollider2D polygonCollider2D;
    private List<GameObject> enemies = new List<GameObject>();
    private Vector3 centerPosition;
    private Vector3 centerToCenterPostion;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Start()
    {
        /*gameManager.HighScoreText();*/
        incomingBossText.SetActive(false);

        LevelFinsihUI.SetActive(false);
        Time.timeScale = 1;
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
        
        switch(SceneManager.GetActiveScene().name)
            {
            case("Endless"):
                StartCoroutine(SpawnSequence());
                break;
            case ("Level_1"):
                StartCoroutine(Level1());
                break;
            case ("Level_2"):
                StartCoroutine(Level2());
                break;
            case ("Level_3"):
                StartCoroutine(Level3());
                break;
            case ("Level_4"):
                StartCoroutine(Level4());
                break;
            case ("Level_5"):
                StartCoroutine(Level5());
                break;
            case ("Level_6"):
                StartCoroutine(Level6());
                break;
            case ("Level_7"):
                StartCoroutine(Level7());
                break;
            case ("Level_8"):
                StartCoroutine(Level8());
                break;
            case ("Level_9"):
                StartCoroutine(Level9());
                break;
            case ("Level_10"):
                StartCoroutine(Level10());
                break;
            /*default:
                StartCoroutine(SpawnSequence());
                break;*/
        }
    }


    void Update()
    {
        if (player != null && player.activeSelf && player.GetComponent<Player_Manager>().currentHealth > 0)
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {

        // if player is available the proceed
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= lastShotTime + shootCooldown && isTripleShot)//Time.time is how much time has been elapsed till the start of the game.
                                                                                                          //Fires 3 Projectiles if isTripleShot is set to true. 
        {
            audioManager.PlaySFX(audioManager.trippleLasersSFX);
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0), player.transform.rotation);
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x + 0.50f, player.transform.position.y + .25f, 0), player.transform.rotation);
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x - 0.50f, player.transform.position.y + .25f, 0), player.transform.rotation);
            lastShotTime = Time.time;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && Time.time >= lastShotTime + shootCooldown)
        {

            /*  int randnum = Random.Range(1, 7);
              switch (randnum)
              {
                  case 0:
                      audioManager.PlaySFX(audioManager.laserShootSFX1);
                      break;
                  case 1:
                      audioManager.PlaySFX(audioManager.laserShootSFX2);
                      break;
                  case 2:
                      audioManager.PlaySFX(audioManager.laserShootSFX3);
                      break;
                  case 3:
                      audioManager.PlaySFX(audioManager.laserShootSFX4);
                      break;
                  case 4:
                      audioManager.PlaySFX(audioManager.laserShootSFX5);
                      break;
                  case 5:
                      audioManager.PlaySFX(audioManager.laserShootSFX6);
                      break;
                  default:
                      audioManager.PlaySFX(audioManager.laserShootSFX1);
                      break;


              }*/
            audioManager.PlaySFX(audioManager.laserShootSFX1);

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
    {   while (true)
        {
            yield return StartCoroutine(EnemySpawner(spawnTime));

            SpawnBoss(boss1Prefab);
            /*            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("LefttoRight").Length == 0);
            */
            yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());


            yield return new WaitForSeconds(4f);

            yield return StartCoroutine(EnemySpawner(spawnTime));

            yield return new WaitForSeconds(4f);

            SpawnBoss(boss2Prefab);
            yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());

            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("LRBoss").Length == 0);
        
        }

    }


    IEnumerator Level1()
    {
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());
        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);
        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);


        SpawnBoss(level1BossPrefab);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("BossLeftToRight").Length == 0);
        Time.timeScale = 0;
        LevelFinsihUI.SetActive(true);
        UnlockNextLevel(2); 


    }
    IEnumerator Level2()
    {
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());

        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);

        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);
        SpawnBoss(level2BossPrefab);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level2Boss").Length == 0);
        Time.timeScale = 0;

        LevelFinsihUI.SetActive(true);
        UnlockNextLevel(3);

    }
    IEnumerator Level3()
    {
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());
        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);

        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);

        SpawnBoss(level3BossPrefab);
        yield return new WaitUntil(()=> GameObject.FindGameObjectsWithTag("Level3Boss").Length==0);
        Time.timeScale = 0;
        LevelFinsihUI.SetActive(true);
        UnlockNextLevel(4);

    }

    IEnumerator Level4()
    {
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());
        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);

        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);

        SpawnBoss(level4BossPrefab);
        yield return new WaitUntil(()=> GameObject.FindGameObjectsWithTag("Level4Boss").Length==0) ;
        Time.timeScale = 0;
        LevelFinsihUI.SetActive(true);
            UnlockNextLevel(5);

    }
    IEnumerator Level5()
    {
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());
        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);

        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);

        SpawnBoss(level5BossPrefab);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level5Boss").Length == 0);
        Time.timeScale = 0;
        LevelFinsihUI.SetActive(true);
        UnlockNextLevel(6);

    }
    IEnumerator Level6()
    {
        
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());
        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);

        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);

        SpawnBoss(level6BossPrefab);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level6Boss").Length == 0);
        Time.timeScale = 0;
        LevelFinsihUI.SetActive(true);
        UnlockNextLevel(7);

    }
    IEnumerator Level7()
    {
        
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());
        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);

        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);
        SpawnBoss(level7BossPrefab);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level7Boss").Length == 0);
        Time.timeScale = 0;
        LevelFinsihUI.SetActive(true);
        UnlockNextLevel(8);

    }
    IEnumerator Level8() 
    {
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());

        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);
        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);

        SpawnBoss(level8BossPrefab_1);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level8Boss_1").Length ==0);
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());

        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);
        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);

        SpawnBoss(level8BossPrefab_2);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level8Boss_2").Length == 0);
        Time.timeScale = 0;
        LevelFinsihUI.SetActive(true);
        UnlockNextLevel(9);

    }
    IEnumerator Level9() 
    {
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());
        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);
        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);

        SpawnBoss(level9BossPrefab_1);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level9Boss_1").Length ==0);
        yield return StartCoroutine(EnemySpawner(spawnTime)); 
        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());


        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);
        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);

        SpawnBoss(level9BossPrefab_2);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level9Boss_2").Length == 0);

        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);
        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);
        SpawnBoss(level9BossPrefab_3);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level9Boss_3").Length == 0);
        Time.timeScale = 0;
        LevelFinsihUI.SetActive(true);

        UnlockNextLevel(10);

    }
    IEnumerator Level10() 
    {
        yield return StartCoroutine(EnemySpawner(spawnTime));

        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());
        
        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);
        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);

        SpawnBoss(level10BossPrefab_1);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level10Boss_1").Length ==0);
        yield return StartCoroutine(EnemySpawner(spawnTime)); 
        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());

        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);
        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);
        
        SpawnBoss(level10BossPrefab_2);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level10Boss_2").Length == 0);
        yield return StartCoroutine(EnemySpawner(spawnTime));

        incomingBossText.SetActive(true);
        audioManager.PlaySFX(audioManager.bossIncomingSFX);
        yield return new WaitForSeconds(4);
        incomingBossText.SetActive(false);
        
        yield return StartCoroutine(WaitUntilEnemiesAreDestroyed());
        SpawnBoss(level10BossPrefab_3);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Level10Boss_3").Length == 0);
        Time.timeScale = 0;
        SceneManager.LoadScene("Main Menu");

    }
    IEnumerator WaitUntilEnemiesAreDestroyed()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("LefttoRight").Length == 0);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("RighttoLeft").Length == 0);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("ToptoBottom").Length == 0);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("LefttoRightLoop").Length == 0);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("NewLefttoRight").Length == 0);
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

    //Being called after every level. 
    public void UnlockNextLevel(int levelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (levelIndex >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelIndex + 1);
            PlayerPrefs.Save();
        }
    }
    IEnumerator EnemySpawner(float duration)
    {

        if (SceneManager.GetActiveScene().name == "Level_6")
        {
            float endTime = Time.time + duration;

            while (Time.time < endTime)
            {
                randomFloatTime = Random.Range(1f,2f);
                yield return new WaitForSeconds(randomFloatTime);
                SpawnRandomEnemy();

            }
        }
        else
        {
            float endTime = Time.time + duration;

            while (Time.time < endTime)
            {
                randomFloatTime = Random.Range(1f, 3f);
                yield return new WaitForSeconds(randomFloatTime);
                SpawnRandomEnemy();

            }
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

    public void LevelFinsih()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }
   public void PauseSpawning()
    {
        foreach (var coroutine in runningCoroutines) 
        { 
            StopCoroutine(coroutine);
        }
    }
    public void ResumeSpawning()
    {
        foreach(var coroutine in runningCoroutines)
        {
            StartCoroutine(coroutine);
        }
    }

}
