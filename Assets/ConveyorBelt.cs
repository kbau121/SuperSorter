using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ConveyorBelt : MonoBehaviour
{
    public Vector3 Force = Vector3.zero;

    [Min(0.1f)]
    public float Speed = 5f;

    private List<Rigidbody> Rigidbodies = new List<Rigidbody>();

    private void Update()
    {
        Vector3 GlobalForce = transform.TransformDirection(Force);
        Vector3 TargetDirection = GlobalForce.normalized;
        Vector3 TargetVelocity = TargetDirection * Speed;

        foreach (Rigidbody rb in Rigidbodies)
        {
            int directionSign = Vector3.Dot(rb.velocity, TargetDirection) > 0f ? 1 : -1;
            float alignedSpeed = Vector3.Project(rb.velocity, TargetDirection).magnitude;
            alignedSpeed *= directionSign;

            float forceModifier = Math.Clamp((Speed - alignedSpeed) / Speed, 0, 1);

            rb.AddForce(GlobalForce * forceModifier, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (!rb) return;

        Rigidbodies.Add(rb);
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (!rb) return;

        Rigidbodies.Remove(rb);
    }
}
