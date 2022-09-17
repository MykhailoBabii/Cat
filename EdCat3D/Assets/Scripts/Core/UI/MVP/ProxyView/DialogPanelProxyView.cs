using Core.UI.MVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class DialogPanelProxyView : BaseProxyView<IDialogPanelView>
    {
        public DialogPanelProxyView(IUIManager uIManager) : base(uIManager)
        {
        }

        public override void Prepare()
        {
            if (IsPrepared)
            {
                return;
            }

            View = _uIManager.GetPanels<DialogPanelView>(PanelType.Dialog);
        }
    }
}