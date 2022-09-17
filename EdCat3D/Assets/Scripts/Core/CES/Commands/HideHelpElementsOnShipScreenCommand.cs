using Core.DI;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{


    public class HideHelpElementsOnShipScreenCommand : BaseCommand
    {
        private IShipScreenPresenter _shipScreenPresenter;

        public override void Prepare(DiContainer container)
        {
            _shipScreenPresenter = container.GetService<IShipScreenPresenter>();
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK");

            _shipScreenPresenter.HideAllHelpElement();
            CompleteCommand();
        }
    }
}