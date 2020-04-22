using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public class AimingTowardsEnemy : MonoBehaviour
{
    public static AimingTowardsEnemy instance;
    public bool isInRange = false;
    public Quaternion newRotation;
    void Awake()
	{
        instance = this;
	}
	void FixedUpdate()
	{
		if (isInRange)
		{
            AimDetection();
        }
		
    }


    public void AimDetection()
	{
		float minDist = Mathf.Infinity;

        
         
        //Collider[] cols = Physics.OverlapSphere(transform.position, 8);
        //foreach (Collider hitt in cols)
        //{

        //    if (hitt.tag == "Enemy")
        //    {

        //        float dist = Vector3.Distance(transform.position, hitt.transform.position);
                
        //        if (dist <= 7)
        //        {
                    Debug.Log(" in range");
              //      minDist = dist;
              //      nearest = hitt.transform;
                    isInRange = true;
        //  Vector3 playerToMouse = nearest.transform.position - transform.position;
         playerToMouse.y = 0f;
         playerToMouse.Normalize();
                    // nearest.y = 0f;
                    // nearest.Normalize();
                    // Quaternion newRotation = Quaternion.LookRotation(nearest);
                    GetComponent<Rigidbody>().MoveRotation(newRotation);
              //  }
               // else
            //    {
            //        isInRange=false;
            //        //Debug.LogError("not in range");
            //    }
            //}
            //else
            //{
            //    //  isInRange=false;
            //    return;
            //}
      //  }
	}
	
}
