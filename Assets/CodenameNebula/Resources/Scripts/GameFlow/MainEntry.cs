using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour
{
    void Awake()
    {
        GameFlow.Instance.Initialize();
    }

    void Start()
    {
        GameFlow.Instance.PostInitialize();
    }

    void Update()
    {
        GameFlow.Instance.Refresh(Time.deltaTime);
    }

    void FixedUpdate()
    {
        GameFlow.Instance.PhysicsRefresh(Time.fixedDeltaTime);
    }
}
