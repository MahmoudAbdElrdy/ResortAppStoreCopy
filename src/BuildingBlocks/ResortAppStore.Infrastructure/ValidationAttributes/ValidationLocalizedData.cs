using Common.Exceptions;
using Common.Extensions;
using Common.Interfaces;
using Common.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ValidationAttributes
{
    public class ValidationLocalizedData : ValidationAttribute
    {
       
        public ValidationLocalizedData(string nameEnProperty)
        {
            this.nameEnProperty = nameEnProperty;
        
        }


        private string nameEnProperty { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var nameEnValue =(string) validationContext.ObjectType.GetProperty(nameEnProperty).GetValue(validationContext.ObjectInstance, null);

            if ((/*value !=null &&*/ string.IsNullOrWhiteSpace(value.ToString())) && string.IsNullOrWhiteSpace(nameEnValue))
            {
               
                throw new UserFriendlyException("MustNameRequired");
            }
            else
            {
                return ValidationResult.Success;
            }
        }

    }
}