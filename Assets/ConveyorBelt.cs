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

    private HashSet<Rigidbody> _rigidBodies = new();

    private void Update()
    {
        Vector3 GlobalForce = transform.TransformDirection(Force);
        Vector3 TargetDirection = GlobalForce.normalized;
        Vector3 TargetVelocity = TargetDirection * Speed;


        foreach (Rigidbody rigidBody in _rigidBodies)
        {
            if (rigidBody == null)
                continue;

            int directionSign = Vector3.Dot(rigidBody.velocity, TargetDirection) > 0f ? 1 : -1;
            float alignedSpeed = Vector3.Project(rigidBody.velocity, TargetDirection).magnitude;
            alignedSpeed *= directionSign;

            float forceModifier = Math.Clamp((Speed - alignedSpeed) / Speed, 0, 1);

            rigidBody.AddForce(GlobalForce * forceModifier, ForceMode.Acceleration);
        }

        _rigidBodies.RemoveWhere(r => r == null);
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (!rb)
            return;

        _rigidBodies.Add(rb);
    }

    private void OnTriggerExit(Collider other)
    {

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (!rb)
            return;

        _rigidBodies.Remove(rb);
    }
}
