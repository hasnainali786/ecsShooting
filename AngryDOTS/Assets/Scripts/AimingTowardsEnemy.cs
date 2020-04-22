using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

public class AimingTowardsEnemy : MonoBehaviour
{
    bool isInRange = false;
	EntityManager manager;

	void FixedUpdate()
	{
		AimDetection();
    }
		public void AimDetection()
	{
		//BuildPhysicsWorld bpw = World.Active.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
		//World.Active.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
	}
	
}
