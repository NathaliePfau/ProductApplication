using System;

namespace ProductApplication.Domain.AppFlowControl
{
    [Serializable]
    public class ServicesException : Exception
    {
        public ServicesException(string message) : base(message)
        {
        }

        public ServicesException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
