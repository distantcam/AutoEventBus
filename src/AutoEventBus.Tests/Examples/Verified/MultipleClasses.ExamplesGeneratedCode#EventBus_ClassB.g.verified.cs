﻿//HintName: EventBus_ClassB.g.cs
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
	private static readonly List<global::ClassB> _subList_ClassB = new List<global::ClassB>(8);

	public static void Subscribe(global::ClassB subscriber)
	{
		lock (_subList_ClassB)
		{
			_subList_ClassB.Add(subscriber);
		}
	}
	public static void Unsubscribe(global::ClassB subscriber)
	{
		lock (_subList_ClassB)
		{
			_subList_ClassB.Remove(subscriber);
		}
	}
}
