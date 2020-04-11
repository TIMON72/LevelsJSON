using LevelsJSON.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace LevelsJSON_Tests
{
    /// <summary>
    /// Тестовый класс <see cref="Json"/>
    /// </summary>
    [TestClass]
    public class JsonTests
    {     
        /// <summary>
        /// Фикстура инициализации тестов
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Инициализация теста");
        }
        /// <summary>
        /// Фикстура очистки тестов
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            Debug.WriteLine("Очистка теста");
        }
        /// <summary>
        /// Тестирование <see cref="Json(dynamic)"/>
        /// </summary>
        [DataTestMethod]
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
            // Тест 8 (объект JSON c { [ " внутри значения)
            actual = new Json("{\"key\": \"\\\"value1\\\" \\\"{[value2, value3]}\\\"\"}");
            expected = "{\"key\": \"\\\"value1\\\" \\\"{[value2, value3]}\\\"\"}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 8");
            // Тест 9
            actual = new Json("{\"key\": +2}");
            expected = "{\"key\": 2}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 9");
            // Тест 10
            actual = new Json("{\"key1\": -2.0, \"key2\": \"2\", \"key3\": 2.0, \"key4\": -2}");
            expected = "{\"key1\": -2, \"key2\": \"2\", \"key3\": 2, \"key4\": -2}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 10");
        }
        /// <summary>
        /// Тестирование <see cref="Json.GetLevels"/>
        /// </summary>
        [TestMethod]
        public void GetLevelsTest()
        {
            // Тест 1
            Json json = new Json("{\"key\": \"value\"}");
            var actual = json.GetLevels();
            var expected = "{\"levels\": 1}";
            Assert.AreEqual(expected, actual, "Ошибка в тесте 1");
            // Тест 2
            json = new Json("{\"identity\": {\"name\": \"Test\", \"translations\": [{\"order\": 1, \"language\": \"ru\", \"value\": \"Тест\"}]}}");
            actual = json.GetLevels();
            expected = "{\"levels\": 4}";
            Assert.AreEqual(expected, actual, "Ошибка в тесте 2");
            // Тест 3
            json = new Json("{\"test1\": true, \"test2\": [null, 1]}");
            actual = json.GetLevels();
            expected = "{\"levels\": 2}";
            Assert.AreEqual(expected, actual, "Ошибка в тесте 3");
            // Тест 4
            json = new Json("{\"title\":\"Conference\",\"participants\":[{},{}]}");
            actual = json.GetLevels();
            expected = "{\"levels\": 3}";
            Assert.AreEqual(expected, actual, "Ошибка в тесте 4");
            // Тест 5
            json = new Json("{\"key\": \"\\\"value1\\\" \\\"{[value2, value3]}\\\"\"}");
            actual = json.GetLevels();
            expected = "{\"levels\": 1}";
            Assert.AreEqual(expected, actual, "Ошибка в тесте 5");
        }
        /// <summary>
        /// Тестирование <see cref="Json.GetHashCode(object)"/>
        /// </summary>
        /// <remarks>Параметризированный тест</remarks>
        /// <param name="obj1">Объект, который сравнивают</param>
        /// <param name="obj2">Объект, с которым сравнивают</param>
        /// <param name="expected">Ожидаемый результат (true или false)</param>
        [DataRow("{\"key1\": \"value1\"}", "{\"key1\": \"value1\"}", true)]
        [DataRow("{\"key1\": \"value1\"}", "{\"key2\": \"value2\"}", false)]
        [DataRow("{\"key1\": \"true\"}", "{\"key1\": true }", false)]
        [DataTestMethod]
        public void GetHashCodeTest(dynamic obj1, dynamic obj2, bool expected)
        {
            Json json1 = new Json(obj1);
            Json json2 = new Json(obj2);
            bool actual = json1.GetHashCode() == json2.GetHashCode();
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Тестирование <see cref="Json.Equals(object)"/>
        /// </summary>
        /// <remarks>Параметризированный тест</remarks>
        /// <param name="obj1">Объект, который сравнивают</param>
        /// <param name="obj2">Объект, с которым сравнивают</param>
        /// <param name="expected">Ожидаемый результат (true или false)</param>
        [DataRow("{\"key1\": \"value1\"}", "{\"key1\": \"value1\"}", true)]
        [DataRow("{\"key1\": \"value1\"}", "{\"key2\": \"value2\"}", false)]
        [DataRow("{\"key1\": \"true\"}", "{\"key1\": true }", false)]
        [DataTestMethod]
        public void EqualsTest(dynamic obj1, dynamic obj2, bool expected)
        {
            Json json1 = new Json(obj1);
            Json json2 = new Json(obj2);
            bool actual = json1.Equals(json2);
            Assert.AreEqual(expected, actual);
        }
    }
}
