using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public Animator animator;
    private Camera mainCamera;
    //Speed vars
    private int moveSpeed = 5;
    private int normalSpeed;
    private int newSpeed = 10;


     Movemeny_JoyStick movemeny_JoyStick;
    private Rigidbody2D rb;
    //Fire Objects
    public Animator FireAnimator;
    public GameObject leftWingFire;
    public GameObject rightWingFire;
    public GameObject middleFire;
    public GameObject thrusters;
    //Health variables
    int maxHealth = 100;
    public int currentHealth;
    public Health_Bar healthBar;
    //Shield
    public GameObject shield;
    private bool isInvincible=false;
     PolygonCollider2D polygonCollider2D;
    bool isMoving = false;
    public GameObject PlayerDeadUI;
    private bool isDestroyed = false;

    bool isMoveingLeft = false;
    bool isMoveingRight = false;


    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isSwiping = false;


    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = moveSpeed;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        mainCamera = Camera.main;
        leftWingFire.SetActive(false);
        rightWingFire.SetActive(false);
        middleFire.SetActive(false);
        thrusters.SetActive(false);
        shield.SetActive(false);
        PlayerDeadUI.SetActive(false);
    }

    private void Awake()
    {
        movemeny_JoyStick = GameObject.FindObjectOfType<Movemeny_JoyStick>();
            rb = GetComponent<Rigidbody2D>();
            polygonCollider2D = gameObject.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDestroyed) 
        { 
        Movement();
        TeleportPlayer(); 
        TurnOnFire();
        TurnOnThrusters();
        TurnOnShield();//Turns on the shield when the IsInvincible boolean gets to true in the Shield_Powerup Script
        }
        if(currentHealth == 0) 
        { 
        StartCoroutine(DestroyPlayer());
        }

    }


    void Movement()
    {
        Vector3 position = transform.position;
        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(position);
        //checks if the ship is moving or not. Used for playing different animations from the animator

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 move = new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
            transform.Translate(move);


            //for Rotating our ship to the left
            isMoving = true;
            isMoveingLeft = true;
            isMoveingRight = false;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 move = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            transform.Translate(move);


            //for Rotating our ship to the right
            isMoving = true;
            isMoveingLeft = false;
            isMoveingRight = true;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (viewportPosition.y < 0.2f) // Only allow movement if within lower 40% of the viewport
            {
                Vector3 move = new Vector3(0, +moveSpeed * Time.deltaTime, 0);
                transform.Translate(move);
            }
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (viewportPosition.y > 0.1f) // Only allow movement if within the viewport
            {
                Vector3 move = new Vector3(0, -moveSpeed * Time.deltaTime, 0);
                transform.Translate(move);
            }
        }
        else {
            isMoving = false;
        }

        if (movemeny_JoyStick.joyStickVec.y != 0 || movemeny_JoyStick.joyStickVec.x != 0)
        {
            Vector3 move = new Vector3(movemeny_JoyStick.joyStickVec.x * moveSpeed * Time.deltaTime, movemeny_JoyStick.joyStickVec.y * moveSpeed * Time.deltaTime, 0);
            Vector3 projectedPosition = transform.position + move;
            Vector2 projectedViewportPosition = Camera.main.WorldToViewportPoint(projectedPosition);

            if (projectedViewportPosition.y >= 0.1f && projectedViewportPosition.y <= 0.25f)
            {
                transform.Translate(move);
                isMoving = true;
            }
            else if (projectedViewportPosition.y < 0.1f || projectedViewportPosition.y > 0.25f)
            {
                transform.Translate(new Vector3(movemeny_JoyStick.joyStickVec.x * moveSpeed * Time.deltaTime, 0, 0));
                isMoving = true;
            }
        }
      


        if (!isMoving)// making the Turn parameter from the animator to 0 so that ideal animation can be played if no movement is happening.
        {
            //for making the ship back to the ideal position
            animator.SetInteger("Turn", 0);
        }
        else
        {
            if (isMoveingLeft)
            {
                animator.SetInteger("Turn", -1);
            }
            else if (isMoveingRight)
            {
                animator.SetInteger("Turn", +1);
            }
        }

    }
    private void TeleportPlayer()
    {
        Vector2 position = transform.position;
        // world is (-infinity , -infinity) to (+infinity, +infinity)
        //viewport is the area covered by the camera. (0,0) for bottom left and (1,1) for top right corner
        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(position);
        if(viewportPosition.x > .95f)
        {
            position.x = mainCamera.ViewportToWorldPoint(new Vector2(0.05f, viewportPosition.y)).x;
        }
        else if(viewportPosition.x<0.05f){
            position.x = mainCamera.ViewportToWorldPoint(new Vector2(.95f, viewportPosition.y)).x;

        }
        transform.position = position;
    }

    //+++++++++++++++++++Health Logic+++++++++++++++++++++
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
            Debug.Log("Name of Object Colliding with player : " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy Laser"))
        {
            Destroy(collision.gameObject);
            if (isInvincible) { }

            else if (currentHealth > 0)//Takes Damage if health is greater than 0 and updates
                                       //the health bar based on the health.
            {
                currentHealth -= 20;
                healthBar.SetHealth(currentHealth);

                if (currentHealth <= 0)//if statement inside this else if so that we check if the current health is 0 or less then 0
                                       //or else the player will still be alive at 0 health until next projectile hits it.
                {
                    StartCoroutine(DestroyPlayer());
                }

            }
            else if (currentHealth <= 0)
            {
                StartCoroutine(DestroyPlayer());
            }
        }
        /* if (collision.gameObject.tag == "BossLeftToRight")
        {

            Debug.Log("Collided with BossLeftToRight");
            currentHealth = 0;
            StartCoroutine(DestroyPlayer());

        }*/
        else if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("LefttoRight") ||
            collision.gameObject.CompareTag("RighttoLeft") ||
            collision.gameObject.CompareTag("LefttoRightLoop") ||
            collision.gameObject.CompareTag("ToptoBottom") ||
            collision.gameObject.CompareTag("LRBoss"))

        {
            if (isInvincible) { return; }
            Enemy_Manager enemy = collision.gameObject.GetComponent<Enemy_Manager>();
            
            Debug.Log("Enemy Destroyed?: " + enemy.GetIsDestroyed());
            if (enemy != null && enemy.GetIsDestroyed()){
       
                Debug.Log("Inside if");
                if (currentHealth > 0)
                {


                    currentHealth -= 40;
                    healthBar.SetHealth(currentHealth);
                    if (currentHealth <= 0)
                    {
                        StartCoroutine(DestroyPlayer());
                    }
                }
                else if (currentHealth <= 0)
                {
                    StartCoroutine(DestroyPlayer());
                }

            } }
        

    }


    public IEnumerator DestroyPlayer()
    {
        isDestroyed = true;
        leftWingFire.SetActive(false);
        rightWingFire.SetActive(false);
        middleFire.SetActive(false);
        animator.SetBool("Destroy", true);
        polygonCollider2D.isTrigger = false;


        yield return new WaitForSeconds(2.63f);
        Time.timeScale = 0;
        /*        gameObject.SetActive(false);
        */
        Destroy(gameObject);

        PlayerDeadUI.SetActive(true);
        

    }
    //Turns on the Thrusters animation when the moveSpeed gets equals to the newSpeed.
    void TurnOnThrusters()
    {   if(moveSpeed == newSpeed) 
        { 
            thrusters.SetActive(true);
        }
        else
        {
            thrusters.SetActive(false);
        }
    }
    void TurnOnFire()//Function for activating the fire based on the health of the player.
    {
        if (currentHealth <= 20)
        {
            leftWingFire.SetActive(true);
            rightWingFire.SetActive(true);
            middleFire.SetActive(true);
        }
        else if (currentHealth <= 40)
        {
            leftWingFire.SetActive(true);
            rightWingFire.SetActive(true);
            middleFire.SetActive(false);

        }
        else if (currentHealth <= 60) 
        { 
            leftWingFire.SetActive(false);
            rightWingFire.SetActive(true);
            middleFire.SetActive(false);

        }
        else
        {
            leftWingFire.SetActive(false);
            rightWingFire.SetActive(false);
            middleFire.SetActive(false);
        }
    }


    //Turns on the Shields if the isInvincible bool is true.
    void TurnOnShield()
    {        
        if (currentHealth > 0 && isInvincible)
        {

            shield.SetActive(true);
        }
        else
        {

            shield.SetActive(false);
        }

    }

    //Getters and Setters
    
    //Starting Move Speed
    public int GetMoveSpeed()
    {
        return moveSpeed;
    }
    public void SetMoveSpeed(int speed)
    {
        moveSpeed = speed;
    }

    //Normal Move Speed
    public int GetNormalMoveSpeed()
    {
        return normalSpeed;
    }
    public void SetNormalSpeed(int speed)
    {
        normalSpeed = speed;
    }

    //New Move Speed
    public int GetNewMoveSpeed()
    {
        return newSpeed;
    }
    public void SetNewMoveSpeed(int speed)
    {
        newSpeed = speed;
    }

    //Shield Invincible
    public void SetIsInvincible(bool invincible) { 
        isInvincible = invincible;
    }
    public bool GetIsInvincible() { 
        return isInvincible;
    }
    public void SetIsDestroyed(bool destroyed) 
    { 
        isDestroyed = destroyed;
    }
    public bool GetIsDestroyed()
    {
        return isDestroyed;
    }
    /*public void SetCanTakeDamage(bool canTake)
    {
        canTakeDamage = canTake;
    }

    public bool GetCanTakeDamage()
    {
        return canTakeDamage;
    }*/
}



