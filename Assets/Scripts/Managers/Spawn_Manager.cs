using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    
    private GameObject projectileClone;
    [SerializeField] private float shootCooldown = 1f;
    private float lastShotTime;

    private void Start()
    {
        lastShotTime = -shootCooldown; //lastShotTime = -shootCooldown so that the player is able to shoot the projectile at 0 secs.
                                       //if lastShotTime is initizlized with 0, then we wont be able to shoot at the start of the game. We will have to wait for 1 sec before shooting.
    }


    void Update()
    {
        FireProjectile();
    }

    void FireProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >=lastShotTime+shootCooldown)//Time.time is how much time has been elapsed till the start of the game.
        {
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0), player.transform.rotation);
            
            lastShotTime = Time.time;

        }
    }
}
