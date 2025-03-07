public class PausedEvent : IEvent
{
    public readonly bool Paused;

    public PausedEvent(bool paused)
    {
        Paused = paused;
    }
}