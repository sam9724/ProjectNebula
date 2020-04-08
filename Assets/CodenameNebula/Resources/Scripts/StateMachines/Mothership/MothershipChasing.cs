using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipChasing : StateMachineBehaviour
{
    Vector3 vtemp = Vector3.zero;
    Transform mothership;

    float minionsTimer = 0;
    
    Transform player;
    bool wanderPoint = false;
            RaycastHit hit;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mothership = MotherShipClass.motherShip;
        player = MotherShipClass.player;
        minionsTimer = 0;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FollowToAPoint(player.position); //        animator.ApplyBuiltinRootMotion();
       
        if(EnemyManager.Instance.minionInScene()<1)
        {
            minionsTimer += Time.deltaTime;
            if (minionsTimer > 5f)
            {
                EnemyManager.Instance.NumberOfMinionsToSpawn(3, 2);
                minionsTimer = 0f;
            if (Physics.Raycast(mothership.position, mothership.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(mothership.position, mothership.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if(hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Call player Damage Function.");
                        //GameObject.Destroy(hit.collider.gameObject);

                    }
                    else
                    {
                        Debug.Log("Destroy all the asteroids in middle");
                        //GameObject.Destroy(hit.collider.gameObject);
                    }
                }
            }
            //if (Physics.CapsuleCast(mothership.position, mothership.position + Vector3.up * mothership.localScale.y, mothership.localScale.x, mothership.forward, out hit, 10))
            //{
            //    Debug.Log(hit.collider.gameObject.name);
                
            //}
            // Does the ray intersect any objects excluding the player layer
        }

       

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    
    void FollowToAPoint(Vector3 Position)
    {
        mothership.rotation = Quaternion.LookRotation(Vector3.RotateTowards(mothership.forward, (Position - mothership.position).normalized, MotherShipClass.rotateSpeed * Time.deltaTime, 0.0f));
        if (Vector3.SqrMagnitude(mothership.position - Position) > 49f)
            mothership.position = Vector3.MoveTowards(mothership.position, player.position, MotherShipClass.MovementSpeed * Time.deltaTime);      
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
