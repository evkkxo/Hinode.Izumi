﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hinode.Izumi.Framework.Web
{
    public class TimeSpanSerializer : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            TimeSpan.Parse(reader.GetString() ?? string.Empty);

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}
