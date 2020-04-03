﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow
{
    #region Singleton
    private static GameFlow instance;
    private GameFlow() { }
    public static GameFlow Instance { get { return instance ?? (instance = new GameFlow()); } }
    #endregion


    public void Initialize()
    {
        //GameSetup.gs = GameObject.FindObjectOfType<GameSetup>();
        //UIManager.Instance.Initialize();
        //InputManager.Instance.Initialize();
        PlayerManager.Instance.Initialize();
        EnemyManager.Instance.Initialize();
    }

    public void PostInitialize()
    {
        //UIManager.Instance.PostInitialize();
        //InputManager.Instance.PostInitialize();
        PlayerManager.Instance.PostInitialize();
        EnemyManager.Instance.PostInitialize();

    }

    public void PhysicsRefresh(float fdt)
    {
        //UIManager.Instance.PhysicsRefresh();
        //InputManager.Instance.PhysicsRefresh(fdt);
        PlayerManager.Instance.PhysicsRefresh(fdt);
        EnemyManager.Instance.PhysicsRefresh(fdt);

    }

    public void Refresh(float dt)
    {
        //UIManager.Instance.Refresh();
        //InputManager.Instance.Refresh(dt);
        PlayerManager.Instance.Refresh(dt);
        EnemyManager.Instance.Refresh(dt);

    }


}
