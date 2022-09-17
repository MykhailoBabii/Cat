using Core.CES;
using Core.DI;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Dialog
{

    public class PhraseDescriptor 
    {
        public string PhraseId;
        public List<IExecutionCommand> Commands;
    }
}