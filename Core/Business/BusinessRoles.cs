using Core.Utilities.Abstract;
using Core.Utilities.Concreate.ErrorResult;
using Core.Utilities.Concreate.SuccessResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business
{
    public static class BusinessRoles
    {
        public static IResult Check(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return new ErrorResult();
                }
            }
            return new SuccessResult();
        }
    }
}
