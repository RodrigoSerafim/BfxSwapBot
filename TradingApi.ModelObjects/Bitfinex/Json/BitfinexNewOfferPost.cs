using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingApi.ModelObjects.Bitfinex.Json
{
   public class BitfinexNewOfferPost : BitfinexPostBase
   {
      [JsonProperty("currency")]
      public string Currency { get; set; }
      
      [JsonIgnore]
      public decimal Amount { get; set; }

      //the bitfinex api is stupid and takes strings instead of the actual field types
      [JsonProperty("amount")]
      public string Amount_string
      {
          get
          {
              return Amount.ToString(System.Globalization.CultureInfo.InvariantCulture);
          }
          set
          {
              Amount = Decimal.Parse(value);
          }
      }


      [JsonIgnore]
      public decimal Rate { get; set; }

      //the bitfinex api is stupid and takes strings instead of the actual field types
      [JsonProperty("rate")]
      public string Rate_string
      {
          get
          {
              return Rate.ToString(System.Globalization.CultureInfo.InvariantCulture);
          }
          set
          {
              Rate = Decimal.Parse(value);
          }
      }

      [JsonIgnore]
      public decimal DailyRate
      {
         get
         {
            return Rate / 365;
         }
      }
      [JsonProperty("period")]
      public int Period { get; set; }
      
      [JsonProperty("direction")]
      public string Direction { get; set; }
   }

}
