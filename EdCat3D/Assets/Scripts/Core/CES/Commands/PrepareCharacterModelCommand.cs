using Core.DI;
using Core.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{

    
    public class PrepareCharacterModelCommand : BaseCommand
    {
        [SerializeField] private CharacterType _character;
        [SerializeField] private bool _isShow;

        private ICharacterDialogController _characterDialogController;

        public override void Prepare(DiContainer container)
        {
            _characterDialogController = container.GetService<ICharacterDialogController>();
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, character: {_character}, _isShow: {_isShow}");

            if (_isShow == true)
            {
                _characterDialogController.ShowCharacter(_character);
            }
            else
            {
                _characterDialogController.HideCharacter(_character);
            }

            CompleteCommand();
        }
    }
}