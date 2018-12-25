using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ValApplyOn
{
    awake, start
}

public abstract class ApplyFloatValue : MonoBehaviour
{
    
    [SerializeField] protected FloatVariable _floatVal;
    [SerializeField] protected ValApplyOn _applyOn;
    [SerializeField] protected bool _applyConstantly = true;

    protected virtual void Awake()
    {
        if (_applyOn == ValApplyOn.awake)
            ApplyValue();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (_applyOn == ValApplyOn.start)
            ApplyValue();   
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_applyConstantly)
            ApplyValue();
    }

    public abstract void ApplyValue();
}
