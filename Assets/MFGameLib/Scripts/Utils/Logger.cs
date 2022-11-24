using System;
using System.IO;
using UnityEngine;

namespace MFGameLib.Utils
{
    static class Logger
    {

        public enum LoggerType
        {
            DEFAULT, BDD, NETWORK, TRANSFERT, ERROR
        }

        public static void log(string value)
        {
            log(value, LoggerType.DEFAULT);
        }

        public static void log(string value, LoggerType type)
        {
            string logDirectory = Utils.getDataDirectory() + "/logs";

            string filename = getFilename(type);

            log(value, logDirectory, filename);
        }

        public static void log(string[] lines)
        {
            log(lines, LoggerType.DEFAULT);

        }

        public static void log(string[] lines, LoggerType type)
        {
            string logDirectory = Utils.getDataDirectory() + "/logs";

            string filename = getFilename(type);

            log(lines, logDirectory, filename);

        }

        private static void log(string value, string directory, string filename)
        {
            Debug.Log(directory);
            Debug.Log(filename);
            Debug.Log(value);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(directory + "/" + filename))
            {
                File.WriteAllText(directory + "/" + filename, value);
            }
            else
            {
                File.AppendAllText(directory + "/" + filename, value);
            }
            File.AppendAllText(directory + "/" + filename, "\r\n");
        }

        public static void log(string[] lines, string directory, string filename)
        {
            Debug.Log(directory);
            Debug.Log(filename);
            Debug.Log(lines);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // WriteAllLines creates a file, writes a collection of strings to the file,
            // and then closes the file.  You do NOT need to call Flush() or Close().
            if (!File.Exists(directory + "/" + filename))
            {
                File.WriteAllLines(directory + "/" + filename, lines);
            }
            else
            {
                File.AppendAllLines(directory + "/" + filename, lines);
            }
            File.AppendAllText(directory + "/" + filename, "\r\n");
        }

        private static string getFilename(LoggerType type)
        {
            string filename;
            DateTime today = DateTime.Today;
            string todayString = today.ToString("yyyy-MM-dd");
            switch (type)
            {
                case LoggerType.BDD:
                    filename = "bdd-" + todayString + ".log.txt";
                    break;
                case LoggerType.NETWORK:
                    filename = "network-" + todayString + ".log.txt";
                    break;
                case LoggerType.TRANSFERT:
                    filename = "transfert-" + todayString + ".log.txt";
                    break;
                case LoggerType.ERROR:
                    filename = "error-" + todayString + ".log.txt";
                    break;
                default:
                    filename = "default-" + todayString + ".log.txt";
                    break;

            }
            return filename;
        }
    }
}