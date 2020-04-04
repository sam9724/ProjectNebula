using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotthershipSwirling : StateMachineBehaviour
{
    Vector3 vtemp;
    Transform mothership;
    Transform player;
    bool wanderPoint = false;
    float timeTemp = 0f;
    Rigidbody motherShipRigidBody;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mothership = MotherShipClass.motherShip;
        player = MotherShipClass.player;
        vtemp = Vector3.zero;
        motherShipRigidBody = MotherShipClass.motherShip.gameObject.GetComponent<Rigidbody>();
        
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        timeTemp += Time.deltaTime;
        SwirlWhileFollow();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
      void SwirlWhileFollow()
      {        
            if (timeTemp>3f)
            {
                vtemp =Random.onUnitSphere*4;
                motherShipRigidBody.AddForce(vtemp,ForceMode.Force);
                Debug.Log(vtemp);
                timeTemp = 0;
            }
           // mothership.position = Vector3.MoveTowards(mothership.position,vtemp, MotherShipClass.MovementSpeed * Time.deltaTime);
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
