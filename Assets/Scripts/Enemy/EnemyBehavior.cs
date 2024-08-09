using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IHurt
{
    private Animator animator;
    public Transform player; // Reference to the player's transform
    public float moveSpeed = 10f; // Speed at which the enemy moves
    public float minDistance = 5f; // Minimum distance to maintain from the player
    public float detectionRange = 10f; // Range within which the enemy detects the player
    public float damageAmount = 20; // Damage dealt to the player upon collision
    public float enemyHealth = 100; // Health of the enemy

    public GameObject lootPrefab; // Reference to the loot prefab

    void Start()
    {
        //animator = GetComponent<Animator>();
        // if (player == null)
        // {
        //     // Find the player by tag if not assigned
        //     GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        //     if (playerObj != null)
        //     {
        //         player = playerObj.transform;
        //     }
        // }
    }

    void Update()
    {
        // if (player == null) return; // Exit if player is not assigned

        // float step = moveSpeed * Time.deltaTime;
        // float distance = Vector3.Distance(transform.position, player.position);

        // if (distance < detectionRange && enemyHealth > 0)
        // {
        //     if (distance > minDistance)
        //     {
        //         transform.LookAt(player);
        //         //animator.SetInteger("animStat", 1);
        //         transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        //     }
        //     else
        //     {
        //         //animator.SetInteger("animStat", 5);
        //     }
        // }
        if (enemyHealth <= 0)
        {
            Die();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemyHealth > 0)
        {
            var playerHealth = other.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    public bool Hurt(float damage, IStateMachineOwner attacker)
    {
        Debug.Log("Hurt called. Damage: " + damage); // Debug
        enemyHealth -= damage;
        Debug.Log("Enemy Health: " + enemyHealth); // Debug
        if (enemyHealth <= 0)
        {
            Die();
            return true; // Return true if the enemy is dead
        }
        return false; // Return false if the enemy is still alive
    }

    private void Die()
    {
        // Play death animation or effect
        Debug.Log("Enemy died");
        //animator.SetInteger("animStat", 4);
        Destroy(gameObject);
        // Drop loot
        if (lootPrefab != null)
        {
            Vector3 lootPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(lootPrefab, lootPos, transform.rotation);
        }
        // Vector3 diePos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        // transform.position = diePos;

        GameManager.Instance.playerState.characterData.UpdateExp(30);
        QuestManager.Instance.UpdateQuestProgress(this.name, 1);
    }
}