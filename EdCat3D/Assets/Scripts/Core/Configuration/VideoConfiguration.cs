using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


namespace Core
{

    [CreateAssetMenu(fileName ="VideoConfig", menuName ="Create/Video Config")]
    public class VideoConfiguration:ScriptableObject
    {
        [SerializeField] private List<VideoConfigData> _videoConfigDatas = new List<VideoConfigData>();
        [SerializeField] private string _fisrtIntroVideo;
        [SerializeField] private string _secondIntroVideo;

        public VideoConfigData GetVideoConfigData(string name)
        {
            var result = _videoConfigDatas.Find(data => data.Name == name);
            return result;
        }

        public VideoConfigData FirstIntroVideoData()
        {
            return GetVideoConfigData(_fisrtIntroVideo);
        }

        public VideoConfigData SecondIntroVideoData()
        {
            return GetVideoConfigData(_secondIntroVideo);
        }

    }

    [System.Serializable]
    public class VideoConfigData
    {
        [SerializeField] private string _name;
        [SerializeField] private VideoClip _videoClip;
        [SerializeField] private string _videoUrl;

        public string Name { get { return _name; } }
        public VideoClip Clip { get { return _videoClip; } }

        public string Url { get { return _videoUrl; } }
    }
}