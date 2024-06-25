using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] Animator animator;
    private float destroyTime;//Length of the "Enemy_Destroyed" clip.
    public GameObject enemy;
    public GameObject enemyProjectile;
    public GameObject enemyProjectileClone;
    PolygonCollider2D polygonCollider2D;

        
    // Start is called before the first frame update
    void Start()
    {
        UpdateAnimClipTimes(); //Gets the length of all the animations in the animator attached to the game object at the start of the game.
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyFireProjectile();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);//Destroys the laser that hit the enemy
            StartCoroutine(DestroyEnemy());
        }
    }
    IEnumerator DestroyEnemy()//Coroutine for playing the animation of the destroying the enemy before it gets destroyed.
                              //"destroyTime" is the length of the clip named "Enemy_Destroyed from the animator"
    {
        animator.SetBool("Destroy", true);
        polygonCollider2D.isTrigger = true;//Setting the collider to trigger so that the other lasers won't interact with the previous gameobject thats still being destroyed.
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);

    }
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;//Gets all the clips in the animator
        foreach (AnimationClip clip  in clips)
        {
            switch (clip.name)
            {
                case "Enemy_Destroyed":
                    destroyTime = clip.length;//sets the variable to the length of the clip 
                    break;
            }
        }
    }

    void EnemyFireProjectile()
    {
        if (Random.Range(0f,1000)<1)
        {
        enemyProjectileClone = Instantiate(enemyProjectile, transform.position, enemy.transform.rotation);
        }
    }

}
/*new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1f, 0)*/