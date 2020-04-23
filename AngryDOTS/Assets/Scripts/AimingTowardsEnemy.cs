using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;


public class AimingTowardsEnemy : MonoBehaviour
{
    [ReadOnly] public static AimingTowardsEnemy instance;
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
        transform.rotation = Quaternion.Lerp(transform.rotation,newRotation,0.15f); 
	}
	
}
