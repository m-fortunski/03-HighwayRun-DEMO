using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField] GameObject TerrainPlane;
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 CurrentTerrainPosition=new Vector3(0,-1,0);
    [SerializeField] int CurrentTerrain=0;
    [SerializeField] int TerrainLength = 17;
    [SerializeField] float TerrainSize = 200;
    [SerializeField] float deltaTerrain;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        GenerateTerrain(3);
        deltaTerrain = TerrainSize / (TerrainLength - 1);
    }

    void GenerateTerrain(int num)
    {
        for (int j = 0; j < num; j++)
        {
            Player = GameObject.Find("Player");
            for (int i = 0; i < 3; i++)
            {
                GameObject New = Instantiate(TerrainPlane, CurrentTerrainPosition+new Vector3(-200+200*i,0,0), Quaternion.Euler(-90, 0, 0), this.gameObject.transform);
                New.AddComponent<MeshCollider>();
                New.name = (CurrentTerrain + "." + i);
            }
            CurrentTerrainPosition += new Vector3(0, 0, 200);

        }

    }

    //void RandomizeTerrain(GameObject terrain, int biomeType)
    //{
    //    Vector3[] vertices = terrain.GetComponent<MeshFilter>().mesh.vertices;
    //    for (int i = 0; i < terrainLength; i++)
    //    {
    //        for (int j = 0; j < TerrainLength; j++)
    //        {
    //            for (int k = 0; k < vertices.Length; k++)
    //            {
    //                if (vertices[k].transform.position == new Vector3(-deltaTerrain * (TerrainLength - 1) + j * deltaTerrain,0, -deltaTerrain * (TerrainLength - 1) + i * deltaTerrain){ }
    //            }



    //        }
    //    }

    //    terrain.GetComponent<MeshFilter>().mesh.vertices = vertices;



    //}



    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            child.transform.position = child.transform.position - Player.GetComponent<PlayerController>().MooveWorld;
        }

        foreach (Transform child in this.gameObject.transform)
        {
            if(Player.transform.position.z-child.transform.position.z > 200)
            {
                Destroy(child.gameObject);
            }
        }

        CurrentTerrainPosition -= Player.GetComponent<PlayerController>().MooveWorld;
        if(CurrentTerrainPosition.z- Player.transform.position.z < 400)
        {
            GenerateTerrain(1);

        }
    }
}
