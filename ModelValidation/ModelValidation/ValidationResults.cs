using System.Collections.Generic;
using System.Linq;

namespace Meyer.Common.ModelValidation
{
    /// <summary>
    /// Represents response information from a validated model
    /// </summary>
    public class ValidationResults
    {
        /// <summary>
        /// Gets whether the request was successful
        /// </summary>
        public bool IsSuccess => !this.Results.Any();

        /// <summary>
        /// Gets the returned result messages
        /// </summary>
        public List<FailedValidationResult> Results { get; }

        /// <summary>
        /// Instantiates a new ValidationResults object
        /// </summary>
        public ValidationResults()
        {
            this.Results = new List<FailedValidationResult>();
        }

        /// <summary>
        /// Instantiates a new ValidationResults object, merging the results of another results object
        /// </summary>
        /// <param name="results">An existing result to merge with</param>
        public ValidationResults(ValidationResults results)
        {
            this.Results = results.Results;
        }
    }
}