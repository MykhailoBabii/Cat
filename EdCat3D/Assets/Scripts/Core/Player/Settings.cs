using Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.PlayerData
{
    public interface ISettings
    {
        bool SoundFXOn { get; }
        bool MusicOn { get; }
        bool VibroOn { get; }
        Localization.LanguageType Language { get; }

        void SetSound(bool isOn);
        void SetMusic(bool isOn);
        void SetVibro(bool isOn);
        void SetLanguage(Localization.LanguageType language);

    }

    public class Settings : ISettings
    {
        public bool SoundFXOn { get; private set; }

        public bool MusicOn { get; private set; }

        public bool VibroOn { get; private set; }

        public LanguageType Language { get; private set; }

        public Settings()
        {
            Language = LanguageType.RU;
        }

        public void SetLanguage(LanguageType language)
        {
            Language = language;
        }

        public void SetMusic(bool isOn)
        {
            MusicOn = isOn;
        }

        public void SetSound(bool isOn)
        {
            SoundFXOn = isOn;
        }

        public void SetVibro(bool isOn)
        {
            VibroOn = isOn;
        }
    }
}