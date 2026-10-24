using System.Text.RegularExpressions;

namespace FileCompressor
{
    public class RepeatedWordsCompressor(string path, string outputPath)
    {
        private readonly string path = path;
        private readonly string outputPath = outputPath;
        Dictionary<string, WordInfo> commonMapper = [];

        public void Compress()
        {
            int last = 0;

            using var reader = new StreamReader(path);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] words = line.Split(
                    (char[]?)null,
                    StringSplitOptions.RemoveEmptyEntries
                );

                foreach (string word in words)
                {
                    if (commonMapper.ContainsKey(word)) continue;
                    commonMapper[word] = new WordInfo(last);
                    last++;
                }
            }


            _WriteCompressFile();

        }

        void _WriteCompressFile()
        {
            using StreamWriter writer = new(outputPath);
            using StreamReader reader = new(path);
            
        



            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] words = line.Split(
                    (char[]?)null,
                    StringSplitOptions.RemoveEmptyEntries
                );

                foreach (string word in words)
                {
                    var item = commonMapper[word];
                    if (item.IsWritten)
                        writer.Write(item.Id.ToString() +" ");
                    else
                    {
                        writer.Write($"[\"{word}\":{item.Id}] ");
                        item.IsWritten = true;
                    }
                }
            }
        }


        public void Decompress(string filePath)
        {
            using StreamWriter writer = new(filePath+ ".decompressed.txt");
            using StreamReader reader = new(filePath);
            Dictionary<int, string> compressedToWord = new();
            string? line;
            while((line = reader.ReadLine()) != null)
            {
                string[] words = line.Split(
                    (char[]?)null,
                    StringSplitOptions.RemoveEmptyEntries
                );

                foreach (string word in words)
                {

                    Match match = Regex.Match(word, @"^\[""(.*)""\s*:\s*(\d+)\]$");
                    if (match.Success)
                    {
                        string wordInFile = match.Groups[1].Value;
                        int number = int.Parse(match.Groups[2].Value);

                        compressedToWord[number] = wordInFile;
                        writer.Write(wordInFile);

                    }
                    else
                    {
                        writer.Write(compressedToWord[Convert.ToInt32(word)]);
                    }
                }
            }
        }

        public void Decompress()
        {
            Decompress(outputPath);
        }
    }
}
