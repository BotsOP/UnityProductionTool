using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    IMAGE_CHANGED,
    UPDATE,
    FIXED_UPDATE,
    DISTRACTION,
    PLAYER_ATTACKED,
    FLASHLIGHT,
    PLAYER_SNEAKING
}

public static class EventSystem
{
    private static Dictionary<EventType, System.Action> eventRegister = new Dictionary<EventType, System.Action>();

    public static void Subscribe(EventType evt, System.Action func)
    {
        if (!eventRegister.ContainsKey(evt))
        {
            eventRegister.Add(evt, null);
        }

        eventRegister[evt] += func;
    }

    public static void Unsubscribe(EventType evt, System.Action func)
    {
        if (eventRegister.ContainsKey(evt))
        {
            eventRegister[evt] -= func;
        }
    }

    public static void RaiseEvent(EventType evt)
    {
        eventRegister[evt]?.Invoke();
    }
}

public static class EventSystem<T>
{
    private static Dictionary<EventType, System.Action<T>> eventRegister = new Dictionary<EventType, System.Action<T>>();

    public static void Subscribe(EventType evt, System.Action<T> func)
    {
        if (!eventRegister.ContainsKey(evt))
        {
            eventRegister.Add(evt, null);
        }

        eventRegister[evt] += func;
    }

    public static void Unsubscribe(EventType evt, System.Action<T> func)
    {
        if (eventRegister.ContainsKey(evt))
        {
            eventRegister[evt] -= func;
        }
    }

    public static void RaiseEvent(EventType evt, T arg)
    {
        eventRegister[evt]?.Invoke(arg);
    }
}

public static class EventSystem<A, T>
{
    private static Dictionary<EventType, System.Action<A, T>> eventRegister = new Dictionary<EventType, System.Action<A, T>>();

    public static void Subscribe(EventType evt, System.Action<A, T> func)
    {
        if (!eventRegister.ContainsKey(evt))
        {
            eventRegister.Add(evt, null);
        }

        eventRegister[evt] += func;
    }

    public static void Unsubscribe(EventType evt, System.Action<A, T> func)
    {
        if (eventRegister.ContainsKey(evt))
        {
            eventRegister[evt] -= func;
        }
    }

    public static void RaiseEvent(EventType evt, A arg,  T arg2)
    {
        eventRegister[evt]?.Invoke(arg, arg2);
    }
}

public static class EventSystem<TA, A, T>
{
    private static Dictionary<EventType, System.Action<TA, A, T>> eventRegister = new Dictionary<EventType, System.Action<TA, A, T>>();

    public static void Subscribe(EventType evt, System.Action<TA, A, T> func)
    {
        if (!eventRegister.ContainsKey(evt))
        {
            eventRegister.Add(evt, null);
        }

        eventRegister[evt] += func;
    }

    public static void Unsubscribe(EventType evt, System.Action<TA, A, T> func)
    {
        if (eventRegister.ContainsKey(evt))
        {
            eventRegister[evt] -= func;
        }
    }

    public static void RaiseEvent(EventType evt, TA arg, A arg2,  T arg3)
    {
        eventRegister[evt]?.Invoke(arg, arg2, arg3);
    }
}
