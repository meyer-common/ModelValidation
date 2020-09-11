using System.ComponentModel.DataAnnotations;

namespace Meyer.Common.ModelValidation
{
    /// <summary>
    /// Represents a failed model validation result
    /// </summary>
    public class FailedValidationResult
    {
        private ValidationException exception;

        /// <summary>
        /// Gets the name of the property which failed
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the ValidationException message
        /// </summary>
        public string Message => this.exception.Message;

        /// <summary>
        /// Instantiates a new instance of FailedResult
        /// </summary>
        /// <param name="key">The name of the property which failed</param>
        /// <param name="e">The exception generated from validation</param>
        public FailedValidationResult(string key, ValidationException e)
        {
            this.Key = key;
            this.exception = e;
        }
    }
}