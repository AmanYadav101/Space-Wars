using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] Animator animator;
    SpawnManager spawnManager;
    PolygonCollider2D polygonCollider2D;
    public GameObject enemy;
    public GameObject enemyProjectile;
    public GameObject bossProjectile1;
    public GameObject bossProjectile2;

    public GameObject level2BossProjectilePrefab;
    public GameObject level3BossProjectilePrefab;
    public GameObject level4BossProjectilePrefab;
    public GameObject level5BossProjectilePrefab;
    public GameObject level6BossProjectilePrefab_1;
    public GameObject level6BossProjectilePrefab_2;
    public GameObject level7BossProjectilePrefab;
    public GameObject level8BossProjectilePrefab_1;
    public GameObject level8BossProjectilePrefab_2;
    public GameObject level9BossProjectilePrefab_1;
    public GameObject level9BossProjectilePrefab_2;
    public GameObject level9BossProjectilePrefab_3;
    public GameObject level10Boss1ProjectilePrefab_1;
    public GameObject level10Boss1ProjectilePrefab_2;
    public GameObject level10Boss2ProjectilePrefab_1;
    public GameObject level10Boss2ProjectilePrefab_2;
    public GameObject level10Boss3ProjectilePrefab_1;
    public GameObject level10Boss3ProjectilePrefab_2;


    private int bossMaxHealth = 1000;
    public int currentHealth;
    public Health_Bar healthBar;
    
    
    public GameObject enemyProjectileClone;
    public GameObject speedUpPowerUpPrefab;
    public GameObject shieldPowerUpPrefab;
    public GameObject tripleShotPowerUpPrefab;

    
    public float moveSpeed = 2f;
    public Player_Manager player_manager;

    private Camera mainCamera;
    private float dropChance = .3f;
    private bool movingLeft = true;
    private bool isDestroyed = false;
