using System.Collections;
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
        ProjectileFactory.Instance.Initialize();
        ParticleFactory.Instance.Initialize();
        PlayerManager.Instance.Initialize();
        EnemyManager.Instance.Initialize();
        ProjectileManager.Instance.Initialize();
        ParticleManager.Instance.Initialize();
        AstroidManager.Instance.Initialize();
        AudioManager.Instance.Initialize();
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
        InputManager.Instance.PhysicsRefresh();
        PlayerManager.Instance.PhysicsRefresh(fdt);
        EnemyManager.Instance.PhysicsRefresh(fdt);

    }

    public void Refresh(float dt)
    {
        //UIManager.Instance.Refresh();
        InputManager.Instance.Refresh();
        PlayerManager.Instance.Refresh(dt);
        EnemyManager.Instance.Refresh(dt);
        ProjectileManager.Instance.Refresh(dt);
        ParticleManager.Instance.Refresh(dt);

    }

    public void RestartFlow()
    {

    }


}
