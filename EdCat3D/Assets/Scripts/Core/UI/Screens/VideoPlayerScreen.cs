using Core.UI.MVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Core.UI
{

    public class VideoPlayerScreen : BaseScreenView, IVideoPlayerScreeenView
    {
        public override ScreenType Type { get { return ScreenType.VideoPlayer; } }

        public override ViewType View { get { return ViewType.Screen; } }

        public IVideoPlayerScreenPresenter Presenter { get; protected set; }

        [SerializeField] private RawImage _rawImage;
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private Button _skipButton;


        private string _videoName;

        public void InitVideoClip(string videoName, VideoClip videoClip)
        {
            _videoName = videoName;
            _videoPlayer.clip = videoClip;
        }

        public void InitPresenter(IVideoPlayerScreenPresenter presenter)
        {
            Presenter = presenter;
        }

        public void PlayVideo()
        {
            string url = "";
#if UNITY_EDITOR
            url = /*"file://" + */Application.streamingAssetsPath + "/" + _videoName;
#else
            url = "file://" + Application.streamingAssetsPath + "/" + _videoName;
#endif

#if !UNITY_ANDROID
            _videoPlayer.source = VideoSource.Url;
            _videoPlayer.url = url;
#endif

            _videoPlayer.EnableAudioTrack(0, true);
            _videoPlayer.SetTargetAudioSource(0, _audioSource);
            StartCoroutine(PlayVideoInner());
        }

        private IEnumerator PlayVideoInner()
        {
            _videoPlayer.Prepare();
            var waitTime = new WaitForSeconds(0.5f);
            while(_videoPlayer.isPrepared == false)
            {
                yield return null;
                //break;
            }

            _videoPlayer.Play();
            //_audioSource.Play();
        }

        protected override void OnAwake()
        {
            UIManager.RegisterView(this);
            _videoPlayer.loopPointReached += OnVideoClipPlayComplete;
            _videoPlayer.errorReceived += OnVideoPlayerErrorReceived;
            _videoPlayer.SetTargetAudioSource(0, _audioSource);

            _skipButton.onClick.AddListener(OnSkipButtonClickHandler);

            base.OnAwake();
        }

        private void OnVideoPlayerErrorReceived(VideoPlayer source, string message)
        {
            Debug.Log(string.Format("[VideoPlayerScreen][OnVideoPlayerErrorReceived] error: {0}", message));

        }

        protected override void OnDestroyInner()
        {
            _videoPlayer.loopPointReached -= OnVideoClipPlayComplete;
            base.OnDestroyInner();
        }

        private void OnVideoClipPlayComplete(VideoPlayer source)
        {
            Presenter.CompleteVideo();
        }

        private void OnSkipButtonClickHandler()
        {
            _videoPlayer.Stop();
            OnVideoClipPlayComplete(null);
        }
    }
}