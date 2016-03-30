using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApi.ModelObjects.Utility
{
   public static class Common
   {
      public static long GetNonce()
      {
         long unixTimeStamp;
         DateTime currentTime = DateTime.Now;
         DateTime dt = currentTime.ToUniversalTime();
         DateTime unixEpoch = new DateTime(1970, 1, 1);
         unixTimeStamp = (long)(dt.Subtract(unixEpoch).TotalSeconds*100.0);
         return unixTimeStamp;
      }

      public static double GetTimeStamp(DateTime dt)
      {
         var unixEpoch = new DateTime(1970, 1, 1);
         return dt.Subtract(unixEpoch).TotalSeconds;
      }


      public static DateTime ParseUnixTimeStamp(double timestamp)
      {
          DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
          d = d.AddSeconds(timestamp).ToLocalTime();
          return d;
      }

   }
}
