using VitLabData.DTOs.Create;

namespace VitaLabAPI.Services
{
    /// <summary>
    /// Generic service interface for creating entities.
    /// T needs to be inherited from CreateDTO model.
    /// </summary>
    /// <typeparam name="T">CreateDTO model.</typeparam>
    public interface IEntityCreate<T> where T : CreateRequest
    {
        /// <summary>
        /// Create entity of type T.
        /// </summary>
        public Task Create(T entity);
    }
}
