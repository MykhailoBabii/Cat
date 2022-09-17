using Core.DI;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{

    
    public class PrepareDialogCharacterNameCommand : BaseCommand
    {
        [SerializeField] private string _characterNameKey;

        private IDialogPanelPresenter _dialogPanelPresenter;

        public override void Prepare(DiContainer container)
        {
            _dialogPanelPresenter = container.GetService<IDialogPanelPresenter>();
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, characterNameKey: {_characterNameKey}");

            _dialogPanelPresenter.SetCharacterName(_characterNameKey);
            CompleteCommand();
        }
    }
}