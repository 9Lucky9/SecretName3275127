namespace VitaLabAPI.Services
{
    /// <summary>
    /// Generic service interface.
    /// T needs to be Entity model.
    /// </summary>
    /// <typeparam name="T">Entity model.</typeparam>
    public interface IServiceBase<T> where T : class
    {
        /// <summary>
        /// Get item of type T by id.
        /// </summary>
        public Task<T> GetById(int id);

        /// <summary>
        /// Delete item of type T by id.
        /// </summary>
        public Task DeleteById(int id);

        /// <summary>
        /// Get items of type T by page.
        /// </summary>
        public Task<IEnumerable<T>> GetByPage(int page); 
    }
}
