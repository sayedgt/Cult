﻿using System;
// ReSharper disable All 
namespace Cult.Toolkit.ExtraAction
{
    public static class ActionExtensions
    {
        public static Action<object> ToActionObject<T>(this Action<T> actionT)
        {
            return actionT == null ? null : new Action<object>(o => actionT((T)o));
        }
    }
}
