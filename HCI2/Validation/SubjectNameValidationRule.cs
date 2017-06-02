using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HCI2.Validation
{
    public class SubjectNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string content = value.ToString();

            if (string.Equals(content, ""))
            {
                return new ValidationResult(false, "Field can not be empty!");

            }
            else
                return new ValidationResult(true, null);
        }
    }

    public class SubjectDescriptionValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string content = value.ToString();

            if (string.Equals(content, ""))
            {
                return new ValidationResult(false, "Field can not be empty!");

            }
            else
                return new ValidationResult(true, null);
        }
    }

    public class SubjectDurationValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int d;
            if (Int32.TryParse(value.ToString(), out d))
            {
                if (d < 0) return new ValidationResult(false, "Value can not be negative.");
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "It must be an integer.");
            }
        }
    }

    public class SubjectTermsValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int d;
            if (Int32.TryParse(value.ToString(), out d))
            {
                if (d < 0) return new ValidationResult(false, "Value can not be negative.");
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "It must be an integer.");
            }
        }
    }

    public class SubjectGroupSizeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int d;
            if (Int32.TryParse(value.ToString(), out d))
            {
                if (d < 0) return new ValidationResult(false, "Value can not be negative.");
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "It must be an integer.");
            }
        }
    }
}
