// ReSharper disable All 
namespace Cult.ParserKit
{
    public readonly struct CharLocation
    {
        public char Character { get; }
        public int Column { get; }
        public int Line { get; }
        public CharLocation(char character, int line, int column)
        {
            Line = line;
            Column = column;
            Character = character;
        }
    }
}
