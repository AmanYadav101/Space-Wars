using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EventSystems;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public Animator animator;
    private int moveSpeed = 5;
    private Camera mainCamera;
    private int normalSpeed;
    private int newSpeed = 10;

    //Health variables
    int maxHealth = 100;
    public int currentHealth;
    public Health_Bar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = moveSpeed;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        mainCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
        TeleportPlayer();
    }


    void Movement()
    {
        //checks if the ship is moving or not
        bool isMoving = false;


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 move = new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
            transform.Translate(move);


            //for Rotating our ship to the left
            animator.SetInteger("Turn",-1);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 move = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            transform.Translate(move);


            //for Rotating our ship to the right
            animator.SetInteger("Turn", 1);
            isMoving = true;
        }
        if (!isMoving)
        {
            //for making the ship back to the ideal position
            animator.SetInteger("Turn", 0);
        }

    }


    private void TeleportPlayer()
    {
        Vector2 position = transform.position;
        // world is (-infinity , -infinity) to (+infinity, +infinity)
        //viewport is the area covered by the camera. (0,0) for bottom left and (1,1) for top right corner
        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(position);
        if(viewportPosition.x > 1.08f)
        {
            position.x = mainCamera.ViewportToWorldPoint(new Vector2(0, viewportPosition.y)).x;
        }
        else if(viewportPosition.x<-0.08f){
            position.x = mainCamera.ViewportToWorldPoint(new Vector2(1, viewportPosition.y)).x;

        }
        transform.position = position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Laser"))
        {
            Destroy(collision.gameObject);
            
            if (currentHealth > 0)
            {   
                currentHealth -= 20;
                healthBar.SetHealth(currentHealth);
                Debug.Log(currentHealth);if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
             
            }
            else if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public int GetMoveSpeed()
    {
        return moveSpeed;
    }
    public void SetMoveSpeed(int speed)
    {
        moveSpeed = speed;
    }
    public int GetNormalMoveSpeed()
    {
        return normalSpeed;
    }
    public void SetNormalSpeed(int speed)
    {
        normalSpeed = speed;
    }
    public int GetNewMoveSpeed()
    {
        return newSpeed;
    }
    public void SetNewSpeed(int speed)
    {
        newSpeed = speed;
    }
}



