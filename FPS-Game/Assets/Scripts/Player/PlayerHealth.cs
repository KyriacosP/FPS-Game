using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health=100;
    public int damage=2;
    public PlayerUI playerStats;
    public int maxHealth=100;


    void Start()
    {
        playerStats.SetMaxHealth(maxHealth);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Guard")
                health-=damage;    
    }
    
    void Update()
    {
        if(health==0)
            RestartGame();
        playerStats.SetHealth(health);
    }

    void RestartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    

   
}
