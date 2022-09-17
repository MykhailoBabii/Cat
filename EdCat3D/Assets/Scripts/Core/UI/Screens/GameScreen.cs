using Core.Property;
using System.Collections;
using System.Collections.Generic;
//using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class GameScreen : BaseScreenView
    {
        public override ScreenType Type
        {
            get
            {
                return ScreenType.Game;
            }
        }

        public override ViewType View
        {
            get
            {
                return ViewType.Screen;
            }
        }

        [SerializeField] private Slider _slider;
        [SerializeField] private Text _levelText;

        private string _levelTextFormat;

        private IProperty<float> _progressProperty;
        public void InitProgressProtpery(IProperty<float> property)
        {
            _progressProperty = property;
            _progressProperty.OnValueChanged += OnProgressProtpertyValueChanged;
        }

        public void InitLevelNumber(int level)
        {
            _levelText.text = string.Format(_levelTextFormat, level);
        }

        private void OnProgressProtpertyValueChanged(float value)
        {
            _slider.value = value;
        }

        private void OnDestroy()
        {
            _progressProperty.OnValueChanged -= OnProgressProtpertyValueChanged;
        }

        protected override void OnAwake()
        {
            UIManager.RegisterView<GameScreen>(this);
            _levelTextFormat = _levelText.text;
            base.OnAwake();
        }
    }
}