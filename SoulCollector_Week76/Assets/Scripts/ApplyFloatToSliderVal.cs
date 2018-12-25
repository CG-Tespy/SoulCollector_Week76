using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ApplyFloatToSliderVal : ApplyFloatValue
{
    Slider slider;

    protected override void Awake()
    {
        slider = GetComponent<Slider>();
        base.Awake();
        
    }

    public override void ApplyValue()
    {
        slider.value = _floatVal.value;
    }
}
