using Core.UI.MVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Core.States
{
    
    public class IntroState:BaseState<ApplicationStates>
    {
        public override ApplicationStates State { get { return ApplicationStates.Intro; } }

        private Core.PlayerData.ILevelProgression _levelProgression;
        //private Core.UI.VideoPlayerScreen _videoPlayerScreen;
        private IVideoPlayerScreenPresenter _videoPlayerScreenPresenter;
        private VideoConfiguration _videoConfiguration;

        public IntroState(Core.PlayerData.ILevelProgression levelProgression
            , VideoConfiguration videoConfiguration, IVideoPlayerScreenPresenter videoPlayerScreenPresenter)
        {
            _levelProgression = levelProgression;
            _videoConfiguration = videoConfiguration;
            _videoPlayerScreenPresenter = videoPlayerScreenPresenter;
        }

        public override void Enter()
        {
            Debug.Log("[IntroState][Enter] OK");
            _videoPlayerScreenPresenter.InitCompletationCommand(OnVideoCompletedhandler);
            CheckIntroVideo();
        }

        public override void Exit()
        {
            _videoPlayerScreenPresenter.Hide();
        }

        private void CheckIntroVideo()
        {
            if (_levelProgression.IsFirstIntroShowed == false)
            {
                Debug.Log("[IntroState][CheckIntroVideo] IsFirstIntroShowed OK");
                var firstIntro = _videoConfiguration.FirstIntroVideoData();
                if (firstIntro != null)
                {
                    PrepareVideoToShow(firstIntro.Url, firstIntro.Clip);
                }
            }
            else if (_levelProgression.IsSecondIntroShowed == false)
            {
                Debug.Log("[IntroState][CheckIntroVideo] IsSecondIntroShowed OK");
                var secondIntro = _videoConfiguration.SecondIntroVideoData();
                if (secondIntro != null)
                {
                    PrepareVideoToShow(secondIntro.Url, secondIntro.Clip);
                }
            }
            else
            {
                Debug.Log("[IntroState][CheckIntroVideo] All intro already showed");
                CompleteState();
            }
        }

        private void PrepareVideoToShow(string url, VideoClip videoClip)
        {
            _videoPlayerScreenPresenter.Init();
            _videoPlayerScreenPresenter.InitVideoClip(url, videoClip);
            _videoPlayerScreenPresenter.Show();
            _videoPlayerScreenPresenter.PlayVideo();
        }

        private void OnVideoCompletedhandler()
        {
            if (_levelProgression.IsFirstIntroShowed == false)
            {
                _levelProgression.MarkFirstIntroShowed();
            }
            else if (_levelProgression.IsSecondIntroShowed == false)
            {
                _levelProgression.MarkSecondIntroShowed();
            }
            CheckIntroVideo();
        }
    }
}