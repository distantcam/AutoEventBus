﻿//HintName: EventBus_MultipleSubscribersOneClass.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by https://github.com/distantcam/AutoEventBus
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;

public static partial class EventBus
{
	private static readonly List<global::MultipleSubscribersOneClass> _subList_MultipleSubscribersOneClass = new List<global::MultipleSubscribersOneClass>(8);

	public static void Subscribe(global::MultipleSubscribersOneClass subscriber)
	{
		lock (_subList_MultipleSubscribersOneClass)
		{
			_subList_MultipleSubscribersOneClass.Add(subscriber);
		}
	}
	public static void Unsubscribe(global::MultipleSubscribersOneClass subscriber)
	{
		lock (_subList_MultipleSubscribersOneClass)
		{
			_subList_MultipleSubscribersOneClass.Remove(subscriber);
		}
	}
}
