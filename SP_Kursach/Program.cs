using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SP_Kursach
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите текст для QR-кода:");
                string inputText = args.Length > 0 ? args[0] : Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputText))
                {
                    Console.WriteLine("Ошибка: текст не может быть пустым.");
                    return;
                }

                string fileName = args.Length > 1 ? args[1] : "qrcode.png";
                string outputDir = Environment.GetEnvironmentVariable("OUTPUT_DIR") ?? Directory.GetCurrentDirectory();
                string outputPath = Path.Combine(outputDir, fileName);

                string directory = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Создаем QR-код
                QRCodeGenerator qrGenerator = new();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                using PngByteQRCode qrCode = new(qrCodeData); // Используем PngByteQRCode
                byte[] qrCodeBytes = qrCode.GetGraphic(20);
                File.WriteAllBytes(outputPath, qrCodeBytes);

                Console.WriteLine($"QR-код сохранен как {outputPath}");
                Console.WriteLine($"Платформа: {Environment.OSVersion.Platform}");
                Console.WriteLine($"Путь к файлу: {Path.GetFullPath(outputPath)}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Ошибка доступа: {ex.Message}. Проверьте права на запись в директорию.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}. Проверьте доступность диска.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            Console.WriteLine("Нажмите Enter для выхода...");
            Console.ReadLine();
        }
    }
}