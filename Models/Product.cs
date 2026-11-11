using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsValidation.Models
{
    public class Product : IValidatableObject
    {
        public enum Category { Toy, Technique, Clothes, Transport}

        public int Id { get; set; }
        public Category Type { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 100000)]
        public decimal Price { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!string.IsNullOrEmpty(Description) && Description.Length <= 2)
            {
                yield return new ValidationResult(
                    "Description should have more than 2 symbols", 
                    new[] { nameof(Description)} 
                );
            }

            if(
                !string.IsNullOrEmpty(Description) && 
                !string.IsNullOrEmpty(Name) && 
                !Description.StartsWith(Name))
            {
                yield return new ValidationResult(
                    $"Description should start with {Name}", 
                    new[] { nameof(Description)} 
                );
            }

            if(!string.IsNullOrEmpty(Description) && Description == Name)
            {
                yield return new ValidationResult(
                    "Description should not be equal Name", 
                    new[] { nameof(Description)} 
                );
            }
        }
    }
}
