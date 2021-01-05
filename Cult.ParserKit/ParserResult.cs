//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
// ReSharper disable All 
//namespace CodeBox.ParserKit
//{
//    public sealed class ParserResult<TResult>
//        where TResult : class, new()
//    {
//        public TResult Result { get; }
//        public ICollection<string> Errors { get; }

//        public bool HasError => Errors != null && Errors.Any();

//        public ParserResult()
//        {
//            Result = new TResult();
//            Errors = new List<string>();
//        }
//    }
//}
