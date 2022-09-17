using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public interface IShipScreenUseCases
    {
        void RubkaSelect();
        void WarhouseSelect();
        void WorkshopSelect();
        void LibrarySelect();
        void MuseumSelect();
        void KitchenSelect();
    }

    public class ShipScreenUseCases : IShipScreenUseCases
    {
        //
        public ShipScreenUseCases()
        {
            //here will be init all presenters that need to implement this use case
        }
        public void KitchenSelect()
        {
            Debug.Log($"[{GetType().Name}][KitchenSelect] OK");
        }

        public void LibrarySelect()
        {
            Debug.Log($"[{GetType().Name}][LibrarySelet] OK");
        }

        public void MuseumSelect()
        {
            Debug.Log($"[{GetType().Name}][MuseumSelect] OK");
        }

        public void RubkaSelect()
        {
            Debug.Log($"[{GetType().Name}][RubkaSelect] OK");
        }

        public void WarhouseSelect()
        {
            Debug.Log($"[{GetType().Name}][WarhouseSelect] OK");
        }

        public void WorkshopSelect()
        {
            Debug.Log($"[{GetType().Name}][WorkshopSelect] OK");
        }
    }
}