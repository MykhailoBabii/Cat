using Core.Dialog;
using Core.PlayerData;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.States
{


    public class LobbyState : BaseState<ApplicationStates>
    {
        public override ApplicationStates State { get { return ApplicationStates.Lobby; } }

        private ISettings _settings;
        private ILevelProgression _levelProgression;
        private IDialogController _dialogController;
        private DialogDataBase _dialogDataBase;

        public LobbyState(ISettings settings, ILevelProgression levelProgression
            , IDialogController dialogController
            , DialogDataBase dialogDataBase)
        {
            _settings = settings;
            _levelProgression = levelProgression;
            _dialogController = dialogController;
            _dialogDataBase = dialogDataBase;
        }

        public override void Enter()
        {
            Debug.Log("[LobbyState][Enter] OK");
            if (_levelProgression.IsFirstDialogShowed == false)
            {
                var dialogDescriptor = _dialogDataBase.GetDialog("first_dialog");
                _dialogController.StartDialog(dialogDescriptor, null);
            }
        }

        public override void Exit()
        {
            
        }

        
    }
}