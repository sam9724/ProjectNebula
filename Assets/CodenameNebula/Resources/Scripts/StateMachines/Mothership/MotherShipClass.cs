using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MotherShipClass 
{
    static public Transform player;
    static public Transform motherShip;

    static public float playerDetectionRange = 225f;
    static public float rotateSpeed = 5f;
    static public float MovementSpeed = 5f;


    static public Transform MinionsSpawnLocation;

    static public float xMin = 0;
    static public float xMax = 0;
    static public float yMin = 0;
    static public float yMax = 0;
    static public float zMin = 0;
    static public float zMax = 0;
}