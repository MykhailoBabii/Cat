using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public interface IShipScreenView: IView<IShipScreenPresenter>
    {
        void ShowHelpElement(int index);
        void HideHelpElement();
    }

    public class ShipScreenView : BaseScreenView, IShipScreenView
    {
        public override ScreenType Type => ScreenType.Ship;

        public override ViewType View => ViewType.Screen;

        public IShipScreenPresenter Presenter { get; private set; }

        [SerializeField] private GameObject _helpElementParent;
        [SerializeField] private GameObject[] _helpElements;

        [SerializeField] private SelectionButton _rubkaButton;
        [SerializeField] private SelectionButton _warhouseButton;
        [SerializeField] private SelectionButton _workshopButon;
        [SerializeField] private SelectionButton _libraryButon;
        [SerializeField] private SelectionButton _museumButon;
        [SerializeField] private SelectionButton _kitchenButon;

        public void HideHelpElement()
        {
            _helpElementParent.SetActive(false);
        }

        public void ShowHelpElement(int index)
        {
            _helpElementParent.SetActive(true);
            _helpElements[index].SetActive(true);
        }

        public void InitPresenter(IShipScreenPresenter presenter)
        {
            Presenter = presenter;
        }

        private void HideAllHelpElements()
        {
            foreach (var item in _helpElements)
            {
                item.SetActive(false);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            HideAllHelpElements();
            //HideHelpElement();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            _rubkaButton.SetClickCallback(OnRubkaClickHandler);
            _warhouseButton.SetClickCallback(OnWarhouseClickHandler);
            _workshopButon.SetClickCallback(OnWorkshopClickHandler);
            _libraryButon.SetClickCallback(OnLibraryClickHandler);
            _museumButon.SetClickCallback(OnMuseumCkickHandler);
            _kitchenButon.SetClickCallback(OnKitchenClickHandler);
        }

        private void OnRubkaClickHandler()
        {
            Presenter.RubkaClick();
        }

        private void OnWarhouseClickHandler()
        {
            Presenter.WarhouseClick();
        }

        private void OnWorkshopClickHandler()
        {
            Presenter.WorkshopClick();
        }

        private void OnLibraryClickHandler()
        {
            Presenter.LibraryClick();
        }

        private void OnMuseumCkickHandler()
        {
            Presenter.MuseumClick();
        }

        private void OnKitchenClickHandler()
        {
            Presenter.KitchenClick();
        }
    }
}