using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;



public class EventManager : MonoBehaviour
{
    private Dictionary<GameEventType, Action> eventDictionary = new();

    /// <summary>
    /// run them in start methode
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public void Subscribe(GameEventType eventType, Action listener)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] += listener;
        }
        else
        {
            eventDictionary[eventType] = listener;
        }
    }

    public void Unsubscribe(GameEventType eventType, Action listener)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] -= listener;


            if (eventDictionary[eventType] == null)
                eventDictionary.Remove(eventType);
        }
    }

    public void Raise(GameEventType eventType)
    {
        if (eventDictionary.TryGetValue(eventType, out var thisEvent))
        {
            thisEvent?.Invoke();
        }
        else
        {
            Debug.LogWarning($"No listeners found for event {eventType}");
        }
    }
}