using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public interface ILoadingScreenView:IView<ILoadingScreenPresenter>
    {
        void SetActiveRandomBackground();
        void SetSliderProgress(float value);
    }

    public class LoadingScreenView : BaseScreenView, ILoadingScreenView
    {
        public override ScreenType Type => ScreenType.Loading;

        public override ViewType View => ViewType.Screen;

        public ILoadingScreenPresenter Presenter { get; private set; }

        [SerializeField] private GameObject[] _backgrounds;
        [SerializeField] private Slider _progress;

        public void SetSliderProgress(float value)
        {
            _progress.value = value;
        }

        public void SetActiveRandomBackground()
        {
            HideAllBackgrounds();
            var index = UnityEngine.Random.Range(0, _backgrounds.Length);
            _backgrounds[index].SetActive(true);
        }
        public void InitPresenter(ILoadingScreenPresenter presenter)
        {
            Presenter = presenter;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            HideAllBackgrounds();
        }

        private void HideAllBackgrounds()
        {
            foreach (var item in _backgrounds)
            {
                item.SetActive(false);
            }
        }
    }
}