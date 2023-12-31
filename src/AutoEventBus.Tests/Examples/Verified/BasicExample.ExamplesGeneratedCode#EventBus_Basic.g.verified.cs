﻿//HintName: EventBus_Basic.g.cs
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
	private static readonly List<global::Basic> _subList_Basic = new List<global::Basic>(8);

	public static void Subscribe(global::Basic subscriber)
	{
		lock (_subList_Basic)
		{
			_subList_Basic.Add(subscriber);
		}
	}
	public static void Unsubscribe(global::Basic subscriber)
	{
		lock (_subList_Basic)
		{
			_subList_Basic.Remove(subscriber);
		}
	}
}
