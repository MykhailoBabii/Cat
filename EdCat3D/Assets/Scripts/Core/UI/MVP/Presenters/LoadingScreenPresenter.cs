using Core.UI.Commands;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public interface ILoadingScreenPresenter : IPresenter<IProxyView<ILoadingScreenView>, ILoadingScreenView>
    {
        void InitCommandOnLoading(System.Action loadingCommand);
        void InitCommandOnLoadingComplete(System.Action loadingCompleteCommand);
    }


    public class LoadingScreenPresenter : ILoadingScreenPresenter
    {
        private const float MinSliderTime = 0.1f;
        private const float MaxSliderTime = 1f;

        private const int MinSliderStep = 2;
        private const int MaxSliderStep = 5;

        public IProxyView<ILoadingScreenView> ProxyView { get; private set; }

        private int _currentStep = 0;
        private int _totalStep = 0;
        private float _currentValue = 0f;
        private float _sliderValue = 0f;
        private bool _isInProcess;

        private System.Action _loadingAction;
        private System.Action _onLoadCompleteAction;

        public LoadingScreenPresenter(IProxyView<ILoadingScreenView> proxyView)
        {
            ProxyView = proxyView;
        }

        public void Hide()
        {
            if(ProxyView.IsPrepared == false)
            {
                return;
            }

            ProxyView.View.Hide();
        }

        public void Init()
        {
            ProxyView.Prepare();
            ProxyView.View.InitPresenter(this);
        }

        public void InitCommandOnLoading(System.Action loadingCommand)
        {
            _loadingAction = loadingCommand;
        }

        public void Show()
        {
            ProxyView.View.SetActiveRandomBackground();
            ShowInner(5);
        }

        public void InitCommandOnLoadingComplete(System.Action loadingCompleteCommand)
        {
            _onLoadCompleteAction = loadingCompleteCommand;
        }

        private void ShowInner(int step, bool useRandom = true)
        {
            ReseValue();

            _isInProcess = true;
            _totalStep = useRandom ? UnityEngine.Random.Range(MinSliderStep, MaxSliderStep) : step;
            ProxyView.View.Show();
            MoveToNextStep();
        }

        private void ReseValue()
        {
            _currentStep = 0;
            _totalStep = 0;
            _sliderValue = 0f;
            _currentValue = 0f;
        }

        private void MoveToNextStep()
        {
            UpdateStep();
            var time = UnityEngine.Random.Range(MinSliderTime, MaxSliderTime);
            var toValue = (float)_currentStep / (float)_totalStep;
            DOTween.To(() => _currentValue, x => _sliderValue = x, toValue, time)
                .SetEase(Ease.OutQuad)
                .OnUpdate(OnValueUpdate)
                .OnComplete(OnStepComplete);
        }

        private void OnValueUpdate()
        {
            ProxyView.View.SetSliderProgress( _sliderValue);
        }

        private void OnStepComplete()
        {
            _currentValue = _sliderValue;

            if (_currentStep >= _totalStep)
            {
                Hide();

                _isInProcess = false;
                _onLoadCompleteAction?.Invoke();
            }
            else if (_currentStep >= _totalStep * 0.2f)
            {
                _loadingAction?.Invoke();
                MoveToNextStep();
            }
            else
            {
                MoveToNextStep();
            }
        }

        private void UpdateStep()
        {
            _currentStep++;
            if (_currentStep > _totalStep)
            {
                _currentStep = _totalStep;
            }
        }

        
    }
}