using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    private bool isTripleShot;
    
    private GameObject projectileClone;
    [SerializeField] private float shootCooldown = 1f;
    private float lastShotTime;

    private void Start()
    {
        lastShotTime = -shootCooldown; //lastShotTime = -shootCooldown so that the player is able to shoot the projectile at 0 secs.
                                       //if lastShotTime is initizlized with 0, then we wont be able to shoot at the start of the game. We will have to wait for 1 sec before shooting.
        
        isTripleShot = false;// false from start
    
    }


    void Update()
    {
        FireProjectile();
    }

    void FireProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >=lastShotTime+shootCooldown && isTripleShot)//Time.time is how much time has been elapsed till the start of the game.
            //Fires 3 Projectiles if isTripleShot is set to true. 
        {
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0), player.transform.rotation);
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x + 0.50f, player.transform.position.y + .25f , 0), player.transform.rotation);
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x - 0.50f, player.transform.position.y + .25f , 0), player.transform.rotation);
            lastShotTime = Time.time;

        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= lastShotTime + shootCooldown )
        {
            projectileClone = Instantiate(projectile, new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0), player.transform.rotation);

            lastShotTime = Time.time;

        }
    }
    public bool GetIsTripleShot()
    {
        return isTripleShot;
    }
    public void SetIsTripleShot(bool istriple)
    {
        isTripleShot = istriple;
    }
}
