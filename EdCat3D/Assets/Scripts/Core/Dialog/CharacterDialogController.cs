using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Dialog
{
    public enum CharacterType
    {
        Cat,
        Sectoid,
        Zoe
    }

    public interface ICharacterDialogController
    {
        void ShowCharacter(CharacterType character);
        void HideCharacter(CharacterType character);

        void PlayCharacterAnimation(CharacterType character, string animationName);
        void PlayCharacterDialog(AudioClip audioClip);
        void StopCharacterDialog();
    }

    public class CharacterDialogController : MonoBehaviour, ICharacterDialogController
    {
        [SerializeField] private AudioSource _characterDialogAudioSource;
        [SerializeField] private List<CharacterAnimationController> _characterAnimationControllers = 
            new List<CharacterAnimationController>();

        public void HideCharacter(CharacterType character)
        {
            var animationControllers = GetAnimationController(character);
            animationControllers.ForEach(controller => controller.gameObject.SetActive(false));
        }

        public void PlayCharacterAnimation(CharacterType character, string animationName)
        {
            var animationControllers = GetAnimationController(character);
            animationControllers.ForEach(controller => controller.PlayAnimation(animationName));
        }

        public void PlayCharacterDialog(AudioClip audioClip)
        {
            _characterDialogAudioSource.clip = audioClip;
            _characterDialogAudioSource.Play();
        }

        public void ShowCharacter(CharacterType character)
        {
            var animationControllers = GetAnimationController(character);
            animationControllers.ForEach(controller => controller.gameObject.SetActive(true));
        }

        public void StopCharacterDialog()
        {
            _characterDialogAudioSource.Stop();
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            HideAllCharacters();
        }

        private List<CharacterAnimationController> GetAnimationController(CharacterType characterType)
        {
            var result = _characterAnimationControllers.FindAll(character => character.Character == characterType);
            return result;
        }

        private void HideAllCharacters()
        {
            foreach (var characterAnimationController in _characterAnimationControllers)
            {
                characterAnimationController.gameObject.SetActive(false);
            }
        }
    }
}