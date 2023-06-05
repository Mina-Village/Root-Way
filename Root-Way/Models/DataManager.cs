using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Root_Way.Models.Elements;
using Root_Way.UtilityCollection;

namespace Root_Way.Models
{
    public static class DataManager
    {
        internal static List<SchoolYear> SchoolYears { get; private set; } = new();
        
        private static string FilePath => Path.Combine(Utilities.FilesParentPath, "SchoolYears.json");

        internal static void LoadData()
        {
            if (!File.Exists(FilePath))
                return;

            string content = File.ReadAllText(FilePath);
            var deserializedList = JsonSerializer.Deserialize<List<SchoolYear>>(content);
            if(deserializedList is not null)
                SchoolYears = deserializedList;
        }

        internal static void SaveData()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(SchoolYears, options);
            File.WriteAllText(FilePath, jsonString);
        }
    }
}