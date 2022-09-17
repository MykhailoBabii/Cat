using Core.DI;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{


    public class SetDialogBackgroundVisabilityCommand : BaseCommand
    {
        [SerializeField] private bool _visible;

        private IDialogPanelPresenter _dialogPanelPresenter;

        public override void Prepare(DiContainer container)
        {
            _dialogPanelPresenter = container.GetService<IDialogPanelPresenter>();
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, visible: {_visible}");

            if (_visible == true)
            {
                _dialogPanelPresenter.ShowBackground();
            }
            else
            {
                _dialogPanelPresenter.HideBackground();
            }

            CompleteCommand();
        }
    }
}