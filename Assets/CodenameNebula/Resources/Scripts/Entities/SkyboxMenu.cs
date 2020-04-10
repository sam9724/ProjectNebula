using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMenu : MonoBehaviour
{
    float rotateSpeed = 0.5f;
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }
}
