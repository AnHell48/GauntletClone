using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviors : MonoBehaviour
{
    //public GameObject target;
    float maxSpeed, targetDistance;
    Vector3 velocity, desiredVelocity, steer;

    private void Start()
    {
       velocity = new Vector3(2, 0, 0);
    }

    public void Follow(GameObject target)
    {
        desiredVelocity = ((target.transform.position - this.transform.position).normalized) * maxSpeed;
        steer = desiredVelocity - velocity;
        steer = Vector3.ClampMagnitude(steer, 5);
        steer /= 10;
        velocity = Vector3.ClampMagnitude(velocity + steer, maxSpeed);

        // lock y position (up & down)
        velocity.y = 0;

        this.transform.position += velocity;
        this.transform.forward = velocity.normalized;
        this.transform.LookAt(target.transform);
        
    }
}
