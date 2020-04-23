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
        AimDetection();		
    }


    public void AimDetection()
	{
         GetComponent<Rigidbody>().MoveRotation(newRotation);   
	}
	
}
