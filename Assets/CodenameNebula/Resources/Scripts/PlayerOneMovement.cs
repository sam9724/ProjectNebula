using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class PlayerOneMovement : Player1PositionBehavior
{
    // Start is called before the first frame update
    public float speed;
    public DynamicJoystick DynamicJoystick;
    public DynamicJoystick AltitudeJoystick;
    public Rigidbody rb;
  

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }

        if (networkObject.IsServer)
        {
            Vector3 direction = Vector3.right * DynamicJoystick.Horizontal;
            transform.Rotate(0, direction.x, 0);

            Vector3 acceleration = transform.forward * DynamicJoystick.Vertical;
            rb.AddForce(acceleration * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            if (transform.rotation.x > -30f && transform.position.x < 30f)
            {
                Vector3 altitudeTilt = transform.forward * AltitudeJoystick.Vertical;
                Debug.Log(altitudeTilt);
                transform.Rotate(-altitudeTilt.z, 0, 0);
            }
        }
    }
}
