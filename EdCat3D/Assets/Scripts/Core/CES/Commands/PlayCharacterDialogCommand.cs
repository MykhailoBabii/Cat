using Core.DI;
using Core.Dialog;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{

    
    public class PlayCharacterDialogCommand : BaseCommand
    {
        [SerializeField] private string _audioDialogKey;

        private ICharacterDialogController _characterDialogController;
        private ResourceInner.ResourceProvider _resourceProvider;

        public override void Prepare(DiContainer container)
        {
            _characterDialogController = container.GetService<ICharacterDialogController>();
            _resourceProvider = container.GetService<ResourceInner.ResourceProvider>();
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, audioKey: {_audioDialogKey}");

            var audioClip = _resourceProvider.GetAudio(_audioDialogKey);
            if (audioClip == null)
            {
                Debug.LogError($"[{GetType().Name}][Execute] can't find audio with key: {_audioDialogKey}");
                return;
            }

            _characterDialogController.PlayCharacterDialog(audioClip);
            CompleteCommand();
        }
    }
}