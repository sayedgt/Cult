namespace Cult.ParserKit
{
    public readonly struct CharLocation
    {
        public CharLocation(char character, int line, int column)
        {
            Line = line;
            Column = column;
            Character = character;
        }
        public int Line { get; }
        public int Column { get; }
        public char Character { get; }

    }
}
