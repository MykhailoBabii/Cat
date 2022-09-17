using Core.DI;
using Core.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{
    
    public class PlayCharacterAnimationCommand : BaseCommand
    {
        [SerializeField] private CharacterType _characterType;
        [SerializeField] private string _animationName;

        private ICharacterDialogController _characterDialogController;

        public override void Prepare(DiContainer container)
        {
            _characterDialogController = container.GetService<ICharacterDialogController>();
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, character: {_characterType}, animationName: {_animationName}");
            _characterDialogController.PlayCharacterAnimation(_characterType, _animationName);
            CompleteCommand();
        }
    }
}