using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayableArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Scoreable scoreable = other.gameObject.GetComponent<Scoreable>();
        if (!scoreable) return;

        Destroy(scoreable.gameObject);
    }
}
