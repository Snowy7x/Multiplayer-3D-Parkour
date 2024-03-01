using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Snowy.MultiplayerBasics
{
    public class SliderS : MonoBehaviour
    {
        public Slider slider;
        public float minValue;
        public float maxValue;
        public float currentValue;
        public float percentage;

        public TMP_Text valueText;

        public void SetValue()
        {
            // convert from slider value to min/max value
            currentValue = (slider.value / 100) * (maxValue - minValue) + minValue;
            percentage = slider.value;
            valueText.text = percentage + "%";
        }

        public void SetFixedValue(float value)
        {
            currentValue = value;
            percentage = (currentValue - minValue) / (maxValue - minValue);
            slider.value = percentage;
            valueText.text = percentage + "%";
        }

        public float GetValue()
        {
            return currentValue;
        }

    }
}

