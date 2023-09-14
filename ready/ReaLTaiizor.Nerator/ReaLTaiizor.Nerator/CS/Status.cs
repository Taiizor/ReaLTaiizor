using System;

namespace ReaLTaiizor.Nerator.CS
{
    public static class Status
    {
        private const string _DefaultStatus = "The application continues to run smoothly.";
        public static string DefaultStatus => _DefaultStatus;

        private static string _Message = _DefaultStatus;
        public static string Message
        {
            get => _Message;
            set
            {
                _Message = value;
                ChangedStatus = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
        }

        public static long ChangedStatus { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}