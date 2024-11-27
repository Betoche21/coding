


namespace DecodeApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // دریافت ورودی‌های کاربر
            Console.WriteLine("Enter the file path:");
            string filePath = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Enter the sender's name:");
            string sender = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Enter the receiver's name:");
            string receiver = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Choose the encoding method (1 or 2):");
            int method = int.Parse(Console.ReadLine());
            Console.Clear();

            IDecode decoder = new Decoder();

            try
            {
                string decodedMessage = decoder.Decode(filePath, sender, receiver, method);
                Console.WriteLine("Done:");
                Console.WriteLine(decodedMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}