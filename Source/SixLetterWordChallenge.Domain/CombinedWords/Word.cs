namespace SixLetterWordChallenge.Domain.CombinedWords
{
    public record Word
    {
        public string Value { get; }
        public int Length => Value.Length;

        private Word(string value)
        {
            Value = value;
        }

        public static Word Create(string value) => new(value);
    }
}