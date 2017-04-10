﻿#if !NOJSONNET
using LushCoin.OpenAsset;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using LushCoin;

namespace LushCoin.JsonConverters
{
#if !NOJSONNET
	public
#else
	internal
#endif
	class AssetIdJsonConverter : JsonConverter
    {
        public AssetIdJsonConverter(Network network)
        {
            if(network == null)
                throw new ArgumentNullException("network");
            Network = network;
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(AssetId).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.TokenType == JsonToken.Null)
                return null;

            try
            {
                var value = reader.Value.ToString();
                return new BitcoinAssetId(value, Network).AssetId;
            }
            catch(FormatException)
            {
                throw new JsonObjectException("Invalid BitcoinAssetId ", reader);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var assetId = value as AssetId;
            if(assetId != null)
            {
                writer.WriteValue(assetId.ToString(Network));
            }
        }

        public Network Network
        {
            get;
            set;
        }
    }
}
#endif