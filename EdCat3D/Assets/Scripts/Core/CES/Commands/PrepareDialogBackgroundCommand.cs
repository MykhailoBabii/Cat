using Core.DI;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{


    public class PrepareDialogBackgroundCommand : BaseCommand
    {
        [SerializeField] private string _backgroundKey;

        private IDialogPanelPresenter _dialogPanelPresenter;

        public override void Prepare(DiContainer container)
        {
            _dialogPanelPresenter = container.GetService<IDialogPanelPresenter>();
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, backgroundKey: {_backgroundKey}");

            _dialogPanelPresenter.SetBackground(_backgroundKey);

            CompleteCommand();
        }
    }
}