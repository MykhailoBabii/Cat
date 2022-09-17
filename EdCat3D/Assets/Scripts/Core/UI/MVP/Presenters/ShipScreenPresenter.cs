using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public interface IShipScreenPresenter:IPresenter<IProxyView<IShipScreenView>, IShipScreenView>
    {
        void RubkaClick();
        void WarhouseClick();
        void WorkshopClick();
        void LibraryClick();
        void MuseumClick();
        void KitchenClick();

        void ShowHelpElement(int index);
        void HideAllHelpElement();
    }

    public class ShipScreenPresenter : IShipScreenPresenter
    {
        public IProxyView<IShipScreenView> ProxyView { get; private set; }

        private readonly IShipScreenUseCases _shipScreenUseCases;

        public ShipScreenPresenter(IProxyView<IShipScreenView> proxyView
            , IShipScreenUseCases shipScreenUseCases)
        {
            ProxyView = proxyView;
            _shipScreenUseCases = shipScreenUseCases;
        }

        public void Hide()
        {
            if (ProxyView.IsPrepared == false)
            {
                return;
            }

            ProxyView.View.Hide();
        }

        public void HideAllHelpElement()
        {
            if (ProxyView.IsPrepared == false)
            {
                return;
            }

            ProxyView.View.HideHelpElement();
        }

        public void Init()
        {
            ProxyView.Prepare();
            ProxyView.View.InitPresenter(this);
        }

        public void KitchenClick()
        {
            _shipScreenUseCases.KitchenSelect();
        }

        public void LibraryClick()
        {
            _shipScreenUseCases.LibrarySelect();
        }

        public void MuseumClick()
        {
            _shipScreenUseCases.MuseumSelect();
        }

        public void RubkaClick()
        {
            _shipScreenUseCases.RubkaSelect();
        }

        public void Show()
        {
            ProxyView.View.Show();
        }

        public void ShowHelpElement(int index)
        {
            ProxyView.View.ShowHelpElement(index);
        }

        public void WarhouseClick()
        {
            _shipScreenUseCases.WarhouseSelect();
        }

        public void WorkshopClick()
        {
            _shipScreenUseCases.WorkshopSelect();
        }
    }
}