using UnityEngine.Video;

namespace Core.UI.MVP
{

    public interface IVideoPlayerScreeenView: IView<IVideoPlayerScreenPresenter>, IScreenView
    {
        void InitVideoClip(string videoName, VideoClip videoClip);
        void PlayVideo();
    }

    public interface IVideoPlayerScreenPresenter: IPresenter<IProxyView<IVideoPlayerScreeenView>, IVideoPlayerScreeenView>
    {
        void InitVideoClip(string videoName, VideoClip videoClip);
        void InitCompletationCommand(System.Action callback);
        void CompleteVideo();
        void PlayVideo();
    }

    public class VideoPlayerScreenPresenter : IVideoPlayerScreenPresenter
    {
        public IProxyView<IVideoPlayerScreeenView> ProxyView { get; private set; }
        private System.Action _completationCallback;

        public VideoPlayerScreenPresenter(IProxyView<IVideoPlayerScreeenView> proxyView)
        {
            ProxyView = proxyView;
        }

        public void CompleteVideo()
        {
            _completationCallback.Invoke();
        }

        public void Hide()
        {
            if (ProxyView.IsPrepared == true)
            {
                ProxyView.View.Hide();
            }
        }

        public void Init()
        {
            PrepareProxy();
        }

        public void InitVideoClip(string videoName, VideoClip videoClip)
        {
            ProxyView.View.InitVideoClip(videoName, videoClip);
        }

        public void PlayVideo()
        {
            ProxyView.View.PlayVideo();
        }

        public void Show()
        {
            ProxyView.View.Show();
        }

        public void InitCompletationCommand(System.Action callback)
        {
            _completationCallback = callback;
        }

        private void PrepareProxy()
        {
            ProxyView.Prepare();
            ProxyView.View.InitPresenter(this);
        }
    }
}