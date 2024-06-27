using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] Animator animator;
    private float destroyTime;//Length of the "Enemy_Destroyed" clip.
    public GameObject enemy;
    public GameObject enemyProjectile;
    public GameObject enemyProjectileClone;
    public GameObject speedUpPowerUpPrefab;
    public GameObject shieldPowerUpPrefab;
    public GameObject tripleShotPowerUpPrefab;
    private float dropChance = .3f;
    SpawnManager spawnManager;
    PolygonCollider2D polygonCollider2D;
    public float moveSpeed = 2f;
    public Player_Manager player_manager;

    private Camera mainCamera;
    private bool movingLeft = true;
    private bool isDestroyed = false;
    private bool canShoot = false;



    // Start is called before the first frame update
    void Start()
    {
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
            
        }
        else if (isDestroyed) 
        {
            gameObject.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime/1.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundry" || collision.gameObject.tag == "Shield")
        {
            StartCoroutine(DestroyEnemy());
        }
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
        if(collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);//Destroys the laser that hit the enemy
            StartCoroutine(DestroyEnemy());
        }
        if (collision.gameObject.tag == "Player"  )
        
        {

            StartCoroutine (DestroyEnemy());
        }
        
    }
    

    
    public IEnumerator DestroyEnemy()//Coroutine for playing the animation of the destroying the enemy before it gets destroyed.
                              //"destroyTime" is the length of the clip named "Enemy_Destroyed from the animator"
    {
        isDestroyed = true;
        animator.SetBool("Destroy", true);
        polygonCollider2D.isTrigger = true;//Setting the collider to trigger so that the other lasers won't interact with the previous gameobject thats still being destroyed.
        DropPowerUp();
                
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);

    }
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;//Gets all the clips in the animator
        foreach (AnimationClip clip  in clips)
        {
            switch (clip.name)
            {
                case "Enemy_Destroyed":
                    destroyTime = clip.length;//sets the variable to the length of the clip 
                    break;
            }
        }
    }

    void EnemyFireProjectile()
    {
        if (Random.Range(0f,1000)<0.7f )
        {
        enemyProjectileClone = Instantiate(enemyProjectile, new Vector3(transform.position.x, transform.position.y - .2f, 0), enemy.transform.rotation);
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
       
        if(gameObject.tag == "ToptoBottom")
        {
            gameObject.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
         else if(gameObject.tag == "LefttoRight")
         {
            gameObject.transform.Translate(new Vector3(1*Time.deltaTime,0,0));
            TeleportEnemy();
         }
        else if(gameObject.tag == "NewLefttoRight")
        {
            gameObject.transform.Translate(new Vector3(1 * Time.deltaTime, -1 * Time.deltaTime, 0));
        }
        else if (gameObject.tag == "RighttoLeft")
        {
            gameObject.transform.Translate(new Vector3(-1 * Time.deltaTime, -1 * Time.deltaTime, 0));
        }
        else if(gameObject.tag == "LefttoRightLoop")
        {
            // Check if the enemy has reached the edges of the viewport
            if (viewportPosition.x > 0.9f)
            {
               movingLeft = true;// Move left if the enemy reaches the right edge

            }
            else if((viewportPosition.x < 0.1f)) 
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
            // Ensure the enemy also moves down
            transform.Translate(Vector3.down * (moveSpeed / 2) * Time.deltaTime);

        }
    }

    public void StartShooting()
    {
        canShoot = true;
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

}