using System;
using System.IO;
class Program
{
    static void Main()
    {
        string baseDir = @"E:\OOP_lab08";
        Directory.CreateDirectory(baseDir);
        Console.WriteLine("Створено каталог: " + baseDir);
        string knGroup = Path.Combine(baseDir, "KN1-B23");
        string studentDir = Path.Combine(baseDir, "Navrotska");
        string sourcesDir = Path.Combine(baseDir, "Sources");
        string reportsDir = Path.Combine(baseDir, "Reports");
        string textsDir = Path.Combine(baseDir, "Texts");
        Directory.CreateDirectory(knGroup);
        Directory.CreateDirectory(studentDir);
        Directory.CreateDirectory(sourcesDir);
        Directory.CreateDirectory(reportsDir);
        Directory.CreateDirectory(textsDir);
        Console.WriteLine("Створено підкаталоги.");
        CopyDirectory(sourcesDir, Path.Combine(studentDir, "Sources"));
        CopyDirectory(reportsDir, Path.Combine(studentDir, "Reports"));
        CopyDirectory(textsDir, Path.Combine(studentDir, "Texts"));
        Console.WriteLine("Скопійовано каталоги в Navrotska.");
        string destination = Path.Combine(knGroup, "Navrotska");
        if (Directory.Exists(destination))
        {
            Directory.Delete(destination, true);
        }
        Directory.Move(studentDir, destination);
        Console.WriteLine("Переміщено Navrotska у KN1-B23.");
        string textDirInNavrotska = Path.Combine(destination, "Texts");
        string infoFile = Path.Combine(textDirInNavrotska, "dirinfo.txt");
        using (StreamWriter writer = new StreamWriter(infoFile))
        {
            writer.WriteLine("Інформація про каталог: " + textDirInNavrotska);
            string[] files = Directory.GetFiles(textDirInNavrotska);
            string[] dirs = Directory.GetDirectories(textDirInNavrotska);

            writer.WriteLine("\nПідкаталоги:");
            foreach (var dir in dirs)
                writer.WriteLine(dir);

            writer.WriteLine("\nФайли:");
            foreach (var file in files)
                writer.WriteLine(file);
        }
        Console.WriteLine("Файл dirinfo.txt створено.");
    }
    static void CopyDirectory(string sourceDir, string destDir)
    {
        Directory.CreateDirectory(destDir);

        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(destDir, fileName);
            File.Copy(file, destFile, true);
        }
        foreach (string directory in Directory.GetDirectories(sourceDir))
        {
            string dirName = Path.GetFileName(directory);
            string destSubDir = Path.Combine(destDir, dirName);
            CopyDirectory(directory, destSubDir);
        }
    }
}