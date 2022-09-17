using Core.DI;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{

    
    public class PrepareCharacterTextureCommand : BaseCommand
    {
        [SerializeField] private string _textureKey;

        private IDialogPanelPresenter _dialogPanelPresenter;
        public override void Prepare(DiContainer container)
        {
            _dialogPanelPresenter = container.GetService<IDialogPanelPresenter>();
        }
        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, textureKey: {_textureKey}");

            _dialogPanelPresenter.Init();
            _dialogPanelPresenter.SetCharacterTexture(_textureKey);
            CompleteCommand();
        }
    }
}