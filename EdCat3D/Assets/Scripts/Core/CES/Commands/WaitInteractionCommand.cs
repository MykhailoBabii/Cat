using Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.CES
{

    
    public class WaitInteractionCommand : BaseCommand
    {
        [SerializeField] private InteractionType _interactionType;
        public override void Execute()
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, interaction: {_interactionType}");

            EventAggregator.Subscribe<InteractionEvent>(OnInteractionEventHandler);
        }

        private void OnInteractionEventHandler(object sender, InteractionEvent eventData)
        {
            Debug.Log($"[{GetType().Name}][Execute] OK, interaction: {_interactionType}, event: {eventData.Type}");

            if (eventData.Type != _interactionType)
            {
                return;
            }

            EventAggregator.Unsubscribe<InteractionEvent>(OnInteractionEventHandler);

            CompleteCommand();
        }
    }
}