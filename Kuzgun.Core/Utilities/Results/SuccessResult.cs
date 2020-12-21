using System;
using System.Collections.Generic;
using System.Text;

namespace Kuzgun.Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult( string message) : base(success:true, message)
        {
        }

        public SuccessResult() : base(true)
        {
        }
    }
}
