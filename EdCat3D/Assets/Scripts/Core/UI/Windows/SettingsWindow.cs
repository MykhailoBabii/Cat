using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{

    public class SettingsWindow : BaseWindowView
    {
        public override WindowType Type {
            get
            {
                return WindowType.Settings;
            }
        }
        public override ViewType View
        {
            get
            {
                return ViewType.Window;
            }
        }

        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;

        [SerializeField] private Button _closeButton;

        private System.Action _closeButtonCallback;
        private System.Action<bool> _musicToggleCallback;

        public void InitCallback(System.Action closeButtonCallback, System.Action<bool> musicToggleCallback)
        {
            _closeButtonCallback = closeButtonCallback;
            _musicToggleCallback = musicToggleCallback;
        }

        public void SetupInitialValue(bool isMusicOn, bool isSoundOn)
        {
            _musicToggle.isOn = isMusicOn;
            _soundToggle.isOn = isSoundOn;
        }

        protected override void OnAwake()
        {
            UIManager.RegisterView(this);
            base.OnAwake();
            _closeButton.onClick.AddListener(OnCloseButtonHandler);

            _musicToggle.onValueChanged.AddListener(OnMusicToggleHandler);
        }

        private void OnCloseButtonHandler()
        {
            if (_closeButtonCallback != null)
            {
                _closeButtonCallback.Invoke();
            }
        }


        private void OnMusicToggleHandler(bool isOn)
        {
            if (_musicToggleCallback != null)
            {
                _musicToggleCallback.Invoke(isOn);
            }
        }
    }
}