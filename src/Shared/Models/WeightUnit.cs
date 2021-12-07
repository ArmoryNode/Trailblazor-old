using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Trailblazor.Shared.Models
{
    public enum WeightUnit
    {
        [Display(Name = "Grams", ShortName = "g")]
        Grams,
        [Display(Name = "Kilograms", ShortName = "kg")]
        Kilograms,
        [Display(Name = "Ounces", ShortName = "oz")]
        Ounces,
        [Display(Name = "Pounds", ShortName = "lb")]
        Pounds
    }
}
