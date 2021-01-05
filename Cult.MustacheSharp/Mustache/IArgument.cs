// ReSharper disable All 
namespace Cult.MustacheSharp.Mustache
{
    public interface IArgument
    {
        string GetKey();

        object GetValue(Scope keyScope, Scope contextScope);
    }
}
