using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

public class ApiDemo
{
    public void StartProcess(string processName)
    {
        Process.Start(processName);
    }

    public string DownloadWebPage(string url)
    {
        using (var client = new WebClient())
        {
            return client.DownloadString(url);
        }
    }

    public void WriteToFile(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
    }

    public void DrawRectangle(string imagePath)
    {
        using (var bitmap = new Bitmap(200, 200))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(Brushes.Red, 0, 0, 200, 200);
            }
            bitmap.Save(imagePath, ImageFormat.Png);
        }
    }

    public void UseGenericList()
    {
        List<string> myList = new List<string>();
        myList.Add("Viktoriia");
        myList.Add("Rekonvald");
        myList.Add("310 group");

        foreach (var item in myList)
        {
            Console.WriteLine(item);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ApiDemo apiDemo = new ApiDemo();
        apiDemo.StartProcess("notepad.exe");
        string webpageContent = apiDemo.DownloadWebPage("https://test-english.com");
        apiDemo.WriteToFile("C:\\Users\\victo\\source\\repos\\LAB\\LAB\\file.txt", webpageContent);
        apiDemo.DrawRectangle("C:\\Users\\victo\\source\\repos\\LAB\\LAB\\file.png");
        apiDemo.UseGenericList();

        Console.WriteLine("Demo completed!");
    }
}
