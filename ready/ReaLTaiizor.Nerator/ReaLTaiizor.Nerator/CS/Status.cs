using System;

namespace ReaLTaiizor.Nerator.CS
{
    public static class Status
    {
        private const string _DefaultStatus = "The application continues to run smoothly.";
        public static string DefaultStatus => _DefaultStatus;

        public static string Message
        {
            get;
            set
            {
                field = value;
                ChangedStatus = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
        } = _DefaultStatus;

        public static long ChangedStatus { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}