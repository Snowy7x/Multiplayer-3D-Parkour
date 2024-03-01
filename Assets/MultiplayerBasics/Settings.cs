using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowy.MultiplayerBasics
{
    public class Settings : MonoBehaviour
    {
        [Header("Sensitivity")]
        [SerializeField] SliderS sensitivitySlider;
        
        void Start()
        {
            sensitivitySlider.SetFixedValue(PlayerPrefs.GetFloat("Sensitivity", 150f));
        }
        
        public void ApplySettings()
        {
            SetSensitivity();
        }
        
        private void SetSensitivity()
        {
            PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.GetValue());
        }
        
    }
}


