using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerWall : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Pilot>(out Pilot player))
        {
            //player.TakeDamage(200);
        }
    }
}
