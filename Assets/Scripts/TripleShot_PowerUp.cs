using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot_PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Player_Manager player_manager;
    SpawnManager spawn_manager;
    private void Awake()
    {
        player_manager = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Manager>();
        spawn_manager = GameObject.FindFirstObjectByType<SpawnManager>().GetComponent<SpawnManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TripleShotPowerUpCoroutine());

        }
    }
    IEnumerator TripleShotPowerUpCoroutine()
    {
        spawn_manager.SetIsTripleShot(true);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(4f);
        spawn_manager.SetIsTripleShot(false);
        Destroy(gameObject);


    }
}
