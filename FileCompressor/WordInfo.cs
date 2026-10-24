namespace FileCompressor
{
    public class WordInfo(int id, bool isWritten = false)
    {
        public int Id { get; set; } = id;
        public bool IsWritten { get; set; } = isWritten;
    }
}
