using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyShooterState
{
    PATROL,
    CHASE,
    CHASE_ATTACK,
    ATTACK,
    REST
}

public class EnemyShooterController : MonoBehaviour
{

    public EnemyShooterAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    public EnemyShooterState enemy_State;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;

    public int health = 100;
    public float chase_Distance = 30f;
    public float chase_attack_Distance = 20f;
    public float attack_Distance = 10f;
    public float running_accuracy = 0.5f;
    public float accuracy = 0.8f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    public float patrol_Timer;
    public float LastRest;

    private Transform player;

    public GameObject attack_Point;
    public float RestTime;
    public bool iwashit;

    public EnemyUI enemyStats;
    public int maxHealth;
    public int deadenemies = 0;

    void Awake()
    {
        enemy_Anim = GetComponent<EnemyShooterAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

    }
    void Start()
    {
        enemyStats.SetMaxHealth(maxHealth);
        enemy_State = EnemyShooterState.PATROL;
        patrol_Timer = patrol_For_This_Time;
    }

    // Update is called once per frame
    void Update()
    {
        enemyStats.SetHealth(health);
        if (health <= 0)
            Death();
        else
        {
            if (enemy_State == EnemyShooterState.PATROL)
            {
                Patrol();
            }

            if (enemy_State == EnemyShooterState.CHASE)
            {
                Chase();
            }

            if (enemy_State == EnemyShooterState.CHASE_ATTACK)
            {
                ChaseAttack();
            }

            if (enemy_State == EnemyShooterState.ATTACK)
            {
                Attack();
            }
            if (enemy_State == EnemyShooterState.REST)
            {
                Rest();
            }
        }
    }

	void Death()
    {
        enemy_Anim.Walk(false);
        enemy_Anim.Dead(true);
        StartCoroutine(LateCall());
    }

    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(7);
        gameObject.SetActive(false);
        deadenemies += 1;
    }

   
    void Rest()
    {
        RestTime += Time.deltaTime;
        navAgent.isStopped = true;
        enemy_Anim.Rest(true);
        enemy_Anim.Walk(false);
        if (RestTime > LastRest + 30)
        {
            enemy_Anim.Rest(false);
            LastRest = RestTime;
            enemy_State = EnemyShooterState.PATROL;
        }
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= chase_Distance)
        {
            enemy_Anim.Rest(false);
            enemy_Anim.Run(true);
            LastRest = RestTime;
            enemy_State = EnemyShooterState.CHASE;
        }
        else if (distance <= chase_attack_Distance)
        {
            enemy_Anim.Rest(false);
            enemy_Anim.RunAttack(true);
            LastRest = RestTime;
            enemy_State = EnemyShooterState.CHASE_ATTACK;
        }
        else if (distance <= attack_Distance || iwashit)
        {
            enemy_Anim.Rest(false);
            enemy_Anim.Attack(true);
            LastRest = RestTime;
            enemy_State = EnemyShooterState.ATTACK;
        }
    }

    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;
        enemy_Anim.Walk(true);

        if (Vector3.Distance(transform.position, player.position) <= chase_Distance || iwashit)
        {
            StartCoroutine(hitcall());
            enemy_State = EnemyShooterState.CHASE;
        }

        RestTime += Time.deltaTime;
        if (RestTime > LastRest + 20)
        {
            enemy_State = EnemyShooterState.REST;
        }

        patrol_Timer += Time.deltaTime;
        if (patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();
            patrol_Timer = 0f;
        }

    }

    IEnumerator hitcall()
    {
        yield return new WaitForSeconds(4);
        iwashit = false;
    }

    void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = 2;
        navAgent.SetDestination(player.position);
        enemy_Anim.Walk(false);
        enemy_Anim.Run(true);
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= chase_attack_Distance)
        {
            enemy_Anim.Run(false);
            enemy_State = EnemyShooterState.CHASE_ATTACK;
        }
        else if (distance > chase_Distance)
        {
            enemy_Anim.Run(false);
            enemy_State = EnemyShooterState.PATROL;
        }

    }

    private void ChaseAttack()
    {
        navAgent.isStopped = false;
        navAgent.speed = 2;
        navAgent.SetDestination(player.position);
        enemy_Anim.Run(false);
        enemy_Anim.RunAttack(true);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attack_Distance)
        {
            enemy_Anim.RunAttack(false);
            enemy_State = EnemyShooterState.ATTACK;
        }
        else if (distance > chase_attack_Distance)
        {
            enemy_Anim.RunAttack(false);
            enemy_State = EnemyShooterState.CHASE;
        }
    }

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        //Vector3 direction = player.position - transform.position;
        //direction.y = 0;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3 * Time.deltaTime);
        enemy_Anim.Attack(true);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > attack_Distance)
        {
            if (distance < chase_attack_Distance)
            {
                enemy_Anim.Attack(false);
                enemy_State = EnemyShooterState.CHASE_ATTACK;
            }
            else if (distance < chase_Distance)
            {
                enemy_Anim.Attack(false);
                enemy_State = EnemyShooterState.CHASE;
            }
        }
    }

    void SetNewRandomDestination()
    {

        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);
        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);
        navAgent.SetDestination(navHit.position);
    }

    public EnemyState Enemy_State
    {
        get; set;
    }

}
