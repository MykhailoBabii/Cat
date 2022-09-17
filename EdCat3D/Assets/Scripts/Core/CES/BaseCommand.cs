using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{
    public interface IExecutionCommand
    {
        void Execute();
        void Prepare(DI.DiContainer container);

        void SetOnComplete(System.Action<IExecutionCommand> onCommandComplete);
    }

    public abstract class BaseCommand : IExecutionCommand
    {
        protected Action<IExecutionCommand> _onCommandComplete;

        public abstract void Execute();

        public virtual void Prepare(DI.DiContainer container)
        {
            
        }

        public void SetOnComplete(Action<IExecutionCommand> onCommandComplete)
        {
            _onCommandComplete = onCommandComplete;
        }

        protected void CompleteCommand()
        {
            _onCommandComplete?.Invoke(this);
        }
    }
}