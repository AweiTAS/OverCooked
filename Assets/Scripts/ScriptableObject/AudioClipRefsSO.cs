using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliverSuccess;
    public AudioClip[] deliverFailed;
    public AudioClip[] footstep;
    public AudioClip[] itemDroped;
    public AudioClip[] itemPicked;
    public AudioClip stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
