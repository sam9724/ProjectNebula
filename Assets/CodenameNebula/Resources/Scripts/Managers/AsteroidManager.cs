using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidManager
{
    #region Singleton
    public static AstroidManager Instance
    {
        get
        {
            return instance ?? (instance = new AstroidManager());
        }
    }

    private static AstroidManager instance;

    private AstroidManager() { }
    #endregion

    [SerializeField]
    private GameObject blockPrefab;
    public int a = 100, b = 50, c = 100;
    [SerializeField]
    private float noiseScale = 2.07f;
    public int seed = 65766;
    [SerializeField, Range(0, 1)]
    private float threshold = 0.77f;
    [SerializeField]
    private float tumble;
    GameObject astroidParent;
    List<GameObject> asteroidList = new List<GameObject>();

    public void Initialize()
    {
        blockPrefab = Resources.Load<GameObject>("Prefabs/Asteroids/Asteroid_1");
        astroidParent = new GameObject("AstroidParent");
        makeRocks();
    }

    void makeRocks()
    {
        for (int x = 0; x < a; x++)
        {
            for (int y = 0; y < b; y++)
            {
                for (int z = 0; z < c; z++)
                {

                    float noiseValue = Perlin3D(x * noiseScale, y * noiseScale, z * noiseScale);
                    if (noiseValue >= threshold)
                    {

                        //Debug.Log((Physics.CheckSphere(new Vector3(x, y, z), checksphere)));
                        //if (!(Physics.CheckSphere(new Vector3(x, y, z), checksphere)))
                        //{
                        GameObject go = GameObject.Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.identity, astroidParent.transform);
                        asteroidList.Add(go);
                        go.GetComponent<Rigidbody>().angularVelocity = Vector3.one * noiseValue * tumble;

                        //}


                    }
                }
            }
        }

        Debug.Log("Total asteroids: " + asteroidList.Count);
    }
    public float Perlin3D(float x, float y, float z)
    {
        x += seed;
        y += seed;
        z += seed;
        float ab = Mathf.PerlinNoise(x, y);
        float bc = Mathf.PerlinNoise(y, z);
        float ac = Mathf.PerlinNoise(x, z);
        float abc = ab + bc + ac;
        return abc / 3f;
    }

}
