using Core.Events;
using UnityEngine;

namespace Core.UI
{
    public interface IDialogPanelPresenter:IPresenter<IProxyView<IDialogPanelView>, IDialogPanelView>
    {
        void SetBackground(string spriteName);
        void ShowBackground();
        void HideBackground();
        void SetCharacterName(string characterNameKey);
        void SetPhraseText(string phraseKey);
        void SetCharacterTexture(string characterTextureNameKey);
        void OnNextButtonClick();
    }

    public class DialogPanelPresenter : IDialogPanelPresenter
    {
        public IProxyView<IDialogPanelView> ProxyView { get; private set; }
        private readonly ResourceInner.ResourceProvider _resourceProvider;
        private readonly Localization.ILocalizationService _localizationService;

        public DialogPanelPresenter(IProxyView<IDialogPanelView> proxyView
            , ResourceInner.ResourceProvider resourceProvider
            , Localization.ILocalizationService localizationService)
        {
            ProxyView = proxyView;
            _resourceProvider = resourceProvider;
            _localizationService = localizationService;
        }

        public void Hide()
        {
            if (ProxyView.IsPrepared)
            {
                ProxyView.View.Hide();
            }
        }

        public void HideBackground()
        {
            if (ProxyView.IsPrepared)
            {
                ProxyView.View.HideBackground();
            }
        }

        public void Init()
        {
            ProxyView.Prepare();
            ProxyView.View.InitPresenter(this);
        }

        public void OnNextButtonClick()
        {
            EventAggregator.Post(this, new InteractionEvent() { Type = InteractionType.NextButtonClick });
        }

        public void SetBackground(string spriteName)
        {
            if (ProxyView.IsPrepared == false)
            {
                return;
            }
                var sprite = _resourceProvider.GetSprite(spriteName);
            ProxyView.View.SetBackground(sprite);
        }

        public void SetCharacterName(string characterNameKey)
        {
            if (ProxyView.IsPrepared == false)
            {
                return;
            }

            var name = _localizationService.GetPhrase(characterNameKey);
            ProxyView.View.SetCharacterName(name);
        }

        public void SetPhraseText(string phraseKey)
        {
            if (ProxyView.IsPrepared == false)
            {
                return;
            }

            var phrase = _localizationService.GetPhrase(phraseKey);
            ProxyView.View.SetPhraseText(phrase);
        }

        public void Show()
        {
            if (ProxyView.IsPrepared == false)
            {
                return;
            }

            ProxyView.View.Show();
        }

        public void ShowBackground()
        {
            if (ProxyView.IsPrepared == false)
            {
                return;
            }

            ProxyView.View.ShowBackground();
        }

        public void SetCharacterTexture(string characterTextureNameKey)
        {
            if (ProxyView.IsPrepared == false)
            {
                return;
            }

            var renderTexure = _resourceProvider.GetRenderTexture(characterTextureNameKey);

            ProxyView.View.SetCharacterTexture(renderTexure);
        }
    }
}