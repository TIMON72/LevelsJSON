using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace LevelsJSON.Models
{
    /// <summary>
    /// Класс JSON
    /// </summary>
    public class Json
    {
        /// <summary>
        /// JSON в формате строки
        /// </summary>
        public string String { get; set; }
        /// <summary>
        /// Корректность JSON
        /// </summary>
        public bool IsInvalid { get; set; } = false;
        /// <summary>
        /// Конструктор, принимающий любой тип данных, который можно привести к string и десерилизовать в JSON.
        /// Формат входных данных должен соответствовать JSON.
        /// <exception cref="JsonException">Происходит, если входные данные не десериализуются в JSON</exception>
        /// </summary>
        /// <param name="input">Динамическая входная переменная с JSON-данными</param>
        public Json(dynamic input)
        {
            try
            {
                // Проверка на null и примитивы
                if (input is null || input.GetType().IsPrimitive)
                    throw new JsonException();
                // Настройка декодера (использование кириллицы)
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                // Десериализация входных данных в JSON-формат
                JsonElement json = JsonSerializer.Deserialize<JsonElement>(input.ToString(), options);
                // Сериализация JSON в JSON-строку
                String = json.ToString();//JsonSerializer.Serialize(json); // (без пробелов)
            }
            catch (JsonException)
            {
                String = "{\"error\": \"Invalid JSON\"}";
                IsInvalid = true;
            }
        }
        /// <summary>
        /// Получить количество уровней глубины вложенности
        /// </summary>
        /// <returns>JSON-строку с информацией о количестве уровней</returns>
        public string GetLevels()
        {
            int levelsCounter = 0;
            int levelsNumber = 1;
            for (int i = 0; i < String.Length; i++)
                if ((String[i] == '{' || String[i] == '['))
                {
                    levelsCounter++;
                    if (levelsCounter > levelsNumber)
                        levelsNumber = levelsCounter;
                }
                else if (String[i] == '}' || String[i] == ']')
                    levelsCounter--;
            return "{\"levels\": \"" + levelsNumber + "\"}";
        }
    }
}
