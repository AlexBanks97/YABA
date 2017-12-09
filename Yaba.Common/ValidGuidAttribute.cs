using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yaba.Common
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ValidGuidAttribute : ValidationAttribute
    {
		public override bool IsValid(Object value)
		{
			bool result = true;

			if ((Guid)value == Guid.Empty)
				result = false;

			return result;
		}
	}
}
