using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{

    /*    public Player_Manager player_Manager;
           public Health_Bar health_bar;
    */    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -5 * Time.deltaTime, 0));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))//Destroys the laser when it hits any surface with the tag "Boundary".
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
       
        /*if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (player_Manager.currentHealth <= 0) 
            { 
            Destroy(collision.gameObject);
            }
            else
            {
                player_Manager.currentHealth -= 20;
*//*                health_bar.SetHealth(player_Manager.currentHealth); 
*//*            }
        }*/
    }
    

}
