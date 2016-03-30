using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BfxSwapBot
{
	[Serializable()]
	public class LendCurrencyXml
	{
		[XmlAttribute("currency")]
		public string Currency { get; set; }
		[XmlAttribute("period")]
		public int Period { get; set; }
		[XmlAttribute("minimum")]
		public decimal Minimum { get; set; }
	}


	[Serializable()]
	[XmlRoot("config")]
	public class ConfigXml
	{
		[XmlElement("key")]
		public string Key { get; set; }
		[XmlElement("secret")]
		public string Secret { get; set; }
		[XmlElement("lendCurrency")]
		public List<LendCurrencyXml> LendCurrencies { get; set; }


		public static ConfigXml Load(string filepath){
			XmlSerializer serializer = new XmlSerializer(typeof(ConfigXml));

			StreamReader reader = new StreamReader(filepath);
			var config = (ConfigXml)serializer.Deserialize(reader);
			reader.Close();

			return config;
		}
	}
}

