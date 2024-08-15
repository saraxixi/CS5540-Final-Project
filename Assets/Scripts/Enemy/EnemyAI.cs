using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
     public enum FSMStates
    {
        IDEL,
        PATROL,
        CHASE,
        ATTACK,
        DEAD
    }

    public float attackDistance = 5;
    public float chaseDistance = 50;
    public float enemySpeed = 5;
    public GameObject player;
     float elapsedTime = 0;

    GameObject[] wanderPoints;
    Vector3 nextDestination;

    Animator anim;

    float distanceToPlayer;
    int currentDestinationIndex = 0;
    NavMeshAgent agent;

    public FSMStates currentState;


    void Start()
    {
        // wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        Initialize();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Distance to player: " + distanceToPlayer);
        switch(currentState)
        {
            case FSMStates.IDEL:
                UpdateIdelState();
                break;
            case FSMStates.PATROL:
                UpdatePatrolState();
                break;
            case FSMStates.CHASE:
                UpdateChaseState();
                break;
            case FSMStates.ATTACK:
                UpdateAttackState();
                break;
            case FSMStates.DEAD:
                UpdateDeadState();
                break;
        }
        elapsedTime += Time.deltaTime;
    }

     void Initialize()
    {
        currentState = FSMStates.IDEL;
        // FindNextPoint();
    }

    void UpdateIdelState()
    {
        anim.SetInteger("EnemyAnimState", 0);
    }

    void UpdatePatrolState()
    {
        agent.stoppingDistance = 0;
        anim.SetInteger("EnemyAnimState", 1);
        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.CHASE;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }

     void UpdateChaseState()
    {
        
        anim.SetInteger("EnemyAnimState", 2);
        nextDestination = player.transform.position;
        agent.stoppingDistance = 0;
        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.ATTACK;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.PATROL;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }

     void UpdateAttackState()
    {
        if (!GameManager.isGameOver)
        {
            nextDestination = player.transform.position;
            agent.stoppingDistance = attackDistance;
        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.ATTACK;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.CHASE;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.PATROL;
        }

        FaceTarget(nextDestination);

        anim.SetInteger("EnemyAnimState", 3);
        }
    }

    void UpdateDeadState()
    {
        Debug.Log("Dead State");
        anim.SetInteger("EnemyAnimState", 4);
        Destroy(gameObject, 2);
    }


      void FindNextPoint()
      {
          nextDestination = wanderPoints[currentDestinationIndex].transform.position;

          currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
      }

    void FaceTarget(Vector3 target)
    {
         Vector3 direction = (target - transform.position).normalized;
         direction.y = 0;
         Quaternion lookRotation = Quaternion.LookRotation(direction);
         transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
    }

     void OnDrawGizmos()
     {
          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(transform.position, chaseDistance);
          Gizmos.color = Color.blue;
          Gizmos.DrawWireSphere(transform.position, attackDistance);
     }
}
