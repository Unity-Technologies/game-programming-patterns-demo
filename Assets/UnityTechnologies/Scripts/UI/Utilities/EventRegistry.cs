using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DesignPatterns.UI
{
    /// <summary>
    /// EventRegistry is a utility class that manages the registration and unregistration of UI Toolkit events.
    /// It can handle any event derived from EventBase, such as ClickEvent, MouseEnterEvent, etc.
    /// 
    /// This also supports a simplified version of callback registration that allows passing a System.Action instead of System.Action<TEvent>.
    /// This can be useful for simple events where we don't care about the event data.
    /// 
    /// When no longer needed, call Dispose from the client to unregister all registered callbacks automatically.
    /// 
    /// Example usage:
    /// 
    /// 1. Instantiate a new EventRegistry:
    ///    EventRegistry eventRegistry = new EventRegistry();
    /// 
    /// 2. Register a callback to a VisualElement (either with a lambda or named method):
    ///    eventRegistry.RegisterCallback<ClickEvent>(myButton, (evt) => Debug.Log($"Button clicked at position: {evt.mousePosition}"));
    /// 
    /// 3. Register a simple callback to a VisualElement (when you don't need the extra event data like mousePosition, userData, etc.):
    ///    eventRegistry.RegisterCallback<ClickEvent>(myButton, () => Debug.Log("Button clicked"));
    /// 
    /// 4. When you are done with the event registry, dispose it to unregister all callbacks:
    ///    eventRegistry.Dispose();
    ///
    /// Elements that use ChangeEvents (like Sliders and Textfields) can use RegisterValueChangedCallback<T>
    /// 
    /// </summary>

    public class EventRegistry : IDisposable
    {
        // Single delegate to hold all unregister actions
        Action m_UnregisterActions;

        // Registers a callback for a specific VisualElement and event type (e.g. ClickEvent, MouseEnterEvent, etc.). 
        // The callback to unregister is added to the common delegate for later cleanup.
        public void RegisterCallback<TEvent>(VisualElement visualElement, Action<TEvent> callback) where TEvent : EventBase<TEvent>, new()
        {
            EventCallback<TEvent> eventCallback = new EventCallback<TEvent>(callback);
            visualElement.RegisterCallback(eventCallback);

            m_UnregisterActions += () => visualElement.UnregisterCallback(eventCallback);
        }

        // Registers a simplified callback for a specific VisualElement and event type without requiring
        // event data. The unregister action is added to the common delegate for later cleanup.
        public void RegisterCallback<TEvent>(VisualElement visualElement, Action callback) where TEvent : EventBase<TEvent>, new()
        {
            EventCallback<TEvent> eventCallback = new EventCallback<TEvent>((evt) => callback());
            visualElement.RegisterCallback(eventCallback);

            m_UnregisterActions += () => visualElement.UnregisterCallback(eventCallback);
        }

        // Registers a value changed callback for a specific BindableElement e.g. (a Slider or TextField, which use a
        // ChangeEvents). When the value of the bindableElement changes, the callback is invoked and receives the
        // the new value of the BindableElement. 
        // 
        // Like the other methods, the callback to unregister is added to the common delegate for later cleanup.
        public void RegisterValueChangedCallback<T>(BindableElement bindableElement, Action<T> callback) where T : struct
        {
            EventCallback<ChangeEvent<T>> eventCallback = new EventCallback<ChangeEvent<T>>(evt => callback(evt.newValue));
            bindableElement.RegisterCallback(eventCallback);

            m_UnregisterActions += () => bindableElement.UnregisterCallback(eventCallback);
        }

        // Unregisters all callbacks by invoking each stored unregister action, then sets the common delegate to null.
        // Call this method from the client when all the registered callbacks are no longer needed (e.g.,
        // in the client's OnDisable).
        public void Dispose()
        {
            m_UnregisterActions?.Invoke();
            m_UnregisterActions = null;
        }
    }
}
