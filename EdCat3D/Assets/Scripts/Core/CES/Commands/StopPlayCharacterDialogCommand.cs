using Core.DI;
using Core.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{
    public class StopPlayCharacterDialogCommand : BaseCommand
    {
        private ICharacterDialogController _characterDialogController;
        public override void Prepare(DiContainer container)
        {
            _characterDialogController = container.GetService<ICharacterDialogController>();
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK");

            _characterDialogController.StopCharacterDialog();
        }
    }
}