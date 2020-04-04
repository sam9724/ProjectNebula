using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustItInArchitectureMotherShip : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    public float playerDetectionRange = 225f;
    public float rotateSpeed = 5f;
    public float MovementSpeed = 5f;
    float tempTimeCounter = 0f;
    bool isUp = false;
    public float idleMovement = 0.005f;
    public Transform MinionsSpawnLocation;
    public float DroneHealth = 0f;
    public float SeekerHealth = 0f;

    public Animator motherShipAnimator;

    public float xMin = 0;
    public float xMax = 0;
    public float yMin = 0;
    public float yMax = 0;
    public float zMin = 0;
    public float zMax = 0;

    Vector3 vtemp=Vector3.zero;

    //public Transform SwirlTransform;

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        MotherShipClass.player = player;
        MotherShipClass.motherShip = transform;
        MotherShipClass.playerDetectionRange = playerDetectionRange;
        MotherShipClass.rotateSpeed = rotateSpeed;
        MotherShipClass.MovementSpeed = MovementSpeed;
        MotherShipClass.MinionsSpawnLocation = MinionsSpawnLocation;
        MotherShipClass.xMin = xMin;
        MotherShipClass.xMax = xMax;
        MotherShipClass.yMin = yMin;
        MotherShipClass.yMax = yMax;
        MotherShipClass.zMin = zMin;
        MotherShipClass.zMax = zMax;

    }

    // Update is called once per frame
    void Update()
    {
       if(PlayerInRange())
        {
            motherShipAnimator.SetBool("PlayerInRange", true);
        }
        else
        {
            motherShipAnimator.SetBool("PlayerInRange", false);
        }

     
    }
    bool PlayerInRange()
    {
        if (Vector3.SqrMagnitude(transform.position - player.position) < playerDetectionRange)
        {
            return true;
        }
        return false;
    }
    void Idle()
    {
        tempTimeCounter += Time.deltaTime;
        if (tempTimeCounter > 0.4f)
        {
            isUp = !isUp;
            tempTimeCounter = 0;
        }
        if (isUp)
            transform.position += new Vector3(0, idleMovement, 0);
        else
            transform.position += new Vector3(0, -idleMovement, 0);

    }
    void Wander()
    {
        Debug.Log("Wander");
        if(!PlayerInRange())
        {
        Debug.Log("player not in range");
            if (Vector3.SqrMagnitude(transform.position - vtemp) < 25f)
                vtemp = Vector3.zero;

            if (vtemp == Vector3.zero) {
                Debug.Log("vtemp zero");
                vtemp = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), Random.Range(zMin, zMax));
                Debug.Log("vtemp : "+vtemp);
                FollowToAPoint(vtemp);               
            }
        }
    }
    void FollowPlayer()
    {

       SwirlWhileFollow();
        if(PlayerInRange())
            FollowToAPoint(player.position);
    }
    void SwirlWhileFollow()
    {
         
    }
    void FollowToAPoint(Vector3 Position)
    {
       
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Position - transform.position, rotateSpeed * Time.deltaTime, 0.0f));
            if (Vector3.SqrMagnitude(transform.position - Position) > 100f)
                transform.position = Vector3.MoveTowards(transform.position, Position, MovementSpeed * Time.deltaTime);
        
        //else
        //{
        //    transform.rotation = new Quaternion(0, 0, 0, 0);
        //}
    }
}
