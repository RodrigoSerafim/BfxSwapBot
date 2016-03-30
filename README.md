# BfxSwapBot
Bitfinex Swap Bot

This is a .Net Mono console application designed to monitor your deposit account in Bitfinex and automatically place and update Lending orders for USD or BTC.
The recomended way to use it is to setup a windows task or linux cron job and call it every hour.

This project clones part of TradingAPI by Chris O'Brien.
https://github.com/workingobrien/TradingApi.Bitfinex.git
The project has not been touched for over a year, so for simplicity sake I chose to copy it instead of creating and managing a submodule. This may change in the future, especially if Bitfinex happens to change their API.