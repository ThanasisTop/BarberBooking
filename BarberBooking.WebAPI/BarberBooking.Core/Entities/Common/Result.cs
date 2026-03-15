using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Core.Entities.Common
{
    public class Result<T>
    {
        public T? Value { get; private set; }
        public string? Error { get; private set; }
        public bool IsSuccess => Error == null;

        public static Result<T> Success(T value) => new() { Value = value };
        public static Result<T> Failure(string error) => new() { Error = error };
    }
}
