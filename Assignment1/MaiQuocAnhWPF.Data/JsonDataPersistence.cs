using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MaiQuocAnhWPF.Data
{
    public class JsonDataPersistence<T> : IDataPersistence<T> where T : class
    {
        private readonly string _dataDirectory;

        public JsonDataPersistence()
        {
            _dataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SalesManagementSystem");
            if (!Directory.Exists(_dataDirectory))
            {
                Directory.CreateDirectory(_dataDirectory);
            }
        }

        public void SaveData(IEnumerable<T> data, string fileName)
        {
            try
            {
                var filePath = Path.Combine(_dataDirectory, fileName);
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                var jsonString = JsonSerializer.Serialize(data, jsonOptions);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save data to {fileName}: {ex.Message}");
            }
        }

        public IEnumerable<T> LoadData(string fileName)
        {
            try
            {
                var filePath = Path.Combine(_dataDirectory, fileName);
                if (!File.Exists(filePath))
                {
                    return new List<T>();
                }

                var jsonString = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    return new List<T>();
                }

                return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load data from {fileName}: {ex.Message}");
            }
        }
    }
}