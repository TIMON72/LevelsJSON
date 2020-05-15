namespace LevelsJSON.Models
{
    /// <summary>
    /// Интерфейс IJsonRepository
    /// </summary>
    public interface IJsonRepository
    {
        /// <summary>
        /// Получение элемента по индексу
        /// </summary>
        /// <param name="index">индекс элемента</param>
        /// <returns></returns>
        public Json GetJson(int index);
    }
}
