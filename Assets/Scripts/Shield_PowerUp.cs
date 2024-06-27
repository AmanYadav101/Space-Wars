using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_PowerUp : MonoBehaviour
{
     Player_Manager player_manager;
    // Start is called before the first frame update
    void Start()
    {
        player_manager = GameObject.FindObjectOfType<Player_Manager>();

        /*        player_manager.SetIsInvincible(false);
        */
        
    }

    private void Awake()
    {
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ShieldPowerUpCoroutine());
        }
    }
    IEnumerator ShieldPowerUpCoroutine()//CoRoutine for setting the isInvincible bool in the player_manager to true and false after a couple of secs and destroy it afterwards.
    {

        player_manager.SetIsInvincible(true);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(4f);
        player_manager.SetIsInvincible(false);
     
        Destroy(gameObject);
    }
}
