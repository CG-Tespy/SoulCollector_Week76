using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Slider))]
public class ApplySliderValToFloat : ApplyFloatValue
{

    Slider slider;

    protected override void Awake()
    {
        slider = GetComponent<Slider>();
        base.Awake();
        
    }

    public override void ApplyValue()
    {
        _floatVal.value = slider.value;
    }


}
