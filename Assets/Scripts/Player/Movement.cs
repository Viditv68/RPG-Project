using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{
    private NavMeshAgent nav;
    private Animator animator;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Move();
        }

        UpdateAnimator();
        
        
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = nav.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;

        animator.SetFloat("forwardSpeed", speed);
    }

    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            nav.destination = hit.point;
            //nav.SetDestination(hit.point);
        }

    }
}
