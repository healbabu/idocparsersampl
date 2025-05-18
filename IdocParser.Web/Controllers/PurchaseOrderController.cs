using IdocParser.Business.Interfaces;
using IdocParser.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IdocParser.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IIdocGeneratorService _idocGeneratorService;
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(
            IIdocGeneratorService idocGeneratorService,
            ILogger<PurchaseOrderController> logger)
        {
            _idocGeneratorService = idocGeneratorService ?? throw new ArgumentNullException(nameof(idocGeneratorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates an IDocs file from a Purchase Order
        /// </summary>
        /// <param name="purchaseOrder">The Purchase Order data</param>
        /// <returns>Result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateIdoc([FromBody] PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null)
            {
                return BadRequest("Purchase Order data is required");
            }

            if (string.IsNullOrEmpty(purchaseOrder.PONumber))
            {
                return BadRequest("PO Number is required");
            }

            try
            {
                _logger.LogInformation("Received request to create IDocs for PO: {PONumber}", purchaseOrder.PONumber);
                
                // Generate IDocs file
                var idocFile = await _idocGeneratorService.GenerateIdocAsync(purchaseOrder);
                
                // Save IDocs file
                bool result = await _idocGeneratorService.SaveIdocAsync(idocFile);
                
                if (result)
                {
                    _logger.LogInformation("Successfully created and saved IDocs for PO: {PONumber}", purchaseOrder.PONumber);
                    return Created($"/api/purchaseorder/{purchaseOrder.PONumber}", new { IdocNumber = idocFile.IdocNumber });
                }
                else
                {
                    _logger.LogWarning("Failed to save IDocs for PO: {PONumber}", purchaseOrder.PONumber);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save IDocs file");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing IDocs for PO: {PONumber}", purchaseOrder.PONumber);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing IDocs: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves an IDocs file by PO number
        /// </summary>
        /// <param name="poNumber">The Purchase Order number</param>
        /// <returns>The IDocs file content</returns>
        [HttpGet("{poNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetIdoc(string poNumber)
        {
            // In a real application, this would retrieve the IDocs file from the repository
            // For now, we'll just return a message that this is not implemented yet
            _logger.LogInformation("GetIdoc operation is not implemented yet");
            return NotFound("Retrieving IDocs is not implemented yet");
        }
    }
} 