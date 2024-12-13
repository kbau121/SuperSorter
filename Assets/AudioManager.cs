using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip successClip;
    [SerializeField] private AudioClip wrongItemClip;

    public void PlaySuccessClip(Vector3 worldPos)
    {
        AudioSource.PlayClipAtPoint(successClip, worldPos);
    }

    public void PlayWrongItemClip(Vector3 worldPos)
    {
        AudioSource.PlayClipAtPoint(wrongItemClip, worldPos);
    }
}
