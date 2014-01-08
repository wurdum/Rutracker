using System;

namespace Rutracker.Anime.Exceptions
{
    public class ScannerException : Exception
    {
        public ScannerException(string message, string title) : base(message) {
            Title = title;
        }

        protected string Title { get; set; }
    }
}