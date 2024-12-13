using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttractTarget : MonoBehaviour
{
    [SerializeField]
    private Goal m_goal;

    [Tooltip("ATTRACTION FORCE")]
    [SerializeField]
    private float m_attractionForce = 1;

    [Tooltip("FALL OFF")]
    [SerializeField]
    private bool m_useDistanceFalloff = true;

    [Tooltip("MAX ATTRACTION DISTANCE")]
    [SerializeField]
    private float m_maxDistance = 1f;

    [Tooltip("LIST OF ATTACT TARGETS")]
    [SerializeField]
    private List<Rigidbody> m_targets = new List<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = transform.parent;
        if(parentTransform != null )
        {
            m_goal = parentTransform.GetComponent<Goal>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ApplyAttraction(Rigidbody rb)
    {
        Vector3 center = transform.position;
        Vector3 direction = (center - rb.position).normalized;
        float distance = Vector3.Distance(center, rb.position);

        // compute force
        float force = m_attractionForce;
        if (m_useDistanceFalloff && distance > 0f)
        {
            force *= Mathf.Clamp01(1f - (distance / m_maxDistance));
        }

        Debug.Log($"Applying force {force} to {rb.name}");
        rb.AddForce(direction * force, ForceMode.Force);
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody rb in m_targets)
        {
            if (rb != null)
                ApplyAttraction(rb);
        }
        m_targets.Clear();
    }

    private void OnTriggerStay(Collider other)
    {
        Scoreable sc = other.gameObject.GetComponent<Scoreable>();
        if (sc != null)
        {
            if (!m_targets.Contains(sc.GetComponent<Rigidbody>()))
            {
                m_targets.Add(sc.GetComponent<Rigidbody>());
            }
        }
    }
}
