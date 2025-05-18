using IdocParser.Business.Models;
using System.Threading.Tasks;

namespace IdocParser.Business.Interfaces
{
    /// <summary>
    /// Interface for IDocs generation service
    /// </summary>
    public interface IIdocGeneratorService
    {
        /// <summary>
        /// Generates an IDocs file from a Purchase Order
        /// </summary>
        /// <param name="purchaseOrder">The Purchase Order data</param>
        /// <returns>The generated IDocs file model</returns>
        Task<IdocFile> GenerateIdocAsync(PurchaseOrder purchaseOrder);
        
        /// <summary>
        /// Saves an IDocs file using the persistence layer
        /// </summary>
        /// <param name="idocFile">The IDocs file to save</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> SaveIdocAsync(IdocFile idocFile);
    }
} 