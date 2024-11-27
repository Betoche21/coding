public class Decoder : IDecode
{
    private Dictionary<char, int> alphabetMap;

    public Decoder()
    {
        alphabetMap = new Dictionary<char, int>();
        for (char c = 'a'; c <= 'z'; c++) alphabetMap[c] = c - 'a' + 1;
    }

    private int CalculateKey(string sender, string receiver, int method)
    {
        int senderSum = sender.ToLower().Sum(c => alphabetMap.ContainsKey(c) ? alphabetMap[c] : 0);
        int receiverSum = receiver.ToLower().Sum(c => alphabetMap.ContainsKey(c) ? alphabetMap[c] : 0);

        if (method == 1)
            return senderSum + receiverSum;
        else
            return (int)Math.Floor((double)(senderSum * receiverSum) / (senderSum + receiverSum));
    }

    // متد عمومی برای خواندن فایل‌ها
    private List<int> ReadFile(string filePath)
    {
        string fileExtension = Path.GetExtension(filePath).ToLower();

        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found!");

        string[] lines = File.ReadAllLines(filePath);
        List<int> numbers = new List<int>();

        if (fileExtension == ".txt")
        {
            // فایل txt
            numbers = lines.SelectMany(line => line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                           .Select(int.Parse).ToList();
        }
        else if (fileExtension == ".ini")
        {
            // فایل ini
            // در اینجا فرض می‌کنیم که داده‌ها به صورت Value1=عدد در هر خط ذخیره می‌شوند.
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2 && int.TryParse(parts[1], out int number))
                {
                    numbers.Add(number);
                }
            }
        }
        else if (fileExtension == ".csv")
        {
            // فایل csv
            numbers = lines.SelectMany(line => line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                           .Select(int.Parse).ToList();
        }
        else
        {
            throw new NotSupportedException("Unsupported file format!");
        }

        return numbers;
    }

    public string Decode(string filePath, string sender, string receiver, int method)
    {
        // خواندن فایل با استفاده از متد ReadFile
        List<int> numbers = ReadFile(filePath);

        int key = CalculateKey(sender, receiver, method);
        List<int> decodedNumbers = numbers.Select(n => n - key).ToList();

        // تبدیل اعداد به متن
        return TextToNumber.ConvertNumbersToText(decodedNumbers);
    }
}