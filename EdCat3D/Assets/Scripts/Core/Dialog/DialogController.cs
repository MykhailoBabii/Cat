using Core.CES;
using Core.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Dialog
{
    public interface IDialogController
    {
        void StartDialog(DialogDescriptor dialog, System.Action onComplete);
    }

    public class DialogController : IDialogController
    {
        private Queue<PhraseDescriptor> _phrases;
        private Queue<IExecutionCommand> _executionCommands;
        private DialogDescriptor _currentDialog;

        private PhraseDescriptor _currentPhrase;
        private IExecutionCommand _currentCommand;

        private Action _onDialogComplete;
        private readonly DI.DiContainer _diContainer;

        public DialogController(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void StartDialog(DialogDescriptor dialog, Action onComplete)
        {
            _currentDialog = dialog;
            _onDialogComplete = onComplete;
            PreprareDialog();
            CheckDialogForCompletation();
        }

        private void PreprareDialog()
        {
            _phrases = new Queue<PhraseDescriptor>(_currentDialog.PhrasesDescriptor);
        }

        private void CheckDialogForCompletation()
        {
            Debug.Log($"[{GetType().Name}][CheckDialogForCompletation] phrases count: {_phrases.Count}");
            if (_phrases.Count > 0)
            {
                PreparePhrases();
            }
            else
            {
                CompleteDialog();
            }
        }

        private void CompleteDialog()
        {
            Debug.Log($"[{GetType().Name}][CompleteDialog] OK");
            _onDialogComplete?.Invoke();
        }

        private void PreparePhrases()
        {
            Debug.Log($"[{GetType().Name}][PreparePhrases] OK");
            _currentPhrase = _phrases.Dequeue();
            _executionCommands = new Queue<IExecutionCommand>(_currentPhrase.Commands);
            CheckPhraseForCompletation();
        }

        private void CheckPhraseForCompletation()
        {
            Debug.Log($"[{GetType().Name}][CheckPhraseForCompletation] commands count: {_executionCommands.Count}");
            if (_executionCommands.Count > 0)
            {
                PrepareCommands();
            }
            else
            {
                CompletePhrase();
            }
        }

        private void CompletePhrase()
        {
            Debug.Log($"[{GetType().Name}][CompletePhrase] OK");
            CheckDialogForCompletation();
        }

        private void PrepareCommands()
        {
            Debug.Log($"[{GetType().Name}][PrepareCommands] OK");
            _currentCommand = _executionCommands.Dequeue();
            _currentCommand.SetOnComplete(OnCommandComplete);
            _currentCommand.Prepare(_diContainer);
            _currentCommand.Execute();
        }

        private void OnCommandComplete(IExecutionCommand executionCommand)
        {
            CheckPhraseForCompletation();
        }
    }
}