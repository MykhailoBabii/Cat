using Core.DI;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{

    
    public class SetDialogPhraseCommand : BaseCommand
    {
        [SerializeField] private string _phraseKey;

        private IDialogPanelPresenter _dialogPanelPresenter;

        public override void Prepare(DiContainer container)
        {
            _dialogPanelPresenter = container.GetService<IDialogPanelPresenter>();
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, phraseKey: {_phraseKey}");

            _dialogPanelPresenter.SetPhraseText(_phraseKey);
            CompleteCommand();
        }
    }
}