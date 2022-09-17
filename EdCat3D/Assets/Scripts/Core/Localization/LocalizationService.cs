using Core.PlayerData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{

    public interface ILocalizationService
    {
        string GetPhrase(string key);
    }

    public class LocalizationService : ILocalizationService
    {
        private ISettings _settings;
        private LocalizationDescriptor _languageDescriptor;

        public LocalizationService(ISettings settings, LocalizationDescriptor languageDescriptor)
        {
            _settings = settings;
            _languageDescriptor = languageDescriptor;
        }
        
        public string GetPhrase(string key)
        {
            return _languageDescriptor.GetPhrase(_settings.Language, key);
        }
    }
}