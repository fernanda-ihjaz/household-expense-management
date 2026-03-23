using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdExpenseManagement.Application.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }

        public static Result Ok() => new() { IsSuccess = true };
        public static Result Fail(string error) => new() { IsSuccess = false, Error = error };
    }

    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string? Error { get; private set; }

        public static Result<T> Ok(T data)
            => new() { IsSuccess = true, Data = data };

        public static Result<T> Fail(string error)
            => new() { IsSuccess = false, Error = error };
    }
}
