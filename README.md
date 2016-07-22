# BfxSwapBot
Bitfinex Swap Bot

This is a .Net Mono console application designed to monitor your deposit account in Bitfinex and automatically place and update Lending orders for USD or BTC.
The recomended way to use it is to setup a windows task or linux cron job and call it every hour.

This project clones part of TradingAPI by Chris O'Brien.

https://github.com/workingobrien/TradingApi.Bitfinex.git

The project has not been touched for over a year, so for simplicity sake I chose to copy it instead of creating and managing a submodule. This may change in the future, especially if Bitfinex happens to change their API.

Configuration file is very simple. Here is an example:
```xml
<?xml version="1.0" encoding="utf-8"?>
<config>
	<key>yourApiKeyGoesHere</key>
	<secret>yourApiSecretGoesHere</secret>
	<lendCurrency currency="usd" period="2" minimum="50" maximum="-1"/>
</config>
```

Reference:
* **key**: Api key you get from bitfinex
* **secret**: The secret code associated with the key id
* **lendCurrency**: Each of these lines let you specify the parameters for lending a specific currency
    * **currency**: The currency you wish to lend
    * **period**: The lending period in days
    * **minimum**: The minimum ammount to create a new lending offer
    * **maximum**: The maximum ammount you wich to have lended. Use -1 for unlimited.