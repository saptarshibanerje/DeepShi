using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DeepShiApp.Utilities.CustomValidator
{
    public sealed class ValidSelectedItemAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int minvalue;

        public ValidSelectedItemAttribute(int MinValue)
        {
            this.minvalue = MinValue;
        }
        public string GetErrorMessage() => $"Value can not be less than {minvalue}";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                int val = Convert.ToInt32(value);
                if (val < minvalue)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            catch (Exception)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-ddlmval", GetErrorMessage());

            MergeAttribute(context.Attributes, "data-val-ddlmval-minvalue", minvalue.ToString());
        }
        private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
    }
}
