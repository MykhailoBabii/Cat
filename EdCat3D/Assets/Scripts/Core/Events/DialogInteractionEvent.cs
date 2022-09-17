namespace Core.Events
{
    public enum InteractionType
    {
        NextButtonClick
    }

    public class InteractionEvent
    {
        public InteractionType Type;
    }
}