using UnityEngine;
using System.Collections;

public class PlayerManager
{
    #region Singleton
    private static PlayerManager instance;
    private PlayerManager() { }
    public static PlayerManager Instance { get { return instance ?? (instance = new PlayerManager()); } }
    #endregion

    Pilot p1;
    Gunner p2;

    public void Initialize()
    {
        
    }

    public void PhysicsRefresh(float fdt)
    {
        
    }

    public void PostInitialize()
    {
        
    }

    public void Refresh(float dt)
    {
        
    }
    

}
