using System;
// ReSharper disable All 
namespace Castle.DynamicProxy
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InterceptorAttribute : Attribute
    {
        public Type Interceptor { get; }
        public string Methods { get; set; }
        public InterceptorAttribute(Type interceptor)
        {
            Interceptor = interceptor;
        }
    }
}
