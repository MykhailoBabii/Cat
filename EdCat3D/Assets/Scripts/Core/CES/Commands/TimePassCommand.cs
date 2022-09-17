using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Core.CES
{
    
    public class TimePassCommand : BaseCommand
    {
        [SerializeField] private float _delay;

        private float _timeCounter;
        private Tweener _tweener;

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, delay: {_delay}");

            _timeCounter = 0f;
            _tweener = DOTween.To(() => _timeCounter, x => _timeCounter = x, _delay, _delay)
                .SetEase(Ease.Linear)
                .OnComplete(OnTimePassComplete);
        }

        private void OnTimePassComplete()
        {
            _tweener.Kill();
            CompleteCommand();
        }
    }
}