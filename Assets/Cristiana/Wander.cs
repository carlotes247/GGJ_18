﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
//[RequireComponent(typeof(CharacterController))]
public class Wander : MonoBehaviour
{
    public float speed = 5;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 30;

    //CharacterController controller;
    Rigidbody body;
    float heading;
    Vector3 targetRotation;

    Vector3 forward
    {
        get { return transform.TransformDirection(Vector3.forward); }
    }

    void Awake()
    {
        //controller = GetComponent<CharacterController>();
        body = GetComponent<Rigidbody>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeadingRoutine());
    }

    void Update()
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        body.velocity = forward * speed;
        //controller.Move(forward * speed);
        drawLaser(transform.position, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag != "Boundary")
        {
            return;
        }

        RaycastHit hit;
        Vector3 rayDir = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, rayDir, out hit, 1000))
        {
            rayDir = Vector3.Reflect((hit.point - transform.position).normalized, hit.normal);
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, rayDir);
        }

        // Bounce off the wall using angle of reflection
        /*if (Physics.Raycast(transform.position, Vector3.forward, out hit))
        {
            var newDirection = Vector3.Reflect(forward, hit.normal);
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, newDirection);
        }*/

        heading = transform.eulerAngles.y;
        NewHeading();
    }

    //For Debug purposes
    void drawLaser(Vector3 startPoint, int n)
    {
        RaycastHit hit;
        Vector3 rayDir = transform.TransformDirection(Vector3.forward);

        for (var i = 0; i < n; i++)
        {
            if (Physics.Raycast(startPoint, rayDir, out hit, 1000))
            {
                Debug.DrawLine(startPoint, hit.point);
                rayDir = Vector3.Reflect((hit.point - startPoint).normalized, hit.normal);
                startPoint = hit.point;
            }
        }
    }
    

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeading()
    {
        var floor = transform.eulerAngles.y - maxHeadingChange;
        var ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeadingRoutine()
    {
        while (true)
        {
            NewHeading();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
}