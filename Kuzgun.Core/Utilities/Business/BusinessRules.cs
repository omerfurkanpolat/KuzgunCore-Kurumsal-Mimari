using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Utilities.Results;

namespace Kuzgun.Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var result in logics)
            {
                if (!result.Success)
                {
                    return result;
                }
            }

            return null;
        }

    }
}
