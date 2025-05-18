using IdocParser.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IdocParser.Persistence.Repositories
{
    /// <summary>
    /// File-based implementation of the IDocs repository
    /// </summary>
    public class FileIdocRepository : IIdocRepository
    {
        private readonly string _basePath;
        private readonly ILogger<FileIdocRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the FileIdocRepository class
        /// </summary>
        /// <param name="basePath">Base directory path where files will be stored</param>
        /// <param name="logger">Logger instance</param>
        public FileIdocRepository(string basePath, ILogger<FileIdocRepository> logger)
        {
            _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            // Ensure directory exists
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
                _logger.LogInformation("Created IDocs storage directory: {DirectoryPath}", _basePath);
            }
        }

        /// <inheritdoc />
        public async Task<bool> SaveIdocAsync(string poNumber, string idocContent)
        {
            if (string.IsNullOrEmpty(poNumber))
            {
                throw new ArgumentException("PO Number cannot be null or empty", nameof(poNumber));
            }

            try
            {
                string filePath = GetFilePath(poNumber);
                await File.WriteAllTextAsync(filePath, idocContent);
                _logger.LogInformation("Successfully saved IDocs file for PO: {PONumber}", poNumber);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving IDocs file for PO: {PONumber}", poNumber);
                return false;
            }
        }

        /// <inheritdoc />
        public async Task<string> GetIdocAsync(string poNumber)
        {
            if (string.IsNullOrEmpty(poNumber))
            {
                throw new ArgumentException("PO Number cannot be null or empty", nameof(poNumber));
            }

            try
            {
                string filePath = GetFilePath(poNumber);
                
                if (!File.Exists(filePath))
                {
                    _logger.LogWarning("IDocs file not found for PO: {PONumber}", poNumber);
                    return null;
                }

                string content = await File.ReadAllTextAsync(filePath);
                _logger.LogInformation("Successfully retrieved IDocs file for PO: {PONumber}", poNumber);
                return content;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving IDocs file for PO: {PONumber}", poNumber);
                return null;
            }
        }

        private string GetFilePath(string poNumber)
        {
            // Sanitize filename to ensure it's safe for file system
            string safeFileName = string.Join("_", poNumber.Split(Path.GetInvalidFileNameChars()));
            return Path.Combine(_basePath, $"{safeFileName}.idoc");
        }
    }
} 