using System;
using System.IO;

// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    internal interface IGenerator
    {
        void GetText(TextWriter writer, Scope keyScope, Scope contextScope, Action<Substitution> postProcessor);
    }
}
