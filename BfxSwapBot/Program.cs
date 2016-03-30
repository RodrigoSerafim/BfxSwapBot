using System;
using TradingApi.Bitfinex;
using TradingApi.ModelObjects;
using System.Linq;

namespace BfxSwapBot
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			SwapBot2();
		}

		public static void SwapBot2()
		{
			var config = ConfigXml.Load ("config.xml");

			BitfinexApi api = new BitfinexApi(config.Secret, config.Key);
			//BitfinexApi api = new BitfinexApi("", "");

			Console.WriteLine (DateTime.Now + " Conecting to bitfinex");


			//----------------------------------------------------------------------
			// GET ACCOUNT DATA
			//----------------------------------------------------------------------
			var offers = api.GetActiveOffers ();
			var balances = api.GetBalances ();

			//----------------------------------------------------------------------
			// ITERATE LENDING CURRENCIES
			//----------------------------------------------------------------------
			foreach (var lendCurrency in config.LendCurrencies) {

				//----------------------------------------------------------------------
				// CALCULATE THE OPTIMAL RATE
				//----------------------------------------------------------------------

				var lends = api.GetLends (lendCurrency.Currency, 1);

				var lendBook = api.GetLendbook (lendCurrency.Currency, 100, 0);


				//find out the average lending period
				double averageRenewalPeriod = 0.0;
				double sumLend = 0;
				foreach (var swap in lendBook.Asks) {
					sumLend += swap.Amount;
					averageRenewalPeriod += swap.Period * swap.Amount;
				}
				averageRenewalPeriod /= sumLend;


				//Calculate the expected average speed at wich contracts are taken (demand)
				//TODO: could be nice to take into account spike probability in demand and maket direction bias (raising BTC market leads to higher rates in USD)
				// this will be the rate of contracts per day
				double contractSpeed = lends [0].AmountLent / averageRenewalPeriod;


				//calculate the best rate taking into account the current lend offers                
				sumLend = 0;
				double bestRate = 0;
				double bestMoney = 0;

				foreach (var swap in lendBook.Asks) {
					sumLend += swap.Amount;

					double swapRate = Math.Round (swap.Rate / 365.0, 4);

					//expected value for days waiting for buyers to fill this swap rate
					double daysWaiting = (sumLend / contractSpeed);
					//quadratic probability function, aproximates the pareto probability distribution
					daysWaiting *= (daysWaiting + 1.0); 
					//expected interest money taking into account the oportunity cost of waiting for a better rate
					double money = (swapRate / 100.0) * ((double)lendCurrency.Period - daysWaiting);

					if (money > bestMoney) {
						bestRate = swapRate;
						bestMoney = money;
					}

					//System.Diagnostics.Debug.WriteLine("rate:" + swapRate + " -> " + money * 1000 + " $" + " -> " + daysWaiting + " wait");
				}

				//put the offer just below the best price
				bestRate -= 0.0001;
				Console.WriteLine ("Best rate = " + bestRate);


				//----------------------------------------------------------------------
				// LOOK FOR SWAPPABLE BALANCE
				//----------------------------------------------------------------------
				//TODO: Do we need to wait a bit for the cancels to take effect?


				var deposit = balances.First (x => x.Type == "deposit" && x.Currency.ToLowerInvariant() == lendCurrency.Currency).Available;
				//deposit = Decimal.Round(deposit, 2);


				//----------------------------------------------------------------------
				// CANCEL OVERSTRETCHED LEND POSITIONS
				//----------------------------------------------------------------------

				foreach (var offer in offers.Where(x => x.Currency.ToLowerInvariant() == lendCurrency.Currency))
				//if the new best rate is lower then cancel this offer (we only update down)
				if (bestRate < (double)offer.Rate / 365.0) {                        
						api.CancelOffer (offer.Id);
						//update the deposit to avoid another API acess
						deposit += offer.RemainingAmount;

						string msg = "Lend canceled. " + offer.RemainingAmount + " @" + (double)offer.Rate / 365.0;
						Console.WriteLine (msg);
					}



				//----------------------------------------------------------------------
				// CREATE NEW LEND
				//----------------------------------------------------------------------
				if (deposit >= lendCurrency.Minimum) {
					api.SendNewOffer (lendCurrency.Currency, deposit, (decimal)(bestRate * 365.0), lendCurrency.Period, "lend");

					string msg = "New lend created. " + deposit + " @" + bestRate + " for " + lendCurrency.Period + " days";
					Console.WriteLine (msg);
				}
			}


			Console.WriteLine (DateTime.Now + " Swap update finished");

		}
	}
}
