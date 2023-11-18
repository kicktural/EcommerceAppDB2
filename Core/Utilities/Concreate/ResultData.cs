using Core.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Concreate
{
    public class ResultData<T> : Result, IResultData<T>
    {
        public ResultData(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }


        public ResultData(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
