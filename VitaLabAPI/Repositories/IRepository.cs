namespace VitaLabAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Create item of type T.
        /// </summary>
        public Task Create(T item);

        /// <summary>
        /// Get item of type T by id.
        /// </summary>
        public Task<T> GetById(int id);

        /// <summary>
        /// Get items of type T with pagination.
        /// </summary>
        public Task<IEnumerable<T>> GetWithPagination(int pageNumber);

        /// <summary>
        /// Update item of type T.
        /// </summary>
        public Task Update(T item);

        /// <summary>
        /// Delete item of type T by id.
        /// </summary>
        public Task DeleteById(int id);
    }
}
