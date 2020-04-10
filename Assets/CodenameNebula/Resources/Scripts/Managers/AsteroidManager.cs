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
    public int a = 400, b = 100, c = 1000;
    [SerializeField]
    private float noiseScale = 2.07f;
    public int seed = 65766;
    [SerializeField, Range(0, 1)]
    private float threshold = 0.86f;
    [SerializeField]
    private float tumble;
    GameObject astroidParent;
    List<GameObject> asteroidList = new List<GameObject>();
    List<string> astroidType = new List<string> { "Asteroid_1", "Asteroid_2", "Asteroid_3" };
    GameObject[] astroidPrefabs;
    int specialNumber;
    int scaleNumber;

    public void Initialize()
    {
        astroidPrefabs = new GameObject[astroidType.Count];
        for (int i = 0; i < astroidType.Count; i++)
        {
            astroidPrefabs[i] = Resources.Load<GameObject>("Prefabs/Asteroids/" + astroidType[i]);
        }
        astroidParent = new GameObject("AstroidParent");
        makeRocks();
    }

    public void DestroyAsteroid(GameObject go)
    {
        GameObject.Destroy(go);
        asteroidList.Remove(go);
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
                    specialNumber = (int)(noiseValue * 1000) % 10;
                    scaleNumber = (int)(noiseValue * 100) %10;
                    if (noiseValue >= threshold)
                    {

                        //Debug.Log((Physics.CheckSphere(new Vector3(x, y, z), checksphere)));
                        //if (!(Physics.CheckSphere(new Vector3(x, y, z), checksphere)))
                        //{

                        if (specialNumber >= 0 && specialNumber <= 3)
                        {
                            blockPrefab = astroidPrefabs[0];
                        }
                        else if (specialNumber >= 4 && specialNumber <= 6)
                        {
                            blockPrefab = astroidPrefabs[1];
                        }
                        else if (specialNumber >= 7 && specialNumber<= 9)
                        {
                            blockPrefab = astroidPrefabs[2];
                        }
                        else
                        {
                            blockPrefab = astroidPrefabs[0];
                        }

                        ////
                        ///

                        if (scaleNumber == 0 && scaleNumber <= 2)
                        {
                            blockPrefab.transform.localScale = Vector3.one;
                        }
                        else if (scaleNumber == 3 && scaleNumber <= 5)
                        {
                            blockPrefab.transform.localScale = Vector3.one*1.5f;
                        }
                        else if (scaleNumber ==6)
                        {
                            blockPrefab.transform.localScale = Vector3.one*2;
                        }
                        else if (scaleNumber == 7 && scaleNumber <= 8)
                        {
                            blockPrefab.transform.localScale = Vector3.one * 2.5f;
                        }
                        else if (scaleNumber == 9)
                        {
                            blockPrefab.transform.localScale = Vector3.one * 3;
                        }
                        else
                        {
                            blockPrefab.transform.localScale = Vector3.one;
                        }

                        GameObject go = GameObject.Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.identity, astroidParent.transform);
                        go.AddComponent<Asteroid>().Initialize();
                        asteroidList.Add(go);
                        
                        //go.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;

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
