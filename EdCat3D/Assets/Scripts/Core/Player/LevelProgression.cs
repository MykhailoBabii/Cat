using Core.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.PlayerData
{
    public interface ILevelProgression : IStoragable, IStoragable<StorageData>
    {
        bool IsFirstIntroShowed { get; }
        bool IsSecondIntroShowed { get; }
        bool IsFirstDialogShowed { get; }

        void SetLevel(int level);
        void RestartLevels();

        void MarkFirstIntroShowed();
        void MarkSecondIntroShowed();
        void MarkFirstDialogShowed();
    }


    public class LevelProgression : ILevelProgression
    {
        private const string StorageKey = "LevelProgression";//nameof(LevelProgression);
        private int _currentLevel = 1;

        public int CurentLevel { get { return _currentLevel; } }

        public bool IsFirstIntroShowed { get; private set; }

        public bool IsSecondIntroShowed { get; private set; }
        public bool IsFirstDialogShowed { get; private set; }

        public LevelProgression()
        {
            IsFirstIntroShowed = false;
            IsSecondIntroShowed = false;
        }

        public void Load()
        {
            _currentLevel = PlayerPrefs.GetInt(StorageKey, 0);
        }

        public void Load(StorageData data)
        {
            IsFirstIntroShowed = data.ProgressData.IsFirstIntroShowed;
            IsSecondIntroShowed = data.ProgressData.IsSecondIntroShowed;
            IsFirstDialogShowed = data.ProgressData.IsFirstDialogShowed;
        }

        public void RestartLevels()
        {
            _currentLevel = 1;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(StorageKey, _currentLevel);
        }

        public void Save(StorageData data)
        {
            data.ProgressData = PrepareLevelProgressData();
        }

        public void SetLevel(int level)
        {
            _currentLevel = level;
        }

        private LevelProgressData PrepareLevelProgressData()
        {
            return new LevelProgressData()
            {
                IsFirstIntroShowed = this.IsFirstIntroShowed
                ,
                IsSecondIntroShowed = this.IsSecondIntroShowed
                ,
                IsFirstDialogShowed = this.IsFirstDialogShowed
            };
        }

        public void MarkFirstIntroShowed()
        {
            IsFirstIntroShowed = true;
        }

        public void MarkSecondIntroShowed()
        {
            IsSecondIntroShowed = true;
        }

        public void MarkFirstDialogShowed()
        {
            IsFirstDialogShowed = true;
        }
    }
}