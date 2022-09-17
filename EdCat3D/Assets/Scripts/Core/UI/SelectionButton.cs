using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Core.UI
{

    [RequireComponent(typeof(Button))]
    public class SelectionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _selectionImage;
        [Range(0, 1)]
        [SerializeField] private float _alphaThreshold = 0.2f;

        [SerializeField] private float _animationTime = 0.3f;

        private System.Action _buttonClickCallback;
        public void OnPointerEnter(PointerEventData eventData)
        {
            _selectionImage.DOFade(1f, _animationTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _selectionImage.DOFade(0f, _animationTime);
        }

        public void SetClickCallback(System.Action clickCallback)
        {
            _buttonClickCallback = clickCallback;
        }

        private void Awake()
        {
            _button.image.alphaHitTestMinimumThreshold = _alphaThreshold;
            _selectionImage.DOFade(0f, 0f);
            _button.onClick.AddListener(OnButonClick);
        }

        private void OnButonClick()
        {
            _buttonClickCallback?.Invoke();
        }
    }
}