# LevelsJSON
## Функционал
Архитектура Web API: `[сервер]/[контроллер]/[метод]`
Существующие запросы к серверу:
- `[сервер]`
    - `/Json`
        - `/GetLevels`
   `        POST:` JSON-строка
## Как пользоваться
### Запуск программы из под любой ОС
1. Установить/запустить [Postman](https://www.postman.com/) или аналог с возможностью POST-запросов
2. Открыть командную строку в корне проекта
3. Выполнить развертывание приложения консольной командой: `dotnet publish LevelsJSON.csproj`
4. Перейти в каталог с полученными файлами: `.\bin\Debug\netcoreapp3.1\publish\`
5. Открыть командную строку и выполнить команду: `dotnet LevelsJSON.dll`
6. Открыть браузер и проверить доступность сервера http://localhost:5000/index.html
7. Отправить запрос требуемому методу через [Postman](https://www.postman.com/) или аналог
### Тестирование программы из под любой ОС
1. Открыть командную строку в `.\LevelsJSON_Tests\`
2. Выполнить развертывание приложения консольной командой: `dotnet publish LevelsJSON_Tests.csproj`
3. Перейти в каталог с полученными файлами: `.\LevelsJSON_Tests\bin\Debug\netcoreapp3.1\publish\`
4. Открыть командную строку и выполнить команду: `dotnet vstest LevelsJSON_Tests.dll`
### Запуск и тестирование программы в Visual Studio
1. Запустить решение `LevelsJSON.sln`
2. Запуск проекта: запуск решения `LevelsJSON.csproj` и пункты 1, 7 раздела [Запуск из под любой ОС](#Запуск-из-под-любой-ОС)
* Просмотр документации: открыть и запусть решение `LevelsJSON.csproj` - запуск браузера - перейти по ссылке *Документация*
* Запуск тестов: открыть и запустить решение `LevelsJSON_Tests\LevelsJSON_Tests.csproj` - *Обозреватель тестов* - *Запуск всех тестов*
## Тестовое задание для Backend-разработчика на C#
Написать приложение, которое принимает на вход данные в формате JSON и проверяет их на
корректность. В случае, если передан корректный JSON, возвращает количество уровней в нем
(максимальную глубину вложенных объектов и массивов).
Реализовать в виде приложения с HTTP API.

Примеры:
- Вход: `{"key": "value"}`
    - Ответ: `{"levels": 1}`
- Вход: `{"identity": {"name": "Test", "translations": [{"order": 1, "language": "ru", "value": "Тест"}]}}`
    - Ответ: `{"levels" : 4}`
- Вход: `{"incorrect: "value"}`
    - Ответ: `{"error": "Invalid JSON"}`
