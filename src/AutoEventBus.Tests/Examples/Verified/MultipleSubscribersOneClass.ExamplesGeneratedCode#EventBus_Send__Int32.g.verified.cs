﻿//HintName: EventBus_Send__Int32.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by https://github.com/distantcam/AutoEventBus
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

public static partial class EventBus
{
	public static void Send(int value)
	{
		lock (_subList_MultipleSubscribersOneClass)
		{
#if NET5_0_OR_GREATER
			foreach (var receiver in System.Runtime.InteropServices.CollectionsMarshal.AsSpan(_subList_MultipleSubscribersOneClass))
#else
			foreach (var receiver in _subList_MultipleSubscribersOneClass)
#endif
			{
				receiver.Do(value);
				receiver.AlsoDo(value);
			}
		}
	}
}
