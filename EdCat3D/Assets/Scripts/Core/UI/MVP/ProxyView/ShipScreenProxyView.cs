using Core.UI.MVP;

namespace Core.UI
{
    public class ShipScreenProxyView : BaseProxyView<IShipScreenView>
    {
        public ShipScreenProxyView(IUIManager uIManager) : base(uIManager)
        {
        }

        public override void Prepare()
        {
            View = _uIManager.GetScreen<IShipScreenView>(ScreenType.Ship);
        }
    }
}