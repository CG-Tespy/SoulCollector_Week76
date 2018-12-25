using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ApplyFloatToAudioSource : ApplyFloatValue
{
    AudioSource audioSource;
    
    protected override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        base.Awake();
    }

    public override void ApplyValue()
    {
        audioSource.volume = _floatVal.value;
    }
}
