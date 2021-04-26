using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyprefab;
    public Terrain terrain;
    public float yOffset = 0.5f;

    private float terrainWidth;
    private float terrainLength;

    private float xTerrainPos;
    private float zTerrainPos;
    public Transform player;
    private float maxx;
    private float maxz;
    public int deadenemies;
    public EnemyController enemycontr;

    void Start()
    {
        //Get terrain size
        terrainWidth = terrain.terrainData.size.x;
        terrainLength = terrain.terrainData.size.z;

        //Get terrain position
        xTerrainPos = terrain.transform.position.x;
        zTerrainPos = terrain.transform.position.z;
        player = GameObject.FindWithTag("Player").transform;
        generateEnemies();
        Check();
        
    }

    void Update(){
        Check();
    }

    void Check(){
            if(enemycontr.deadenemies==3){
                generateEnemies();
                enemycontr.deadenemies=0;
            }
            
    }

    void generateEnemies()
    {
        for (int i = 0; i < 6; i++){
            //Generate random x,z,y position on the terrain
            if((player.position.x + 100) > terrainWidth)
                maxx=terrainWidth;
            else
                maxx=player.position.x + 100;
            if((player.position.z + 100) > terrainLength)
                maxz=terrainLength;
            else
                maxz=player.position.z + 100;
            float randX = UnityEngine.Random.Range(player.position.x, maxx);
            float randZ = UnityEngine.Random.Range(player.position.z, maxz);
            //float randX = UnityEngine.Random.Range(xTerrainPos, xTerrainPos + terrainWidth);
            //float randZ = UnityEngine.Random.Range(zTerrainPos, zTerrainPos + terrainLength);
            int xInt = (int)randX;
            int zInt = (int)randZ;
            float yVal = terrain.terrainData.GetHeight(xInt,zInt);
            yVal = yVal + yOffset;
            //Generate the Prefab on the generated position
            GameObject objInstance = (GameObject)Instantiate(enemyprefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
        }

    }
    
}
