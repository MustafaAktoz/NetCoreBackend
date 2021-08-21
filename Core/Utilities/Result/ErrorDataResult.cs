using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Result
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(true, data, message)
        {

        }

        public ErrorDataResult(T data) : base(true, data)
        {

        }

        public ErrorDataResult(string message) : base(true, default, message)
        {

        }

        public ErrorDataResult() : base(true, default)
        {

        }
    }
}
