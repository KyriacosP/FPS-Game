using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health=100;
    public int damage=2;
    public PlayerUI playerStats;
    public int maxHealth=100;
    public GameObject shelter;
    public float yOffset = 0.5f;
    public Transform player;
    public bool randombool=true;
    public int level;
    public GameObject ImageLose;
    void Start()
    {
        playerStats.SetMaxHealth(maxHealth);
        player = GameObject.FindWithTag("Player").transform;
        ImageLose.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Guard")
                health-=damage;    
    }
    
    void Update()
    {
        // if(randombool){
        //     if(health<=4)
        //     spawnShelter();
        // }
        if(health<=0)
            RestartGame();
        playerStats.SetHealth(health);
    }

    IEnumerator reloadgame()
    {
         yield return new WaitForSeconds(2);
         SceneManager.LoadScene("Level" + level.ToString());
         
    }
    void RestartGame() {
        ImageLose.gameObject.SetActive(true);
        StartCoroutine(reloadgame());
        
    }
    
    // void spawnShelter(){
    //     float randX = UnityEngine.Random.Range(player.position.x, player.position.x+30);
    //     float randZ = UnityEngine.Random.Range(player.position.z, player.position.x+30);
    //     // float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
    //     int xInt = (int)randX;
    //     int zInt = (int)randZ;
    //     float yVal = Terrain.activeTerrain.terrainData.GetHeight(xInt,zInt);
       
    //     yVal = yVal + yOffset;
    //     //Generate the Prefab on the generated position
    //     GameObject objInstance = (GameObject)Instantiate(shelter, new Vector3(randX, yVal, randZ), Quaternion.identity);
    //     randombool=false;
    // }

    public void Damage(int damage)
    {
        health -= damage;
    }


}
