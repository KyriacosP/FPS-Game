using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GuardState {
    PROTECT,
    CHASE,
    ATTACK
}

public class GuardController : MonoBehaviour {

    private GuardAnimator guard_Anim;
    private NavMeshAgent navAgent;

    private GuardState guard_State;

    public int health;
    public float attack_Distance = 1.8f;
    public float treasureDistance;

    private Transform player;
    private Transform treasure;

    void Awake() {
        guard_Anim = GetComponent<GuardAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        treasure = GameObject.FindWithTag("Treasure").transform;

    }

    void Start () {
        guard_State = GuardState.PROTECT;
	}
	
	// Update is called once per frame
	void Update () {
        if(health<=0)
            Death();
        else{
        if (guard_State == GuardState.PROTECT)
            Protect();
        if (guard_State == GuardState.CHASE)
            Chase();
        if (guard_State == GuardState.ATTACK)
            Attack();
        }
    }

     void Death(){
        guard_Anim.Dead(true);
     }
    

    void Protect() {
        navAgent.isStopped = false;
        navAgent.SetDestination(treasure.position);
        guard_Anim.Run(false);

        if(Vector3.Distance(treasure.position, player.position) <= treasureDistance) {
             guard_State = GuardState.CHASE;
        }

        if(Vector3.Distance(transform.position, treasure.position) > 4){
            // if(Vector3.Distance(treasure.position, player.position) <= treasureDistance){
            //     guard_Anim.Walk(false);
            //     guard_State = GuardState.CHASE;
            // } else 
                guard_Anim.Walk(true);
        }else{
            guard_Anim.Walk(false);
            navAgent.isStopped = true;
        }   
    }

    void Chase(){
        navAgent.isStopped = false;
        navAgent.speed = 2;
        navAgent.SetDestination(player.position);
        guard_Anim.Run(true);
        
        if(Vector3.Distance(transform.position, player.position) <= attack_Distance) {
            guard_Anim.Run(false);
            guard_State = GuardState.ATTACK;
        } 
        else if(Vector3.Distance(transform.position, player.position)> attack_Distance && 
        Vector3.Distance(treasure.position, player.position) > treasureDistance) {
            guard_State = GuardState.PROTECT;
        }

    }

    void Attack() {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        transform.LookAt(player);
        guard_Anim.Attack(true);

        if(Vector3.Distance(transform.position, player.position) > attack_Distance) {
            guard_Anim.Attack(false);
            if(Vector3.Distance(treasure.position, player.position)<= treasureDistance){
                guard_State = GuardState.CHASE;
            }else{
                guard_State = GuardState.PROTECT;
            }  
        }
    } 


    public GuardState GuardState {
        get; set;
    }

} 
