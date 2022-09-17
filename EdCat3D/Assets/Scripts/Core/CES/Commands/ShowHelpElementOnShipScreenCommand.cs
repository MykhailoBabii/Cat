using Core.DI;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{
    public class ShowHelpElementOnShipScreenCommand : BaseCommand
    {
        [SerializeField] private int _index;

        private IShipScreenPresenter _shipScreenPresenter;

        public override void Prepare(DiContainer container)
        {
            _shipScreenPresenter = container.GetService<IShipScreenPresenter>();
        }
        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, _index: {_index}");

            _shipScreenPresenter.ShowHelpElement(_index);
            CompleteCommand();
        }
    }
}