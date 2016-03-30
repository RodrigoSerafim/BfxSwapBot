using System;

namespace TradingApi.Bitfinex
{
    [Serializable]
    public class BifinexApiException : Exception
    {
        public BifinexApiException() { }
        public BifinexApiException(string message) : base(message) { }
        public BifinexApiException(string message, Exception inner) : base(message, inner) { }
        protected BifinexApiException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

}
