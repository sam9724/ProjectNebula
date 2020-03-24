using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour
{
    #region Singleton
    private static ProjectileManager instance;
    private ProjectileManager() { }
    public static ProjectileManager Instance { get { return instance ?? (instance = new ProjectileManager()); } }
    #endregion

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
