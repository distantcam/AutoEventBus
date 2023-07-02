using AutoEventBus;

public partial class Example
{
    [Subscriber]
    public void Do(int value)
    {
    }
    [Subscriber]
    public void AlsoDo(int value)
    {
    }
}
