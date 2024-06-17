using System;
using System.IO;
using UnityEngine;

namespace FpvDroneSimulator.Common.Utilities.Logging
{
    public class ExternalFileLogger : ILogger, IDisposable
    {
        public ExternalFileLogger(string fileName, string fileType, bool isEnabled = true)
        {
            IsEnabled = isEnabled;
            string filePath = $"{Application.dataPath}/{fileName}.{fileType}";
            _streamWriter = new StreamWriter(File.Open(filePath, FileMode.Append));
        }


        public bool IsEnabled { get; set; }

        private readonly StreamWriter _streamWriter;

        public void Log(string message, string prefixTag = null)
        {
            if (IsEnabled)
            {
                _streamWriter.WriteLineAsync($"[{GetCurrentTime()}] <INFO> [{prefixTag}] {message}");
                _streamWriter.Flush();
            }
        }

        public void LogError(string message, string prefixTag = null)
        {
            if (IsEnabled)
            {
                _streamWriter.WriteLineAsync($"[{GetCurrentTime()}] <ERROR> [{prefixTag}] {message}");
                _streamWriter.Flush();
            }
        }

        public void LogWarning(string message, string prefixTag = null)
        {
             if (IsEnabled)
             {
                 _streamWriter.WriteLineAsync($"[{GetCurrentTime()}] <WARNING> [{prefixTag}] {message}");
                 _streamWriter.Flush();
             }
        }

        public void LogAssertion(string message, string prefixTag = null)
        {
            if (IsEnabled)
            {
                _streamWriter.WriteLineAsync($"[{GetCurrentTime()}] <ASSERT> [{prefixTag}] {message}");
                _streamWriter.Flush();
            }
        }

        public void Dispose()
        {
            _streamWriter?.Dispose();
        }


        private string GetCurrentTime()
        {
            DateTime dateTimeNow = DateTime.Now;
            return $"{dateTimeNow.ToShortDateString()} {dateTimeNow.ToLongTimeString()}";
        }
    }
}