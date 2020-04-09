using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : StateMachineBehaviour
{
    Vector3 vtemp = Vector3.zero;
    Transform mothership;
    bool wanderPoint = false;
    float TempTime = 0f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mothership = MotherShipClass.motherShip;
        TempTime = 0;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector3.SqrMagnitude(mothership.position-vtemp)<100 || vtemp == Vector3.zero)
            vtemp = new Vector3(Random.Range(MotherShipClass.xMin, MotherShipClass.xMax), Random.Range(MotherShipClass.yMin, MotherShipClass.yMax), Random.Range(MotherShipClass.zMin, MotherShipClass.zMax));

        FollowToAPoint(vtemp);
        TempTime += Time.deltaTime;
        if (TempTime>60f)
        {
            EnemyManager.Instance.NumberOfMinionsToSpawn(5, 3);
            TempTime = 0;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    void FollowToAPoint(Vector3 Position)
    {

        mothership.rotation = Quaternion.LookRotation(Vector3.RotateTowards(mothership.forward, Position - mothership.position, MotherShipClass.rotateSpeed * Time.deltaTime, 0.0f));
            mothership.position = Vector3.MoveTowards(mothership.position, Position, MotherShipClass.MovementSpeed * Time.deltaTime);

        //else
        //{
        //    transform.rotation = new Quaternion(0, 0, 0, 0);
        //}
    }


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
