using Core.DI;
using Core.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{
    public class SetObjectPositionCommand : BaseCommand
    {
        [SerializeField] private string _keyObjectWho;
        [SerializeField] private string _keyObjectTo;

        private Transform _objectWho;
        private Transform _objectTarget;

        public override void Prepare(DiContainer container)
        {
            var objectHolder = container.GetService<IDynamicObjectHolder>();
            _objectWho = objectHolder.GetObject<Transform>(_keyObjectWho);
            _objectTarget = objectHolder.GetObject<Transform>(_keyObjectTo);
        }

        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, _keyObjectWho: {_keyObjectWho}, _keyObjectTo: {_keyObjectTo}");

            _objectWho.position = _objectTarget.position;
            CompleteCommand();
        }
    }
}