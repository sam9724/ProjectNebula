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
    float cloackTimer=0f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mothership = MotherShipClass.motherShip;
        player = MotherShipClass.player;
        minionsTimer = 0;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FollowToAPoint(player.position); //        animator.ApplyBuiltinRootMotion();
       
       // Debug.Log("MAterial: "+EnemyManager.Instance.mothership.GetComponentInChildren<MeshRenderer>().material.name);// SetFloat("CloakSlider",1);
        if(EnemyManager.Instance.minionInScene()<1)
        {
            minionsTimer += Time.deltaTime;
            if (minionsTimer > 5f)
            {
                EnemyManager.Instance.NumberOfMinionsToSpawn(3, 2);
                minionsTimer = 0f;
                if (cloackTimer <= 1f)
                {
                    cloackTimer += Time.deltaTime;
                    EnemyManager.Instance.mothership.GetComponentInChildren<MeshRenderer>().material.SetFloat("_CloakSlider", cloackTimer);
                }
                if (cloackTimer >= 1)
                    cloackTimer = 1;
            }

            if (cloackTimer >= 0f)
            {
                cloackTimer -= Time.deltaTime;
                EnemyManager.Instance.mothership.GetComponentInChildren<MeshRenderer>().material.SetFloat("_CloakSlider", cloackTimer);
            }
            if (cloackTimer <= 0)
                cloackTimer = 0;

                EnemyManager.Instance.mothership.shoot();
                if (Physics.Raycast(mothership.position, mothership.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(mothership.position, mothership.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    raycastHit(hit);
                    

                }
                if(Physics.Raycast(new Vector3(mothership.position.x,mothership.position.y-(mothership.localScale.y/2),mothership.position.z), mothership.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(new Vector3(mothership.position.x, mothership.position.y - (mothership.localScale.y / 2), mothership.position.z), mothership.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    raycastHit(hit);
                }
                if (Physics.Raycast(new Vector3(mothership.position.x, mothership.position.y + (mothership.localScale.y / 2), mothership.position.z), mothership.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(new Vector3(mothership.position.x, mothership.position.y + (mothership.localScale.y / 2), mothership.position.z), mothership.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    raycastHit(hit);
                }//
                if (Physics.Raycast(new Vector3(mothership.position.x - (mothership.localScale.x / 2), mothership.position.y, mothership.position.z), mothership.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(new Vector3(mothership.position.x - (mothership.localScale.x / 2), mothership.position.y , mothership.position.z), mothership.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    raycastHit(hit);
                }
                if (Physics.Raycast(new Vector3(mothership.position.x + (mothership.localScale.x / 2), mothership.position.y, mothership.position.z), mothership.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(new Vector3(mothership.position.x + (mothership.localScale.x / 2), mothership.position.y , mothership.position.z), mothership.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    raycastHit(hit);
                }
                //
                //
            
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
        if (Vector3.SqrMagnitude(mothership.position - Position) > MotherShipClass.stoppingDistance)
            mothership.position = Vector3.MoveTowards(mothership.position, player.position, MotherShipClass.MovementSpeed * Time.deltaTime);      
    }

    void raycastHit(RaycastHit hit)
    {
       // Debug.Log(hit.collider.gameObject.name);
        if(hit.collider.gameObject.GetComponent<IDamagable>()!=null)
        {
            hit.collider.gameObject.GetComponent<IDamagable>().TakeDamage(2000f);

        }
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
