using LevelsJSON.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LevelsJSON_Tests
{
    [TestClass]
    public class JsonTests
    {
        /// <summary>
        /// ������������ �������������
        /// </summary>
        [TestMethod]
        public void JsonTest()
        {
            // ���� 1 (������ JSON)
            var actual = new Json("{\"key\": \"value\"}");
            var expected = "{\"key\": \"value\"}";
            Assert.AreEqual(expected, actual.String, "������ � ����� 1");
            // ���� 2 (������ JSON � ���������)
            actual = new Json("{\"key: \"value\"}");
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "������ � ����� 2");
            // ���� 3 (������ JSON)
            actual = new Json(actual);
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "������ � ����� 3");
            // ���� 4 (������ int)
            actual = new Json(1);
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "������ � ����� 4");
            // ���� 5 (������ bool)
            actual = new Json(true);
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "������ � ����� 5");
            // ���� 6 (������ null)
            actual = new Json(null);
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "������ � ����� 6");
            // ���� 7 (������ double)
            actual = new Json(1.0);
            expected = "{\"error\": \"Invalid JSON\"}";
            Assert.AreEqual(expected, actual.String, "������ � ����� 7");
        }
        /// <summary>
        /// ������������ ��������� ���������� ������� ������� �����������
        /// </summary>
        [TestMethod]
        public void GetLevelsTest()
        {
            // ���� 1
            Json json = new Json("{\"key\": \"value\"}");
            var actual = json.GetLevels();
            var expected = "{\"levels\": \"1\"}";
            Assert.AreEqual(expected, actual, "������ � ����� 1");
            // ���� 2
            json = new Json("{\"identity\": {\"name\": \"Test\", \"translations\": [{\"order\": 1, \"language\": \"ru\", \"value\": \"����\"}]}}");
            actual = json.GetLevels();
            expected = "{\"levels\": \"4\"}";
            Assert.AreEqual(expected, actual, "������ � ����� 2");
            // ���� 3
            json = new Json("{\"test1\": true, \"test2\": [null, 1]}");
            actual = json.GetLevels();
            expected = "{\"levels\": \"2\"}";
            Assert.AreEqual(expected, actual, "������ � ����� 3");
            // ���� 4
            json = new Json("{\"title\":\"Conference\",\"participants\":[{},{}]}");
            actual = json.GetLevels();
            expected = "{\"levels\": \"3\"}";
            Assert.AreEqual(expected, actual, "������ � ����� 4");
        }
    }
}
