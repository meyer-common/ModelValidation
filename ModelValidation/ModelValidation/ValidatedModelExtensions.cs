using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Meyer.Common.ModelValidation
{
    /// <summary>
    /// Validates a model based on validation data annotaions
    /// </summary>
    public static class ValidatedModelExtensions
    {
        /// <summary>
        /// Validates a model based on validation data annotaions
        /// </summary>
        /// <param name="modelToValidate">The object to validate</param>
        /// <returns>Returns any failed results in validation</returns>
        public static ValidationResults ValidateRoot(this object modelToValidate)
        {
            ValidationResults response = new ValidationResults();
            foreach (var propertyInfo in modelToValidate.GetType().GetProperties())
            {
                foreach (var attribute in propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>())
                {
                    try { attribute.Validate(propertyInfo.GetValue(modelToValidate, null), ""); }
                    catch (ValidationException e) { response.Results.Add(new FailedValidationResult(propertyInfo.Name, e)); }
                }
            }
            return response;
        }

        /// <summary>
        /// Validates a model based on validation data annotaions, including any complex properties and collections
        /// </summary>
        /// <param name="modelToValidate">The object to validate</param>
        /// <returns>Returns any failed results in validation</returns>
        public static ValidationResults ValidateNested(this object modelToValidate)
        {
            return ValidateNested(modelToValidate, null, null);
        }

        private static ValidationResults ValidateNested(this object modelToValidate, ValidationResults validationResults, List<object> complexList)
        {
            if (validationResults == null) validationResults = new ValidationResults();
            if (complexList == null) complexList = new List<object>();

            if (modelToValidate == null)
                return validationResults;

            if (modelToValidate.GetType().IsCollection())
            {
                foreach (var listItem in (ICollection)modelToValidate)
                {
                    listItem.ValidateNested(validationResults, complexList);
                }
            }
            else if (modelToValidate.GetType().IsComplexType() && !complexList.Contains(modelToValidate))
            {
                complexList.Add(modelToValidate);

                foreach (var propertyInfo in modelToValidate.GetType().GetProperties())
                {
                    if (propertyInfo.PropertyType.IsCollection() || propertyInfo.PropertyType.IsComplexType())
                    {
                        propertyInfo.GetValue(modelToValidate, null).ValidateNested(validationResults, complexList);
                    }
                    else
                    {
                        foreach (var attribute in propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>())
                        {
                            try { attribute.Validate(propertyInfo.GetValue(modelToValidate, null), ""); }
                            catch (ValidationException e) { validationResults.Results.Add(new FailedValidationResult(propertyInfo.Name, e)); }
                        }
                    }
                }
            }
            return validationResults;
        }
    }
}