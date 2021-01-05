using System.Collections.ObjectModel;

// ReSharper disable All 
namespace Cult.SimMetrics.Api
{
    public interface ITokeniser
    {
        Collection<string> Tokenize(string word);
        Collection<string> TokenizeToSet(string word);

        string Delimiters { get; }

        string ShortDescriptionString { get; }

        ITermHandler StopWordHandler { get; set; }
    }
}

