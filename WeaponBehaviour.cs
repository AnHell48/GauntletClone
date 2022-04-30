using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private Vector3 startPosition;
    private float distanceLimit, damage, moveSpeed;

    public float DamagePower
    {
        get{return damage;}
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 15f;
        damage = 4f;
        distanceLimit = 10.0f;
        startPosition = GetComponentInParent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //check if the starting position is greater then the limit position and delete if it is.
        if (Vector3.Distance(startPosition, this.transform.position) >= distanceLimit)
            DeleteObj();
    }

    private void OnCollisionEnter(Collision other)
     {
         if(other.gameObject.tag != "Player")
            DeleteObj();
    }

    private void DeleteObj()
    {
        Debug.Log("weapon have been DELETED!");

        Destroy(this.gameObject);
    }
}
