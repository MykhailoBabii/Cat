using Core.UI;
using Core.UI.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.States
{


    public class LoadingState : BaseState<ApplicationStates>
    {
        public override ApplicationStates State => ApplicationStates.Loading;

        private ILoadingScreenPresenter _loadingScreenPresenter;
        private ILoadingCommand _loadingCommand;

        public LoadingState(ILoadingScreenPresenter loadingScreenPresenter, ILoadingCommand loadingCommand)
        {
            _loadingScreenPresenter = loadingScreenPresenter;
            _loadingCommand = loadingCommand;
        }

        public override void Enter()
        {
            _loadingScreenPresenter.Init();
            _loadingScreenPresenter.InitCommandOnLoading(_loadingCommand.Execute);
            _loadingScreenPresenter.InitCommandOnLoadingComplete(CompleteState);
            _loadingScreenPresenter.Show();
        }

        public override void Exit()
        {
            _loadingScreenPresenter.Hide();
        }
    }

    public interface ILoadingCommand : ICommand
    {
        void InitExecutionAction(System.Action action);
    }

    public class LoadingCommand : ILoadingCommand
    {
        private System.Action _loadingCommand;
        public void Execute()
        {
            _loadingCommand?.Invoke();
            _loadingCommand = null;
        }

        public void InitExecutionAction(Action action)
        {
            _loadingCommand = action;
        }
    }
}