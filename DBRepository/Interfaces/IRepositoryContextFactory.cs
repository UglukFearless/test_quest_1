/****************************************************************************
*  Файл: IRepositoryContextFactory.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение интерфейса IRepositoryContextFactory
****************************************************************************/

namespace DBRepository.Interfaces
{
    /// <summary>
    /// Интерфейс фабрики создания контекста базы данных
    /// </summary>
    public interface IRepositoryContextFactory
    {
        /// <summary>
        /// Метод создающий контекст базы данных
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных</param>
        /// <returns>Контекст базы данных</returns>
        RepositoryContext CreateDbContext(string connectionString);
    }
}
