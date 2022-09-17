using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public interface IDialogPanelView:IView<IDialogPanelPresenter>
    {
        void SetBackground(Sprite sprite);
        void ShowBackground();
        void HideBackground();
        void SetCharacterName(string name);
        void SetPhraseText(string phrase);

        void SetCharacterTexture(Texture texture);
    }

    public class DialogPanelView : BasePanelView, IDialogPanelView
    {
        public override PanelType Type => PanelType.Dialog;

        public override ViewType View => ViewType.Panel;

        public IDialogPanelPresenter Presenter { get; private set; }

        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private TextMeshProUGUI _phraseText;
        [SerializeField] private RawImage _characterImage;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _fullScreenButton;

        public void InitPresenter(IDialogPanelPresenter presenter)
        {
            Presenter = presenter;
        }

        public void SetBackground(Sprite sprite)
        {
            _backgroundImage.sprite = sprite;
        }

        public void ShowBackground()
        {
            _backgroundImage.gameObject.SetActive(true);
        }

        public void HideBackground()
        {
            _backgroundImage.gameObject.SetActive(false);
        }

        public void SetCharacterName(string name)
        {
            _characterName.text = name;
        }

        public void SetPhraseText(string phrase)
        {
            _phraseText.text = phrase;
        }

        public void SetCharacterTexture(Texture texture)
        {
            _characterImage.texture = texture;
        }

        protected override void OnAwake()
        {

            base.OnAwake();
            _nextButton.onClick.AddListener(OnNextButtonClickHandler);
            _fullScreenButton.onClick.AddListener(OnNextButtonClickHandler);
        }

        private void OnNextButtonClickHandler()
        {
            Presenter.OnNextButtonClick();
        }

        
    }
}