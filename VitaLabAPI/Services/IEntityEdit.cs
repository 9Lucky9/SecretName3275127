namespace VitaLabAPI.Services
{
    /// <summary>
    /// Generic service interface.
    /// T needs to be DTO model.
    /// </summary>
    /// <typeparam name="T">DTO model.</typeparam>
    public interface IEntityEdit<T> where T : class
    {
        /// <summary>
        /// Edit item of type T.
        /// </summary>
        public Task Edit(T entity);
    }
}
