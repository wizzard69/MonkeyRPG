﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

    NavMeshAgent agent;
    Transform target;


	void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
	}

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

			FaceTarget();
        }
    }

    public void Move2Point(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget (Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * 0.8f;
        agent.updateRotation = false;

        target = newTarget.interactableTransform;
    }

    public void StopFollowingTarget ()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }
}
