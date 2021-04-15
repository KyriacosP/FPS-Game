using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    private Animator anim;

	void Awake () {
        anim = GetComponent<Animator>();	
	}
	
    public void Walk(bool walk) {
        anim.SetBool("Walk", walk);
    }

    public void Run(bool run) {
        anim.SetBool("Run", run);
    }

    public void Attack(bool attack) {
        anim.SetBool("Attack",attack);
    }

     public void Dead(bool dead) {
         anim.SetBool("Dead", dead);
     }

    public void Rest(bool rest) {
        anim.SetBool("Rest", rest);
    }
} 