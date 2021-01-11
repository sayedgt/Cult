using System;
using System.ComponentModel;
// ReSharper disable All 
namespace Cult.Extensions.ExtraEventHandler
{
    public static class EventHandlerExtensions
    {
        public static void Raise(this EventHandler handler, object sender, EventArgs e)
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
    }
}
