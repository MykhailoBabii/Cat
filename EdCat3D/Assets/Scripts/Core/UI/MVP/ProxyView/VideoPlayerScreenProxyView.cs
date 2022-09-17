using Core.UI.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Core.UI.MVP
{

    public class VideoPlayerScreenProxyView : BaseProxyView<IVideoPlayerScreeenView>
    {
        public VideoPlayerScreenProxyView(IUIManager uIManager) : base(uIManager)
        {
        }

        public override void Prepare()
        {
            View = _uIManager.GetScreen<IVideoPlayerScreeenView>(ScreenType.VideoPlayer);
        }
    }
}