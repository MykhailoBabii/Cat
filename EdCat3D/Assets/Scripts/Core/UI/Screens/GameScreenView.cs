using Core.Property;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class GameScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentHPText;
        [SerializeField] private TextMeshProUGUI _maxHPText;
        [SerializeField] private Slider  _healthPointSlider;
        private IIntProperty _healthProperty;
        private string _currentHPTextFormat;

        public IIntReadOnlyProperty HealthProperty => _healthProperty;

        public void BindHealthPointProperty(IIntReadOnlyProperty property)
        {
            _healthProperty = (IIntProperty) property;
            _healthProperty.OnValueChanged += OnHealthValueChangeHandler;
        }

        public void SetHPMaxValue(int healthvalue) 
        {
            _healthPointSlider.maxValue = healthvalue;
            
        }   
        public void OnHealthValueChangeHandler( int healthvalue)
        { 
            _currentHPText.text = string.Format(_currentHPTextFormat, healthvalue);
            _healthPointSlider.value = healthvalue;

        }
       

        
    }
}

