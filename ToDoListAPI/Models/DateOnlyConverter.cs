﻿using System.Text.Json.Serialization;
using System.Text.Json;

namespace ToDoListAPI.Models
{
    public class DateOnlyConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateTime.Parse(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString("MMMM dd, yyyy hh:mm tt"));
    }

}
