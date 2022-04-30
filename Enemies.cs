using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    /* --TODO--
     *  + movement 
     *  + life
     *  + collition (bullet & player)
     */
    HealthManager Health;
    Vector3 speed, lookAt,rotDirection, currentPos, prevPos;
    float damagePower, walkSpeed, rotSpeed;
    private Transform targetToFollow;

    // Start is called before the first frame update
    void Start()
    {
        targetToFollow = GameObject.Find("player").transform;
        walkSpeed = 1.5f;
        rotSpeed = 1f;
        Health = new HealthManager(2.5f);
        damagePower = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        prevPos = this.transform.position;
        //Debug.Log("current_U: "+this.transform.position);

        lookAt = new Vector3(targetToFollow.position.x, this.transform.position.y, targetToFollow.position.z);
        rotDirection = lookAt - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(rotDirection),Time.deltaTime * rotSpeed);
        this.transform.Translate(new Vector3(0,0,walkSpeed * Time.deltaTime));
    }
     
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Weapon")
        {
            Health.TakeDamage(other.transform.GetComponent<WeaponBehaviour>().DamagePower);
            if(!Health.StillAlive())
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        //if(other.gameObject.tag == "Wall")
        //{
        //}
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag == "Wall")
    //    {
    //    }
    //}
    
    public float DamagePower
    {
        get{return damagePower;}
    }
}
