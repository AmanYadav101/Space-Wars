using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Powerup : MonoBehaviour
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SpeedPowerupCoroutine());

        }
    }
    IEnumerator SpeedPowerupCoroutine()
    {
        audioManager.PlaySFX(audioManager.powerupPickupSFX);
        player_manager.SetMoveSpeed(player_manager.GetNewMoveSpeed());
        audioManager.PlaySFX(audioManager.thrustersSFX);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(4f);
        player_manager.SetMoveSpeed(player_manager.GetNormalMoveSpeed());
        Destroy(gameObject);


    }
}
