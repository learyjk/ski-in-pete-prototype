using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainSpawner : MonoBehaviour
{
    public GameObject[] terrainPrefabs;

    [Range(0, 300)] public int stepSet = 0; //Determines terrain speed.
    private float step = 0.1f;
    private float count = 0f;
    private float size;
    private int offset = 4;
    private float multiplier = 30;
    Vector3 terrainSpawnPoint;
    GameObject lastTerrainPiece;
    float lastPieceLength;


    void Start()
    {
        
        Camera cam = Camera.main;
        size = cam.orthographicSize;

        for (int i = (int)size * (-2); i < (int)size * 2 + offset; i++)
        {
            GameObject terrainPiece = Instantiate(terrainPrefabs[0], new Vector3(i, -i, 0f), Quaternion.identity);
            terrainPiece.transform.SetParent(this.transform);    
        }
        

        terrainSpawnPoint = new Vector3(size * 2 + (offset-1), size * (-2) - (offset - 1), 0f);
        lastTerrainPiece = this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 1).gameObject;
        lastPieceLength = lastTerrainPiece.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {   
        if (stepSet < 300) stepSet += 1;
        step = (float)stepSet/1000;
        step = Mathf.Round(step*10.0f)/10.0f;
        Debug.Log(step);
        SpawnNewTerrain();
        ShiftTerrain();
    }

    private void SpawnNewTerrain()
    {
        //Check if terrainSpawnPoint has no terrain
        if ((int)(lastTerrainPiece.transform.position.x + lastPieceLength) <= terrainSpawnPoint.x)
        {

            float rand = Random.Range(0, 10);

            if (rand > 0)
            {
                //Add newTerrainPiece
                GameObject newTerrainPiece;
                newTerrainPiece = Instantiate(terrainPrefabs[0], terrainSpawnPoint, Quaternion.identity);
                newTerrainPiece.transform.SetParent(this.transform);

                //Reset tracker for lastTerrainPiece
                lastTerrainPiece = newTerrainPiece;
                lastPieceLength = lastTerrainPiece.GetComponent<SpriteRenderer>().bounds.size.x;
            }
            else
            {
                SpawnChasm();   
            }
            
        }
        else
        {
            //Debug.Log("Terrain exists right now!");
            return;
        }
    }

    private void SpawnChasm()
    {
        //Add newTerrainPiece
        lastPieceLength = 0;
        GameObject newTerrainPiece;
        for (int i = 0; i < multiplier*step*4; i++)
        {
            if (i < (int)(multiplier*step))
            {
                //first half of new terrain
                newTerrainPiece = Instantiate(terrainPrefabs[2], new Vector2(terrainSpawnPoint.x + i, terrainSpawnPoint.y - i), Quaternion.identity);
                newTerrainPiece.transform.SetParent(this.transform);
                lastPieceLength += newTerrainPiece.GetComponent<SpriteRenderer>().bounds.size.x;
                if (i==0)
                {
                    lastTerrainPiece = newTerrainPiece;
                }
            }
            else
            {
                //second two thirds -> spawn regular terrain pieces equal to lenght of chasm to enusre player can land.
                newTerrainPiece = Instantiate(terrainPrefabs[1], new Vector2(terrainSpawnPoint.x + i, terrainSpawnPoint.y - i), Quaternion.identity);
                newTerrainPiece.transform.SetParent(this.transform);
                lastPieceLength += newTerrainPiece.GetComponent<SpriteRenderer>().bounds.size.x;
            } 
        }
    }

    private void ShiftTerrain()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            GameObject go = this.gameObject.transform.GetChild(i).gameObject;
            go.transform.Translate(-step, step, 0f);

            if (go.transform.position.x < size*(-2) - 2)
            {
                Destroy(go);
            }
        }
    }
}


        // if ((int)count % 1 == 0 && (int)count != 0)
        // {
        //     //terrain has moved by a whole step so add another terrain piece.
        //     float rand = Random.Range(0, 10);

        //     Debug.Log("Terrain spawn x = " + terrainSpawnPoint.x);
            
        //     GameObject terrainPiece;

        //     //check if a terrainPiece exists in spawn position.
        //     GameObject lastPiece = this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 1).gameObject;
        //     float lastPieceLength = lastPiece.GetComponent<SpriteRenderer>().bounds.size.x;

        //     Debug.Log("Last piece x + length = " + (lastPiece.transform.position.x + lastPieceLength));
        //     if ((lastPiece.transform.position.x + lastPieceLength) <= terrainSpawnPoint.x)
        //     {
        //         Debug.Log("Spawn now!");
        //     }

        //     if (rand > 0)
        //     {
        //         //regular terrain piece
        //         terrainPiece = Instantiate(terrainPrefabs[0], terrainSpawnPoint, Quaternion.identity);
        //         Debug.Log("Spawn");
        //     }
        //     else
        //     {
        //         terrainPiece = Instantiate(terrainPrefabs[1], terrainSpawnPoint, Quaternion.identity);
        //     }
            
        //     terrainPiece.transform.SetParent(this.transform);
        //     count = step;
        // }
        // else
        // {
        //     count += step;
        // }