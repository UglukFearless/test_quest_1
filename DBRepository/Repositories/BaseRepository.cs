/****************************************************************************
*  Файл: BaseRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение абстрактного класса BaseRepository
****************************************************************************/

using DBRepository.Interfaces;

namespace DBRepository.Repositories
{

    /// <summary>
    /// Базовый класс репозитория для доступа к данным
    /// </summary>
    public abstract class BaseRepository
    {
        // Строка подключения к БД
        protected string ConnectionString { get; }

        // Фабрика для создания контекста БД
        protected IRepositoryContextFactory ContextFactory { get; }

        public BaseRepository(string connectionString, IRepositoryContextFactory contextFactory)
        {
            ConnectionString = connectionString;
            ContextFactory = contextFactory;
        }
    }
}
