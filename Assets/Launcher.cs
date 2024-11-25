using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Launcher : MonoBehaviour
{
    public int LauncherID = -1;

    private AudioSource AudioSource;

    private bool DoLob = false;
    private Vector3 Direction;

    [SerializeField]
    private float Strength;
    [SerializeField]
    private float StrengthRange;
    [SerializeField]
    private Vector3 Target;
    [SerializeField]
    private float TargetRadius;

    [Header("Gizmos")]

    [SerializeField]
    [Min(0f)]
    private float GizmoDensity = 10f;

    [SerializeField]
    [Min(0f)]
    private float GizmoTimespan = 3f;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private struct TargetParams
    {
        public bool DoLob;
        public float Strength;
        public float StrengthRange;
        public Vector3 Direction;
        public Vector3 Target;
        public float TargetRadius;

        public TargetParams(bool doLob, float strength, float strengthRange, Vector3 direction, Vector3 target, float targetRadius)
        {
            DoLob = doLob;
            Strength = strength;
            StrengthRange = strengthRange;
            Direction = direction;
            Target = target;
            TargetRadius = targetRadius;
        }
    }

    private TargetParams? GetTargetParams(Vector3 target, float strength, float targetRadius = 0f, float strengthRange = 0f, bool doLob = false)
    {
        target += Random.onUnitSphere * (targetRadius * Random.value);
        strength += (1f - 2f * Random.value) * strengthRange;

        float g = Physics.gravity.magnitude;
        Quaternion ToWorkingSpace = Quaternion.FromToRotation(Physics.gravity / g, Vector3.down);
        Quaternion FromWorkingSpace = Quaternion.FromToRotation(Vector3.down, Physics.gravity / g);

        Vector3 startPos = ToWorkingSpace * transform.position;
        Vector3 endPos = ToWorkingSpace * target;

        Vector3 deltaPos = endPos - startPos;
        Vector3 deltaPos_xz = deltaPos; deltaPos_xz.y = 0f;

        float h = startPos.y - endPos.y;
        float x = deltaPos_xz.magnitude;
        float v = strength;

        float phi = Mathf.Atan(x / h);

        float angle;
        if (doLob)
        {
            angle = (Mathf.Acos((g * x * x / (v * v) - h) / Mathf.Sqrt(h * h + x * x)) + phi) / 2f;

            if (h < 0f)
            {
                angle = Mathf.PI / 2 - angle;
                deltaPos *= -1;
            }
        }
        else
        {
            angle = (Mathf.Acos((g * x * x / (v * v) - h) / Mathf.Sqrt(h * h + x * x)) - phi) / 2f;

            if (h < 0f)
            {
                angle = Mathf.PI / 2 - angle;
            }
            else
            {
                angle *= -1f;
            }
        }

        Vector3 direction = new Vector3(0, Mathf.Sin(angle), 0);
        if (x > 0)
        {
            direction.x = Mathf.Cos(angle) * deltaPos.x / x;
            direction.z = Mathf.Cos(angle) * deltaPos.z / x;
        }

        direction = direction.normalized;
        if (direction.Equals(Vector3.zero)) return null;

        return new TargetParams(
            doLob,
            strength,
            strengthRange,
            FromWorkingSpace * direction,
            target,
            targetRadius
            );
    }

    private void SetTargetParams(TargetParams targetParams)
    {
        DoLob = targetParams.DoLob;
        Strength = targetParams.Strength;
        StrengthRange = targetParams.StrengthRange;
        Direction = targetParams.Direction;
        Target = targetParams.Target;
        TargetRadius = targetParams.TargetRadius;
    }

    public void Alert()
    {
        if (!AudioSource) return;
        AudioSource.Play();
    }

    public void LaunchObject(GameObject gameObject, Vector3 target, float strength, float targetRadius = 0f, float strengthRange = 0f, bool doLob = false)
    {
        TargetParams? targetParams = GetTargetParams(target, strength, targetRadius, strengthRange, doLob);
        if (!targetParams.HasValue) return;

        TargetParams validParams = targetParams.Value;

        if (!gameObject.GetComponent<Rigidbody>()) return;

        GameObject launchObject = Instantiate(gameObject, transform);
        Rigidbody rb = launchObject.GetComponent<Rigidbody>();

        rb.velocity = validParams.Direction * validParams.Strength;
        rb.angularVelocity = Random.onUnitSphere;
    }

    private void ValidGizmos(TargetParams targetParams)
    {
        List<Vector3> linePoints = new List<Vector3>() { transform.position };

        Vector3 initialVelocity = targetParams.Direction * targetParams.Strength;
        for (int i = 1; i < GizmoDensity * GizmoTimespan; ++i)
        {
            float t = i * GizmoTimespan / GizmoDensity;
            linePoints.Add(0.5f * t * t * Physics.gravity + initialVelocity * t + transform.position);
        }

        Gizmos.color = Color.black;
        Gizmos.DrawLineStrip(new System.ReadOnlySpan<Vector3>(linePoints.ToArray()), false);
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(targetParams.Target, Mathf.Max(0.025f, targetParams.TargetRadius));
    }

    private void InvalidGizmos(Vector3 target, float targetRadius)
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(target, Mathf.Max(0.025f, targetRadius));
    }

    private void OnDrawGizmos()
    {
        TargetParams? targetParams = GetTargetParams(Target, Strength, doLob: DoLob);
        if (targetParams.HasValue)
        {
            TargetParams validParams = targetParams.Value;
            validParams.TargetRadius = TargetRadius;
            validParams.StrengthRange = StrengthRange;
            ValidGizmos(validParams);
        }
        else
        {
            InvalidGizmos(Target, TargetRadius);
        }
    }
}
