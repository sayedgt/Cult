﻿using System.Linq;
using System.Reflection;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

namespace NSubstitute
{
    public static class NSubstituteExtensions
    {
        /*
          var sub = Substitute.For<Foo>();
          sub.Protected("DoStuff", Arg.Is<int>(x => x < 10)).Returns(x => x.Arg<int>().ToString());
          
          var sub = Substitute.For<Foo>();
          sub.ReallyDoStuff(5);
          sub.Received().Protected("DoStuff", 5);
          sub.DidNotReceive().Protected("DoStuff", 2);
        */
        public static object Protected(this object target, string methodName, params object[] args)
        {
            var type = target.GetType();
            var method = type
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Single(x => x.Name == methodName);
            return method.Invoke(target, args);
        }
    }
}
