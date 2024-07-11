using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Animator animator; // Reference to the Animator component for playing animations
    bool isDestroyed =false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDestroyed) 
        { 
        transform.Translate(new Vector3(0, 15 * Time.deltaTime,0));
        }
        else if (isDestroyed)
        {
            transform.Translate(Vector3.zero);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundry"))//Destroys the laser when it hits any surface with the tag "Boundary".
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "BossLeftToRight" || collision.gameObject.tag == "Level2Boss" ||
           collision.gameObject.tag == "Level3Boss" || collision.gameObject.tag == "Level4Boss" ||
           collision.gameObject.tag == "Level5Boss" || collision.gameObject.tag == "Level6Boss" ||
           collision.gameObject.tag == "Level7Boss" || collision.gameObject.tag == "Level8Boss_1" ||
           collision.gameObject.tag == "Level8Boss_2" || collision.gameObject.tag == "Level9Boss_1" ||
           collision.gameObject.tag == "Level9Boss_2" || collision.gameObject.tag == "Level9Boss_3" ||
           collision.gameObject.tag == "Level10Boss_1" || collision.gameObject.tag == "Level10Boss_2" ||
           collision.gameObject.tag == "Level10Boss_3")
        {
Destroy(gameObject);
        }
    }
    private IEnumerator DestroyProjectile()
    {
        isDestroyed = true;
        // Trigger the "Destroy" animation if an Animator is attached
        if (animator != null)
        {
            animator.SetTrigger("Destroy");

            // Wait for the animation to complete
            yield return new WaitForSeconds(1);
        }

        // Destroy the GameObject after animation completes
        Destroy(gameObject);
    }

}
