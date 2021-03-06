﻿using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
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
        /// Парсинг
        /// </summary>
        /// <param name="input">Строковое представление JSON-данных</param>
        /// <returns>Распарсенное строковое представление JSON-данных</returns>
        public static string Parse(string input)
        {
            Regex regex;
            MatchCollection matches;
            string pattern;
            // Парсинг целых чисел
            pattern = "([+-]?\\d+[.]?\\d*)|(\"[^\"\\\\]*(?:\\\\.[^\"\\\\]*)*\")"; // найти все целые числа +/- int double и выражения в "..."
            regex = new Regex(pattern);
            matches = regex.Matches(input);
            foreach (Match m in matches)
            {
                // Число не в "..."?
                if (!m.Value.Contains('"'))
                {
                    double numberDouble = double.Parse(m.Value.Replace('.',','));
                    int numberInt = int.Parse(numberDouble.ToString());
                    input = input.Replace(m.Value, numberInt.ToString());
                }
            }
            return input;
        }
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
                // Парсинг
                input = Json.Parse(input.ToString());
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
            int levelsCounter = 1;
            int levelsNumber = 1;
            bool quot = false;
            for (int i = 1; i < String.Length; i++)
                if (quot)
                {
                    if (String[i] == '"' && String[i - 1] != '\\')
                        quot = false;
                    else
                        continue;
                }
                else if (String[i] == '"' && String[i - 1] != '\\')
                {
                    quot = true;
                    continue;
                }
                else if ((String[i] == '{' || String[i] == '['))
                {
                    levelsCounter++;
                    if (levelsCounter > levelsNumber)
                        levelsNumber = levelsCounter;
                }
                else if (String[i] == '}' || String[i] == ']')
                    levelsCounter--;
            return "{\"levels\": " + levelsNumber + "}";
        }
        /// <summary>
        /// Перегрузка метода <see cref="object.GetHashCode"/>
        /// </summary>
        /// <returns>Возвращает хэш-код, формируемый из свойств класса</returns>
        public override int GetHashCode()
        {
            return String.GetHashCode() ^ IsInvalid.GetHashCode();
        }
        /// <summary>
        /// Перегрузка метода <see cref="object.Equals(object)"/>
        /// </summary>
        /// <param name="obj">Объект, с которым сравнивают</param>
        /// <returns>Возвращает true или false</returns>
        public override bool Equals(object obj)
        {
            if (String == ((Json)obj).String && IsInvalid == ((Json)obj).IsInvalid)
                return true;
            return false;
        }
        /// <summary>
        /// Перегрузка метода <see cref="object.ToString()"/>
        /// </summary>
        /// <returns>Возвращает строковое представление объекта <see cref="Json"/></returns>
        public override string ToString()
        {
            return String;
        }
    }
}
