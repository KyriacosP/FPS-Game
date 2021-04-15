using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyprefab;
    public GameObject guardprefab;
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
    public int deadguards;
    public EnemyController enemycontr;
   public GuardController guardcontr;

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
        generateGuards();
        Check();
        
    }

    void Update(){
        Check();
    }

    void Check(){
            if(enemycontr.deadenemies==5){
                generateEnemies();
                enemycontr.deadenemies=0;
            }
            
            if(guardcontr.deadguards==4){
                generateGuards();
                guardcontr.deadguards=0;
            }
    }

    void generateEnemies()
    {
        for (int i = 0; i < 6; i++){
            //Generate random x,z,y position on the terrain
            if((player.position.x + 200) > terrainWidth)
                maxx=terrainWidth;
            else
                maxx=player.position.x + 200;
            if((player.position.z + 200) > terrainLength)
                maxz=terrainLength;
            else
                maxz=player.position.z + 200;
            float randX = UnityEngine.Random.Range(player.position.x, maxx);
            float randZ = UnityEngine.Random.Range(player.position.z, maxz);
            //float randX = UnityEngine.Random.Range(xTerrainPos, xTerrainPos + terrainWidth);
            //float randZ = UnityEngine.Random.Range(zTerrainPos, zTerrainPos + terrainLength);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
            yVal = yVal + yOffset;
            //Generate the Prefab on the generated position
            GameObject objInstance = (GameObject)Instantiate(enemyprefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
        }

    }
    void generateGuards(){
        for (int i = 0; i < 4; i++){
            //Generate random x,z,y position on the terrain
            float randX = UnityEngine.Random.Range(xTerrainPos, xTerrainPos + terrainWidth);
            float randZ = UnityEngine.Random.Range(zTerrainPos, zTerrainPos + terrainLength);
            float yVal = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
            yVal = yVal + yOffset;
            //Generate the Prefab on the generated position
            GameObject objInstance = (GameObject)Instantiate(guardprefab, new Vector3(randX, yVal, randZ), Quaternion.identity);
        }   
    }
    
}
