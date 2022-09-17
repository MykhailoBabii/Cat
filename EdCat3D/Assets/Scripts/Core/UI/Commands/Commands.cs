using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.UI.Commands
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommand<TData>
    {
        void Execuete(TData data);
    }

}