/*    private bool canShoot = true;
*/    
    private int bossCurrentHealth;
    private float destroyTime;//Length of the "Enemy_Destroyed" clip.



    // Start is called before the first frame update
    void Start()
    {
        bossCurrentHealth = bossMaxHealth;
        if (healthBar != null) { 
        healthBar.SetMaxHealth(bossCurrentHealth);
        }
        mainCamera = Camera.main;

        UpdateAnimClipTimes(); //Gets the length of all the animations in the animator attached to the game object at the start of the game.
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDestroyed)
        {
            EnemyBehaviour();
            EnemyFireProjectile();

           /* Debug.Log(canShoot);
            if (canShoot) {
                Debug.Log("Inside ifCanShoot");

            }*/

        }
        else if (isDestroyed) 
        {
            gameObject.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime/1.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "BossLeftToRight" || gameObject.tag == "Level2Boss" ||
            gameObject.tag == "Level3Boss" || gameObject.tag == "Level4Boss" ||
            gameObject.tag == "Level5Boss" || gameObject.tag == "Level6Boss" ||
            gameObject.tag == "Level7Boss"|| gameObject.tag == "Level8Boss_1" ||
            gameObject.tag == "Level8Boss_2" || gameObject.tag == "Level9Boss_1" || 
            gameObject.tag == "Level9Boss_2" || gameObject.tag == "Level9Boss_3" ||
            gameObject.tag == "Level10Boss_1" || gameObject.tag == "Level10Boss_2" ||
            gameObject.tag == "Level10Boss_3") 
        {
            Debug.Log(bossCurrentHealth);
            if (collision.gameObject.tag == "Laser")
            {
                Destroy(collision.gameObject);
                if (bossCurrentHealth > 0)
                {
                    bossCurrentHealth -= 50;
                    healthBar.SetHealth(bossCurrentHealth);
                    if (bossCurrentHealth <= 0)
                    {
                        StartCoroutine(DestroyEnemy());
                    }
                }
                else if (bossCurrentHealth <= 0)
                {
                    StartCoroutine(DestroyEnemy());
                }
            }


        }
        else if ((gameObject.tag == "ToptoBottom" ||
                gameObject.tag == "LefttoRight" ||
                gameObject.tag == "LRBoss" ||
                gameObject.tag == "NewLefttoRight" ||
                gameObject.tag == "RighttoLeft" ||
                gameObject.tag == "LefttoRightLoop") &&
                collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);//Destroys the laser that hit the enemy
            StartCoroutine(DestroyEnemy());
        }
        /*if (collision.gameObject.tag == "Laser")
       {
           Destroy(collision.gameObject);//Destroys the laser that hit the enemy
           StartCoroutine(DestroyEnemy());
       }*/
        if (collision.gameObject.tag == "Boundry")
        {
            StartCoroutine(DestroyEnemy());
        }
        if ((gameObject.tag == "ToptoBottom" ||
                gameObject.tag == "LefttoRight" ||
                gameObject.tag == "LRBoss" ||
                gameObject.tag == "NewLefttoRight" ||
                gameObject.tag == "RighttoLeft" ||
                gameObject.tag == "LefttoRightLoop") && collision.gameObject.tag == "Shield") 
        {
            StartCoroutine(DestroyEnemy());

        }

        /*if ((gameObject.tag == "ToptoBottom" ||
                gameObject.tag == "LefttoRight" ||
                gameObject.tag == "LRBoss" ||
                gameObject.tag == "NewLefttoRight" ||
                gameObject.tag == "RighttoLeft" ||
                gameObject.tag == "LefttoRightLoop") && 
                collision.gameObject.tag == "Player")

        {
            StartCoroutine(DestroyEnemy());
        }*/

    }
    private void Awake()
    {        
        polygonCollider2D = GetComponent<PolygonCollider2D>();
/*        player_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Manager>();
*/        /*        player_manager = GameObject.FindObjectOfType<Player_Manager>().GetComponent<Player_Manager>();
        */
        spawnManager = GameObject.FindFirstObjectByType<SpawnManager>().GetComponent<SpawnManager>();
    }      
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*        if (gameObject.tag == "BossLeftToRight" && collision.gameObject.tag == "Laser")
                {

                    Destroy(collision.gameObject);
                     if (bossCurrentHealth > 0)
                     {
                     bossCurrentHealth -= 50;
                         if (bossCurrentHealth <= 0)
                         {
                             StartCoroutine(DestroyEnemy());
                         }
                     }
                     else if (bossCurrentHealth <= 0)
                     {
                         StartCoroutine(DestroyEnemy());
                     }


                }*/

        if (gameObject.tag == "BossLeftToRight" || gameObject.tag == "Level2Boss"||
            gameObject.tag == "Level3Boss" || gameObject.tag == "Level4Boss" ||
            gameObject.tag == "Level5Boss" || gameObject.tag == "Level6Boss" ||
            gameObject.tag == "Level7Boss" || gameObject.tag == "Level8Boss_1" ||
            gameObject.tag == "Level8Boss_2" || gameObject.tag == "Level9Boss_1" ||
            gameObject.tag == "Level9Boss_2" || gameObject.tag == "Level9Boss_3" ||
            gameObject.tag == "Level10Boss_1" || gameObject.tag == "Level10Boss_2" ||
            gameObject.tag == "Level10Boss_3")
        {
            if(collision.gameObject.tag == "Laser") 
            { 
                Destroy(collision.gameObject);
                if (bossCurrentHealth > 0)
                {
                    bossCurrentHealth -= 50;
                    if (bossCurrentHealth <= 0)
                    {
                        StartCoroutine(DestroyEnemy());
                    }
                }
                else if (bossCurrentHealth <= 0)
                {
                    StartCoroutine(DestroyEnemy());
                }
            }

            if(collision.gameObject.tag == "Player")
            {
                StartCoroutine(player_manager.DestroyPlayer());            }
        }
        else if ((gameObject.tag == "ToptoBottom" ||
                gameObject.tag == "LefttoRight" ||
                gameObject.tag == "LRBoss" ||
                gameObject.tag == "NewLefttoRight" ||
                gameObject.tag == "RighttoLeft" ||
                gameObject.tag == "LefttoRightLoop" )&&
                collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);//Destroys the laser that hit the enemy
            StartCoroutine(DestroyEnemy());
        }
        if ((gameObject.tag == "ToptoBottom" ||
                gameObject.tag == "LefttoRight" ||
                gameObject.tag == "LRBoss" ||
                gameObject.tag == "NewLefttoRight" ||
                gameObject.tag == "RighttoLeft" ||
                gameObject.tag == "LefttoRightLoop") && 
                collision.gameObject.tag == "Player"  )
        {
                StartCoroutine(DestroyEnemy());
        }
        
    }
    

    
    public IEnumerator DestroyEnemy()//Coroutine for playing the animation of the destroying the enemy before it gets destroyed.
                                     //"destroyTime" is the length of the clip named "Enemy_Destroyed from the animator"
    {
        if (animator != null)
        {
            isDestroyed = true;
            animator.SetBool("Destroy", true);
            polygonCollider2D.isTrigger = true;//Setting the collider to trigger so that the other lasers won't interact with the previous gameobject thats still being destroyed.
            DropPowerUp();

            yield return new WaitForSeconds(destroyTime);
            Destroy(gameObject);
        }

    }
    public void UpdateAnimClipTimes()
    {
        if (animator != null)
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;//Gets all the clips in the animator
            foreach (AnimationClip clip in clips)
            {
                switch (clip.name)
                {
                    case "Enemy_Destroyed":
                    case "Boss Destroyed":
                    case "Level2BossDestroy":
                    case "Level 4 Boss Destroyed":
                    case "Level 5 Boss Destroyed":
                    case "Level 6 Boss Destroyed":
                    case "Level 7 Boss Destroyed":
                        destroyTime = clip.length-1f;//sets the variable to the length of the clip 
                        break;
                    default:
                        destroyTime = 4f;
                        break;
                }
            }
        }
    }

    void EnemyFireProjectile()
    {
        if (!FindObjectOfType<GameManager>() || !FindObjectOfType<Player_Manager>())
        {
            return;
        }
        if (!FindObjectOfType<GameManager>().GetIsPaused() && !FindObjectOfType<Player_Manager>().GetIsDestroyed()) { 
        
        if (Random.Range(0f,1000)<1.5f )
        {
            if(gameObject.tag == "BossLeftToRight")
            {
                enemyProjectileClone = Instantiate(bossProjectile1, new Vector3(transform.position.x + .35f, transform.position.y -.8f , 0), Quaternion.identity);
                enemyProjectileClone = Instantiate(bossProjectile1, new Vector3(transform.position.x + .6f, transform.position.y -.65f , 0), Quaternion.identity);
                enemyProjectileClone = Instantiate(bossProjectile1, new Vector3(transform.position.x - .35f, transform.position.y -.8f , 0), Quaternion.identity);
                enemyProjectileClone = Instantiate(bossProjectile1, new Vector3(transform.position.x - .6f, transform.position.y -.65f , 0), Quaternion.identity);
                enemyProjectileClone = Instantiate(bossProjectile2, new Vector3(transform.position.x, transform.position.y - 2.2f, 0), Quaternion.identity);
                enemyProjectileClone = Instantiate(enemyProjectile, new Vector3(transform.position.x-1.5f, transform.position.y - .8f, 0), Quaternion.identity);
                enemyProjectileClone = Instantiate(enemyProjectile, new Vector3(transform.position.x+1.4f, transform.position.y - .8f, 0), Quaternion.identity);
                enemyProjectileClone = Instantiate(enemyProjectile, new Vector3(transform.position.x+1.5f, transform.position.y - .65f, 0), Quaternion.identity);
                enemyProjectileClone = Instantiate(enemyProjectile, new Vector3(transform.position.x-1.4f, transform.position.y - .65f, 0), Quaternion.identity);

            }
            else if(gameObject.tag == "Level2Boss")
            {
                enemyProjectileClone = Instantiate(level2BossProjectilePrefab, new Vector3(transform.position.x -1.25f ,transform.position.y-.1f,0), Quaternion.identity);
            }
            else if(gameObject.tag == "Level3Boss")
            {
                enemyProjectileClone = Instantiate(level3BossProjectilePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }
            else if(gameObject.tag == "Level4Boss")
            {
                enemyProjectileClone = Instantiate(level4BossProjectilePrefab, new Vector3(transform.position.x+.3f, transform.position.y-1.1f, 0), Quaternion.identity);
            }
            else if(gameObject.tag == "Level5Boss")
            {
                enemyProjectileClone = Instantiate(level5BossProjectilePrefab, new Vector3(transform.position.x -1.05f, transform.position.y -4.15f, 0), Quaternion.identity);
            }
            else if(gameObject.tag == "Level6Boss")
            {
                switch(Random.Range(0,2))
                {
                    case 0: enemyProjectileClone = Instantiate(level6BossProjectilePrefab_1, new Vector3(transform.position.x -.6f , transform.position.y + .1f, 0), Quaternion.identity); break;
                    case 1: enemyProjectileClone = Instantiate(level6BossProjectilePrefab_2, new Vector3(transform.position.x , transform.position.y - 3.15f, 0), Quaternion.identity); break;
                }
            }
            else if(gameObject.tag == "Level7Boss")
            {
                enemyProjectileClone = Instantiate(level7BossProjectilePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }
            else if (gameObject.tag == "Level8Boss_1")
            {
                enemyProjectileClone = Instantiate(level8BossProjectilePrefab_1, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }
            else if(gameObject.tag == "Level8Boss_2")
            {
                enemyProjectileClone = Instantiate(level8BossProjectilePrefab_2, new Vector3(transform.position.x-.5f, transform.position.y-2.3f, 0), Quaternion.identity);

            }else if (gameObject.tag == "Level9Boss_1")
            {
                enemyProjectileClone = Instantiate(level9BossProjectilePrefab_1, new Vector3(transform.position.x, transform.position.y-1f, 0), Quaternion.identity);
            }
            else if(gameObject.tag == "Level9Boss_2")
            {
                enemyProjectileClone = Instantiate(level9BossProjectilePrefab_2, new Vector3(transform.position.x-.45f, transform.position.y-.8f, 0), Quaternion.identity);

            }else if(gameObject.tag == "Level9Boss_3")
            {
                enemyProjectileClone = Instantiate(level9BossProjectilePrefab_3, new Vector3(transform.position.x-7.8f, transform.position.y - 1.65f, 0), Quaternion.identity);

            }
            else if (gameObject.tag == "Level10Boss_1")
            {
                switch (Random.Range(0, 2))
                {
                    case 0: enemyProjectileClone = Instantiate(level10Boss1ProjectilePrefab_1, new Vector3(transform.position.x-1.1f, transform.position.y-.2f , 0), Quaternion.identity); break;
                    case 1: enemyProjectileClone = Instantiate(level10Boss1ProjectilePrefab_2, new Vector3(transform.position.x-1.4f, transform.position.y-1.2f, 0), Quaternion.identity); break;
                }
            }else if (gameObject.tag == "Level10Boss_2")
            {
                switch (Random.Range(0, 2))
                {
                    case 0: enemyProjectileClone = Instantiate(level10Boss2ProjectilePrefab_1, new Vector3(transform.position.x-0.35f , transform.position.y -0.7f, 0), Quaternion.identity); break;
                    case 1: enemyProjectileClone = Instantiate(level10Boss2ProjectilePrefab_2, new Vector3(transform.position.x-0.1f, transform.position.y - .5f, 0), Quaternion.identity); break;
                }
            }else if (gameObject.tag == "Level10Boss_3")
            {
                switch (Random.Range(0, 2))
                {
                    case 0: enemyProjectileClone = Instantiate(level10Boss3ProjectilePrefab_1, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity); break;
                    case 1: enemyProjectileClone = Instantiate(level10Boss3ProjectilePrefab_2, new Vector3(transform.position.x, transform.position.y , 0), Quaternion.identity); break;
                }
            }
            else { 
            enemyProjectileClone = Instantiate(enemyProjectile, new Vector3(transform.position.x, transform.position.y - .2f, 0), enemy.transform.rotation);
            }
            }
        }
    }

    void DropPowerUp()//enemies when destroyed will have a chance to drop a powerup
    {
        float randomValue = Random.Range(0f, 1f);//Chance to drop a powerup
         
        if (randomValue <= dropChance)
        {
            int randomValueForWhichPowerUpToDrop = Random.Range(0, 3);//Chance for which powerup to drop
            if (randomValueForWhichPowerUpToDrop == 0)
            {
                Instantiate(speedUpPowerUpPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }
            else if (randomValueForWhichPowerUpToDrop == 1)
            {
                Instantiate(shieldPowerUpPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }
            else if (randomValueForWhichPowerUpToDrop == 2)
            {
                Instantiate(tripleShotPowerUpPrefab, gameObject.transform.position, gameObject.transform.rotation);
            }

        }
    }
    

    void EnemyBehaviour()
    {
        Vector2 position = transform.position;
        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(position);

        switch (gameObject.tag)
        {
            case "ToptoBottom":
                gameObject.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                break;

            case "LefttoRight":
                if (viewportPosition.x > 0.9f)
                {
                    movingLeft = true;// Move left if the enemy reaches the right edge

                }
                else if ((viewportPosition.x < 0.1f))
                {
                    movingLeft = false;// Move right if the enemy reaches the left edge
                }
                //Move enemy based on the direction
                if (movingLeft)
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
                transform.Translate(Vector3.down * (moveSpeed / 2) * Time.deltaTime);
                TeleportEnemyUpDown();
                break;
            case "BossLeftToRight":
                /*                Debug.Log(viewportPosition.x);
*/
                if (viewportPosition.x > 0.8f)
                {

                    movingLeft = true; // Move left if the enemy reaches the right edge
                }
                else if (viewportPosition.x < 0.2f)
                {
                    movingLeft = false; // Move right if the enemy reaches the left edge
                }
                // Move enemy based on the direction
                if (movingLeft)
                {

                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
                break;
            case "Level2Boss":
                if (viewportPosition.x > 0.8f)
                {

                    movingLeft = true; // Move left if the enemy reaches the right edge
                }
                else if (viewportPosition.x < 0.2f)
                {
                    movingLeft = false; // Move right if the enemy reaches the left edge
                }
                // Move enemy based on the direction
                if (movingLeft)
                {

                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
                break;
            case "Level3Boss":
            case "Level4Boss":
            case "Level5Boss":
            case "Level6Boss":
            case "Level7Boss":
            case "Level8Boss_1":
            case "Level8Boss_2":
            case "Level9Boss_1":
            case "Level9Boss_2":
            case "Level9Boss_3":
            case "Level10Boss_1":
            case "Level10Boss_2":
            case "Level10Boss_3":
                /*                Debug.Log(viewportPosition.x);
                */
                if (viewportPosition.x > 0.8f)
                {
                     

                    movingLeft = true; // Move left if the enemy reaches the right edge
                }
                else if (viewportPosition.x < 0.2f)
                {
                    movingLeft = false; // Move right if the enemy reaches the left edge
                }
                // Move enemy based on the direction
                if (movingLeft)
                {
                    

                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.right* moveSpeed * Time.deltaTime);
                }
                break;

            case "LRBoss":
                gameObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                TeleportEnemy();
                break;

            case "NewLefttoRight":
                gameObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                gameObject.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                break;

            case "RighttoLeft":
                gameObject.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                gameObject.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                break;

            case "LefttoRightLoop":
                if (viewportPosition.x > 0.9f)
                {
                    movingLeft = true; // Move left if the enemy reaches the right edge
                }
                else if (viewportPosition.x < 0.1f)
                {
                    movingLeft = false; // Move right if the enemy reaches the left edge
                }
                if (movingLeft)
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
                transform.Translate(Vector3.down * (moveSpeed / 2) * Time.deltaTime);
                break;
        }
    }

    public void StartShooting()
    {
        /*if (FindObjectOfType<GameManager>().GetIsPaused()) {

            canShoot = false;
        }
        else
        {
            canShoot = true;
        }*/
        polygonCollider2D.isTrigger = false;

    }
    private void TeleportEnemy()
    {
        Vector2 position = transform.position;
        // world is (-infinity , -infinity) to (+infinity, +infinity)
        //viewport is the area covered by the camera. (0,0) for bottom left and (1,1) for top right corner
        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(position);
        if (viewportPosition.x > 1.08f)
        {
            position.x = mainCamera.ViewportToWorldPoint(new Vector2(0, viewportPosition.y)).x;
        }
        else if (viewportPosition.x < -0.08f)
        {
            position.x = mainCamera.ViewportToWorldPoint(new Vector2(1, viewportPosition.y)).x;

        }
        transform.position = position;
    }
    private void TeleportEnemyUpDown()
    {
        Vector2 position = transform.position;
        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(position);

        /*        if (viewportPosition.y > 0.9f)
                {
                    position.y = mainCamera.ViewportToWorldPoint(new Vector2(viewportPosition.x, 0.89f)).y;

                }
                else*/
        if (viewportPosition.y < 0.3f)
        {
            position.y = mainCamera.ViewportToWorldPoint(new Vector2(viewportPosition.x, 0.99f)).y;

        }
        transform.position = position;
    }
}