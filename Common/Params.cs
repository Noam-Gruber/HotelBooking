using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Common
{
    public static class Params
    {
        private static JObject? _config;
        private static readonly string baseProject = Directory.GetParent(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName).FullName;
        private static readonly string JsonFilePath = Path.Combine(baseProject, "Common\\Configuration.json");

        /// <summary>
        /// Static constructor to load the configuration.
        /// </summary>
        static Params() => LoadConfig();

        /// <summary>
        /// Loads the configuration from the JSON file.
        /// </summary>
        private static void LoadConfig()
        {
            if (File.Exists(JsonFilePath))
            {
                try
                {
                    var jsonString = File.ReadAllText(JsonFilePath);
                    _config = JObject.Parse(jsonString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading Params.json: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Params.json file not found.");
            }
        }

        /// <summary>
        /// Gets the server address from the configuration.
        /// </summary>
        /// <returns>The server address as a string.</returns>
        public static string GetServerAddress()
        {
            return _config?["serverAddress"]?.Value<string>() ?? string.Empty;
        }

        /// <summary>
        /// Gets the port number from the configuration.
        /// </summary>
        /// <returns>The port number as an integer.</returns>
        public static int GetPort()
        {
            return (int)(_config?["port"]?.Value<long>() ?? 0);
        }
    }
}