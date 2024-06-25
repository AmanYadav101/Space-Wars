using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Powerup : MonoBehaviour
{
    Player_Manager player_manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        player_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Manager>();
    }

    // Update is called once per frame
    void Update()
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
            StartCoroutine(SpeedPowerupCoroutine());
        }
    }
    IEnumerator SpeedPowerupCoroutine()
    {
        player_manager.SetMoveSpeed(player_manager.GetNewMoveSpeed());
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(4f);
       
        player_manager.SetMoveSpeed(player_manager.GetNormalMoveSpeed());
        Destroy(gameObject);


    }
}
