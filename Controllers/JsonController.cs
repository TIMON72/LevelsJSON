using LevelsJSON.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LevelsJSON.Controllers
{
    /// <summary>
    /// Контроллер класса Json
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class JsonController : ControllerBase
    {
        /// <summary>
        /// Контроллер получения количества уровня глубины вложенности
        /// </summary>
        /// <param name="input">Динамическая входная переменная с JSON-данными</param>
        /// <returns>JSON-строку с информацией о количестве уровней</returns>
        [Route("GetLevels")]
        [HttpPost]
        public async Task<string> GetLevels([FromBody]dynamic input)
        {
            return await Task.Run(() =>
            {
                Json json = new Json(input);
                if (json.IsInvalid)
                    return json.String;
                return json.GetLevels();
            });
        }
    }
}
