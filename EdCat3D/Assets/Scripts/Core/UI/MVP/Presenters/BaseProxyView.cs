namespace Core.UI.MVP
{

    public abstract class BaseProxyView<TView> : IProxyView<TView> where TView: IView
    {
        public bool IsPrepared => View != null;

        public TView View { get; protected set; }

        protected readonly IUIManager _uIManager;

        public abstract void Prepare();


        public BaseProxyView(IUIManager uIManager)
        {
            _uIManager = uIManager;
        }
    }
}