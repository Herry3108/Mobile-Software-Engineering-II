using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BoardGamerApp.Database;
public static class FirebaseParser
{
    public static IEnumerable<T> ParseCollection<T>(string json) where T : class
    {
        if (string.IsNullOrEmpty(json) || json == "null")
        {
            return new List<T>();
        }

        var jsonToken = JToken.Parse(json);

        if (jsonToken is JArray)
        {
            var result = JsonConvert.DeserializeObject<List<T>>(json);
            var cleanedResult = new List<T>();

            if (result is not null)
            {
                foreach (var item in result)
                {
                    if (item is not null)
                    {
                        cleanedResult.Add(item);
                    }
                }
            }

            return cleanedResult;
        }

        if (jsonToken is JObject)
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);

            if (dict != null)
            {
                return dict.Values;
            }
        }

        return new List<T>();
    }

    public static Dictionary<string, TValue> ParseValueDictionary<TValue>(string json)
    {
        if (string.IsNullOrEmpty(json) || json == "null")
        {
            return new Dictionary<string, TValue>();
        }

        try
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
            return result ?? new Dictionary<string, TValue>();
        }
        catch (Exception ex)
        {
            return new Dictionary<string, TValue>();
        }
    }

    public static T? ParseSingle<T>(string json) where T : class
    {
        if (string.IsNullOrEmpty(json) || json == "null")
        {
            return null;
        }

        var settings = new JsonSerializerSettings
        {
            Error = (sender, args) =>
            {
                args.ErrorContext.Handled = true;
            },
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        var result = JsonConvert.DeserializeObject<T>(json, settings);
        return result;
    }
}