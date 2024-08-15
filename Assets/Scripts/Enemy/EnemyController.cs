using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    GUARD,
    PATROL,
    CHASE,
    DEAD
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IHurt
{
    private NavMeshAgent agent;
    private EnemyState enemyState;
    private Animator anim;
    private CharacterState characterState;



    [Header("Basic Settings")]
    public float sightRadius;
    public bool isGuard;
    private float speed;
    protected GameObject attackTarget;
    public float lookAtTime;
    private float remainLookAtTime;
    private float lastAttackTime;
    private Quaternion guardRotation;
    private Collider coll;

    [Header("Patrol State")]
    public float patrolRange;
    private Vector3 wayPoint;
    private Vector3 guardPosition;

    [Header("Reward Info")]
    public GameObject rewardPrefab;

    // bool for animation
    bool isWalk;
    bool isChase;
    bool isFollow;
    bool isDead;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        characterState = GetComponent<CharacterState>();
        guardPosition = transform.position;
        guardRotation = transform.rotation;
        speed = agent.speed;
        remainLookAtTime = lookAtTime;
    }

    private void Update()
    {
        if (characterState.currentHealth == 0)
        { 
            isDead = true;
        }
        SwitchState();
        SwitchAnimation();
        lastAttackTime -= Time.deltaTime;
    }

    private void Start()
    {
        if (isGuard)
        {
            enemyState = EnemyState.GUARD;
        }
        else
        {
            enemyState = EnemyState.PATROL;
            GetNewWayPoint();
        }
    }

    void SwitchState()
    {
        if (isDead)
        { 
            enemyState = EnemyState.DEAD;
        }
        else if (FoundPlayer())
        {
            enemyState = EnemyState.CHASE;
            Debug.Log("Found Player");
        }
        switch (enemyState)
        { 
            case EnemyState.GUARD:
                GuardState();
                break;
            case EnemyState.PATROL:
                PatrolState();
                break;
            case EnemyState.CHASE:
                ChaseState();
                break;
            case EnemyState.DEAD:
                DeadState();
                break;
        }
    }

    bool FoundPlayer()
    {

        var colliders = Physics.OverlapSphere(transform.position, sightRadius);

        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            { 
                attackTarget = target.gameObject;
                return true;
            }
        }
        attackTarget = null;
        return false;
    }

    bool TargetInAttackRange()
    {
        if (attackTarget != null)
        { 
            return Vector3.Distance(transform.position, attackTarget.transform.position) <= characterState.attackData.attackRange;
        }
        else
        {
            return false;
        }
    }

    bool TargetInSkillRange()
    {
        if (attackTarget != null)
        { 
            return Vector3.Distance(transform.position, attackTarget.transform.position) <= characterState.attackData.skillRange;
        }
        else
        {
            return false;
        }
    }

    void SwitchAnimation()
    { 
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
        anim.SetBool("Critical", characterState.isCritical);
        anim.SetBool("Death", isDead);
    }

    private void DeadState()
    {
        coll.enabled = false;
        agent.enabled = false;
        Destroy(gameObject, 1f);
    }
    private void GuardState()
    { 
        isChase = false;
        if (transform.position != guardPosition)
        { 
            isWalk = true;
            agent.isStopped = false;
            agent.destination = guardPosition;

            if (Vector3.SqrMagnitude(guardPosition - transform.position) <= agent.stoppingDistance)
            { 
                isWalk = false;
                transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, 0.01f);
            }
        }
    }

    private void PatrolState()
    { 
        isChase = false;
        agent.speed = speed * 0.5f;

        if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
        {
            isWalk = false;
            if (remainLookAtTime > 0)
            {
                remainLookAtTime -= Time.deltaTime;
            }
            else
            {
                GetNewWayPoint();
            }

        }
        else
        { 
            isWalk = true;
            agent.destination = wayPoint;
        }
    }
    private void ChaseState()
    {
        isWalk = false;
        isChase = true;
        agent.speed = speed;

        if (!FoundPlayer())
        {

            isFollow = false;
            if (remainLookAtTime > 0)
            {
                agent.destination = transform.position;
                remainLookAtTime -= Time.deltaTime;
            }
            else if (isGuard)
            {
                enemyState = EnemyState.GUARD;
            }
            else
            {
                enemyState = EnemyState.PATROL;
                GetNewWayPoint();
            }
        }
        else
        {
            isFollow = true;
            agent.isStopped = false;
            agent.destination = attackTarget.transform.position;
        }

        if (TargetInAttackRange() || TargetInSkillRange())
        { 
            isFollow = false;
            agent.isStopped = true;

            if (lastAttackTime < 0)
            { 
                lastAttackTime = characterState.attackData.coolDown;
                characterState.isCritical = Random.value < characterState.attackData.criticalChance;
                Attack();
            }
        }
    }

    void Attack()
    { 
        transform.LookAt(attackTarget.transform);
        if (TargetInAttackRange())
        { 
            anim.SetTrigger("Attack");
        }

        if (TargetInSkillRange())
        { 
            anim.SetTrigger("Skill");
        }
    }
    void GetNewWayPoint()
    { 
        remainLookAtTime = lookAtTime;
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);

        Vector3 randomPoint = new Vector3(guardPosition.x + randomX, transform.position.y, guardPosition.z + randomZ);

        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    private void Hit()
    {
        if (attackTarget != null)
        {
            Debug.Log("Hitting: " + attackTarget.name);
            var targetState = attackTarget.GetComponent<CharacterState>();
            targetState.TakeDamage(characterState, targetState);
        }
    }

    public void Hurt(int damage, IStateMachineOwner attacker)
    {
        damage = Mathf.Max(damage - characterState.currentDefence, 0);
        characterState.currentHealth = Mathf.Max(characterState.currentHealth - damage, 0);
        anim.SetTrigger("Hit");

        Debug.Log("Enemy Health: " + characterState.currentHealth);

        if (characterState.currentHealth == 0)
        {
            GameManager.Instance.playerState.characterData.UpdateExp(characterState.characterData.killPoint);
            QuestManager.Instance.UpdateQuestProgress(this.name, 1);
            if (rewardPrefab != null)
            {
                Instantiate(rewardPrefab, transform.position, transform.rotation);
            }
        }
    }
}
