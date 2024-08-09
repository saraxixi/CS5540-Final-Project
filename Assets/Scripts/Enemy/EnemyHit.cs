using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
   public GameObject enemyExpelled;
    // public static int EnemyShoot = 0;
    // public static int EnemyCount = 0;
    void Start()
    {
        // EnemyCount++;
    }

    // Update is called once per frame
    void Update()
    {
        // if (GameManager.isGameOver)
        // {
        //     EnemyShoot = 0;
        //     EnemyCount = 0;
        // }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.CompareTag("Projectile"))
    //     {
    //         DestroyEnemy();
    //     }
    // }

    // void DestroyEnemy()
    // {
    //     if (!GameManager.isGameOver)
    //     {
    //         Instantiate(enemyExpelled, transform.position, transform.rotation);

    //         gameObject.SetActive(false);

    //         Destroy(gameObject, 0.5f);
    //         EnemyShoot++;
    //         // Debug.Log("Enemy Count: " + EnemyShoot);
    //         if (EnemyShoot > 2)
    //         {
    //             Debug.Log("YOU WIN!");
    //             FindObjectOfType<GameManager>().LevelBeat();
    //         }
    //     }

    // }
}
