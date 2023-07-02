using AutoEventBus;

public class MultipleSubscribersOneClass
{
    [Subscriber]
    public void Do(int value)
    {
    }

    [Subscriber]
    public void AlsoDo(System.Int32 value)
    {
    }

    [Subscriber]
    public void Different(string value)
    {
    }
}
