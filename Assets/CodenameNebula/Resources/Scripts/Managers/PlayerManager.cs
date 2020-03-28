using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager
{
    #region Singleton
    private static PlayerManager instance;
    private PlayerManager() { }
    public static PlayerManager Instance { get { return instance ?? (instance = new PlayerManager()); } }
    #endregion

    public Pilot pilot;
    public Gunner gunner;


    public void Initialize()
    {

    }

    public void PostInitialize()
    {

    }

    public void PhysicsRefresh(float fdt)
    {
        
    }

    public void Refresh(float dt)
    {
        if (pilot)
            pilot.Refresh(dt);

        if (gunner)
            gunner.Refresh(dt);
    }
    

}
