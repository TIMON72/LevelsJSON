using LevelsJSON.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LevelsJSON_Tests
{
    [TestClass]
    public class JsonTests
    {
        /// <summary>
        /// Тестирование конструкторов
        /// </summary>
        [TestMethod]
        public void JsonTest()
        {
            // Тест 1 (строка JSON)
            var actual = new Json("{\"key\": \"value\"}");
            var expected = "{\"key\": \"value\"}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 1");
            // Тест 2 (строка JSON с опечаткой)
            actual = new Json("{\"key: \"value\"}");
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 2");
            // Тест 3 (объект JSON)
            actual = new Json(actual);
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 3");
            // Тест 4 (объект int)
            actual = new Json(1);
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 4");
            // Тест 5 (объект bool)
            actual = new Json(true);
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 5");
            // Тест 6 (объект null)
            actual = new Json(null);
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 6");
            // Тест 7 (объект double)
            actual = new Json(1.0);
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 7");
        }
        /// <summary>
        /// Тестирование получения количества уровней глубины вложенности
        /// </summary>
        [TestMethod]
        public void GetLevelsTest()
        {
            // Тест 1
            Json json = new Json("{\"key\": \"value\"}");
            var actual = json.GetLevels();
            var expected = "{\"levels\": \"1\"}";
            Assert.AreEqual(expected, actual, "Ошибка в тесте 1");
            // Тест 2
            json = new Json("{\"identity\": {\"name\": \"Test\", \"translations\": [{\"order\": 1, \"language\": \"ru\", \"value\": \"Тест\"}]}}");
            actual = json.GetLevels();
            expected = "{\"levels\": \"4\"}";
            Assert.AreEqual(expected, actual, "Ошибка в тесте 2");
            // Тест 3
            json = new Json("{\"test1\": true, \"test2\": [null, 1]}");
            actual = json.GetLevels();
            expected = "{\"levels\": \"2\"}";
            Assert.AreEqual(expected, actual, "Ошибка в тесте 3");
            // Тест 4
            json = new Json("{\"title\":\"Conference\",\"participants\":[{},{}]}");
            actual = json.GetLevels();
            expected = "{\"levels\": \"3\"}";
            Assert.AreEqual(expected, actual, "Ошибка в тесте 4");
        }
    }
}
