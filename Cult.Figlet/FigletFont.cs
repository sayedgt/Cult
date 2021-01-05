using System.Collections.Generic;
using System.IO;
using System.Linq;
// ReSharper disable IdentifierTypo

// ReSharper disable All 
namespace Cult.Figlet
{
    internal class FigletFont
    {
        internal string Signature { get; private set; }
        internal string HardBlank { get; private set; }
        internal int Height { get; private set; }
        internal int BaseLine { get; private set; }
        internal int MaxLenght { get; private set; }
        internal int OldLayout { get; private set; }
        internal int CommentLines { get; private set; }
        internal int PrintDirection { get; private set; }
        internal int FullLayout { get; set; }
        internal int CodeTagCount { get; set; }
        internal List<string> Lines { get; set; }

        internal FigletFont(string flfFontFile)
        {
            LoadFont(flfFontFile);
        }
        internal FigletFont(Stream flfFontFile)
        {
            LoadFont(flfFontFile);
        }
        internal FigletFont()
        {
            LoadFont();
        }

        private void LoadLines(List<string> fontLines)
        {
            Lines = fontLines;
            var configString = Lines.First();
            var configArray = configString.Split(' ');
            Signature = configArray.First().Remove(configArray.First().Length - 1);
            if (Signature != "flf2a") return;
            HardBlank = configArray.First().Last().ToString();
            Height = configArray.GetIntValue(1);
            BaseLine = configArray.GetIntValue(2);
            MaxLenght = configArray.GetIntValue(3);
            OldLayout = configArray.GetIntValue(4);
            CommentLines = configArray.GetIntValue(5);
            PrintDirection = configArray.GetIntValue(6);
            FullLayout = configArray.GetIntValue(7);
            CodeTagCount = configArray.GetIntValue(8);
        }

        private void LoadFont()
        {
            using (var stream = this.GetResourceStream("Cult.Figlet.Font.standard.flf"))
            {
                LoadFont(stream);
            }
        }

        private void LoadFont(string flfFontFile)
        {
            using (var fso = File.Open(flfFontFile, FileMode.Open))
            {
                LoadFont(fso);
            }
        }

        private void LoadFont(Stream fontStream)
        {
            var fontData = new List<string>();
            using (var reader = new StreamReader(fontStream))
            {
                while (!reader.EndOfStream)
                {
                    fontData.Add(reader.ReadLine());
                }
            }
            LoadLines(fontData);
        }

    }
}