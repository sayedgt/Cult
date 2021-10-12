using System;
using System.ComponentModel;
// ReSharper disable All 
namespace Cult.Toolkit.ExtraEventHandler
{
    public static class EventHandlerExtensions
    {
        public static void Raise(this EventHandler handler, object sender, EventArgs e)
        {
            handler?.Invoke(sender, e);
        }
        public static void Raise(this EventHandler handler, object sender)
        {
            handler.Raise(sender, EventArgs.Empty);
        }
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender) where TEventArgs : EventArgs
        {
            handler.Raise(sender, Activator.CreateInstance<TEventArgs>());
        }
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            handler?.Invoke(sender, e);
        }
        public static void RaiseEvent(this EventHandler @this, object sender)
        {
            @this?.Invoke(sender, null);
        }
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender) where TEventArgs : EventArgs
        {
            @this?.Invoke(sender, Activator.CreateInstance<TEventArgs>());
        }
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> @this, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            @this?.Invoke(sender, e);
        }
        public static void RaiseEvent(this PropertyChangedEventHandler @this, object sender, string propertyName)
        {
            @this?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

        public static void HandleEvent<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            EventHandler<T> handler = eventHandler;
            if (handler != null) handler(sender, e);
        }
    }
}
