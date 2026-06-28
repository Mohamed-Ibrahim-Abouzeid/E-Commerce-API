using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Common
{
    public sealed record Error(string code, string description, ErrorType Type = ErrorType.Failure)
    {
        public static Error Failure(
            string code = "General Failure",
            string description = "A General Failure Has Occurred")
            => new Error(code, description, ErrorType.Failure);
        public static Error Validation(
          string code = "General.Validation",
          string description = "One or more validation errors occurred.")
          => new(code, description, ErrorType.Validation);

        public static Error NotFound(
            string code = "General.NotFound",
            string description = "The requested resource was not found.")
            => new(code, description, ErrorType.NotFound);

        public static Error Conflict(
            string code = "General.Conflict",
            string description = "A conflict occurred.")
            => new(code, description, ErrorType.Conflict);

        public static Error Unauthorized(
            string code = "General.Unauthorized",
            string description = "You are not authorized to perform this action.")
            => new(code, description, ErrorType.Unauthorized);

        public static Error Forbidden(
            string code = "General.Forbidden",
            string description = "Access to this resource is forbidden.")
            => new(code, description, ErrorType.Forbidden);

        public static Error InvalidCredentials(
            string code = "General.InvalidCredentials",
            string description = "The provided credentials are invalid.")
            => new(code, description, ErrorType.InvalidCredentials);
    }
}
