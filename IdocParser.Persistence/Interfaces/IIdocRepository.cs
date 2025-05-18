using System.Threading.Tasks;

namespace IdocParser.Persistence.Interfaces
{
    /// <summary>
    /// Interface for IDocs persistence operations
    /// </summary>
    public interface IIdocRepository
    {
        /// <summary>
        /// Saves an IDocs file content to storage
        /// </summary>
        /// <param name="poNumber">Purchase Order number used as filename</param>
        /// <param name="idocContent">The content of the IDocs file</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> SaveIdocAsync(string poNumber, string idocContent);
        
        /// <summary>
        /// Retrieves an IDocs file content by PO number
        /// </summary>
        /// <param name="poNumber">Purchase Order number used as filename</param>
        /// <returns>The IDocs file content if found, null otherwise</returns>
        Task<string> GetIdocAsync(string poNumber);
    }
} 