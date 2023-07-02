namespace AutoEventBus;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
[System.Diagnostics.Conditional("AUTOEVENTBUS_USAGES")]
public sealed class SubscriberAttribute : Attribute
{
    [System.Runtime.CompilerServices.CompilerGenerated]
    public SubscriberAttribute()
    {
    }
}
