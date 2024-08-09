using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLootBehavior : MonoBehaviour
{
    public int healthAmount = 20;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     transform.Rotate(Vector3.forward, 90 * Time.deltaTime);

     if (transform.position.y < Random.Range(130, 150))
     {
         Destroy(gameObject.GetComponent<Rigidbody>());
     }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var characterState = other.GetComponent<CharacterState>();
            if (characterState != null)
            {
                characterState.Heal(healthAmount);
                
                Destroy(gameObject);
            }
        }
    }
}
