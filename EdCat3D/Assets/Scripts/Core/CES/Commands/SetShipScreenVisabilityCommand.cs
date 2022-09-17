using Core.DI;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{
    public class SetShipScreenVisabilityCommand : BaseCommand
    {
        [SerializeField] private bool _visible;

        private IShipScreenPresenter _shipScreenPresenter;

        public override void Prepare(DiContainer container)
        {
            _shipScreenPresenter = container.GetService<IShipScreenPresenter>();
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, _visible: {_visible}");

            if (_visible == true)
            {
                _shipScreenPresenter.Init();
                _shipScreenPresenter.Show();
            }
            else
            {
                _shipScreenPresenter.Hide();
            }

            CompleteCommand();
        }
    }
}