using System.Collections.Generic;
using System.Threading.Tasks;

namespace VitaLabManager.Services
{
    /// <summary>
    /// Represents interface that indicates 
    /// item of type T support loading with pagination.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILoadByPage<T> where T : class
    {
        /// <summary>
        /// Load 20 items by page.
        /// </summary>
        /// <returns>20 elements.</returns>
        public Task<IEnumerable<T>> LoadByPage(int pageNumber);
    }
}
