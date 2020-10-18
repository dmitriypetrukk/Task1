using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string path = @"E:\Task1\File.txt";
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                var newLog = new List<Log>();
                string line;

                while ((line = await sr.ReadLineAsync()) != null)
                {
                    if (Regex.Match(line, @"[|]").Length > 0)
                    {
                        newLog.Add(new Log()
                        {
                            timeStamp = DateTime.Parse(Regex.Match(line, @"\d{2}/\d{2}/\d{2}\s\d{2}:\d{2}:\d{2}.\d{3}").Value),
                            logLevel = Regex.Match(line, @"[A-Z]{4,5}").Value,
                            thread = Convert.ToInt32(Regex.Match(line, @"\s\d{1,2}\s").Value),
                            processName = Regex.Match(line, @"Con(\w*)").Value,
                            methodName = Regex.Match(line, @"\s[A-Z]\w*[`]*\w*[.]\S+\s").Value,
                            message = Regex.Replace(line, @"\d{2}/\d{2}/\d{2}\s\d{2}:\d{2}:\d{2}.\d{3}\s[|]\s[A-Z]{4,5}\s*[|]\s\d{1,2}\s+.+Con(\w*)\s*[|]\s[A-Z]\w*[`]*\w*[.]\S+\s", "")
                        });
                    }
                }

                int i = 0;
                while (i < 5)
                {
                    newLog[i + 15].GetInfo();
                    i++;
                }
            }
        }
    }
}

