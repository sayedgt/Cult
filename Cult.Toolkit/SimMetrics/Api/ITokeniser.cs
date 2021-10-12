using System.Collections.ObjectModel;

// ReSharper disable All 
namespace Cult.Toolkit.SimMetrics.Api
{
    internal interface ITokeniser
    {
        Collection<string> Tokenize(string word);
        Collection<string> TokenizeToSet(string word);

        string Delimiters { get; }

        string ShortDescriptionString { get; }

        ITermHandler StopWordHandler { get; set; }
    }
}

