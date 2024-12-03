using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip successClip;

    public void PlaySuccessClip(Vector3 worldPos)
    {
        AudioSource.PlayClipAtPoint(successClip, worldPos);
    }
}
