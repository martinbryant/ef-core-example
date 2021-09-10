using System;
using CSharpFunctionalExtensions;

namespace ef_core_example.Models
{
    public class Envelope
    {
        public object Data { get; }

        public string ErrorCode { get; }

        public string ErrorMessage { get; }

        public DateTime TimeGenerated { get; }

        private Envelope()
        {
            TimeGenerated = DateTime.Now;
        }

        private Envelope(object result)
        {
            TimeGenerated   = DateTime.Now;
            Data            = result;
        }

        private Envelope(Error error)
        {
            TimeGenerated   = DateTime.Now;
            ErrorCode       = error.Code;
            ErrorMessage    = error.Message;
        }

        internal static Envelope Error(Error error, string fieldName) => new Envelope(error);

        internal static Envelope Ok() => new Envelope();

        internal static Envelope Ok<T>(T result) => new Envelope(result);

        internal static Envelope Error(Error error) => new Envelope(error);
    }
}