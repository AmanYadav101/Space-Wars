using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_PowerUp : MonoBehaviour
{
    Player_Manager player_manager;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        
        player_manager = GameObject.FindObjectOfType<Player_Manager>();

        /*        player_manager = GameObject.FindAnyObjectByType<Player_Manager>().GetComponent<Player_Manager>();
        */
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            player_manager.HealthPowerUpCoRoutine();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
   /* void HealthPowerUpCoRoutine()
    {
        audioManager.PlaySFX(audioManager.powerupPickupSFX);
        if (player_manager.currentHealth < 100)
        {
            Debug.Log("Before " + player_manager.currentHealth);
            player_manager.currentHealth += 20;
            
            Debug.Log("After "  + player_manager.currentHealth);

        }
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        Destroy(gameObject);


    }*/
}
