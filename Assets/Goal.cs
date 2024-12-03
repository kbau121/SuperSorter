using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Goal : MonoBehaviour
{
    [SerializeField]
    private List<Rule> Rules;

    private void OnTriggerEnter(Collider other)
    {
        Scoreable scoreable = other.gameObject.GetComponentInParent<Scoreable>();

        if (!scoreable) Debug.Log("NULL");

        if (!scoreable) return;

        bool success = true;
        foreach (Rule rule in Rules)
        {
            success = rule.Score(scoreable);

            if (!success) break;
        }

        scoreable.Score(success);
        Destroy(scoreable.gameObject);
    }
}
