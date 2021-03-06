﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using Newtonsoft.Json;

namespace TradingApi.ModelObjects.Bitfinex.Json.LendbookTypes
{

   public class Ask
   {

      [JsonProperty("rate")]
      public double Rate { get; set; }

      [JsonProperty("amount")]
      public double Amount { get; set; }

      [JsonProperty("period")]
      public int Period { get; set; }

      [JsonProperty("timestamp")]
      public double Timestamp { get; set; }

      [JsonProperty("frr")]
      public string Frr { get; set; }

      public override string ToString()
      {
         var str = string.Format("Ask - Rate: {0}, Amount: {1}, Period: {2}, Timestamp: {3}, Frr: {4}",
            Rate, Amount, Period, Timestamp, Frr);
         return str;
      }
   }

}
