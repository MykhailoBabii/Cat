using Core.UI.MVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{


    public class LoadingScreenProxyView : BaseProxyView<ILoadingScreenView>
    {
        public LoadingScreenProxyView(IUIManager uIManager) : base(uIManager)
        {
        }

        public override void Prepare()
        {
            View = _uIManager.GetScreen<ILoadingScreenView>(ScreenType.Loading);
        }
    }
}