using LevelsJSON.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LevelsJSON_Tests
{
    /// <summary>
    /// Тестовый класс <see cref="IJsonRepository"/>
    /// </summary>
    [TestClass]
    public class IJsonRepositoryTests
    {
        /// <summary>
        /// Тестирование <see cref="IJsonRepository.GetJson(int)"/>
        /// </summary>
        [TestMethod]
        public void GetJsonTest()
        {
            Json[] jsons =
{
                new Json("{\"key\": \"mock object\"}"),
            };
            Mock<IJsonRepository> mock = new Mock<IJsonRepository>();
            mock.Setup(j => j.GetJson(0)).Returns(jsons[0]);
            mock.Setup(j => j.GetJson(It.Is<int>(i => i < 0 && i > jsons.Length))).Returns(new Json(null));
            IJsonRepository iJsonRepo = mock.Object; // используем подставной объект
            // Тест 1
            var actual = iJsonRepo.GetJson(0);
            var expected = "{\"key\": \"mock object\"}";
            Assert.AreEqual(expected, actual.String, "Ошибка в тесте 1");
            // Тест 2
            actual = iJsonRepo.GetJson(1);
            expected = null;
            Assert.AreEqual(expected, actual, "Ошибка в тесте 2");
        }
    }
}
