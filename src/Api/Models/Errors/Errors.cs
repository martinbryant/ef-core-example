using System;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace ef_core_example.Models
{
    public static class Errors
    {
        public static class General
        {
            public const string Record_Not_Found    = "record.not.found";
            public const string Id_Is_Invalid       = "id.is.invalid";
            public const string Value_Is_Required   = "value.is.required";
            public const string Value_Is_Too_Long   = "value.is.too.long";
            public const string Value_Is_Invalid    = "value.is.invalid";

            public static Error NotFound(string entityName, Guid id) => 
                new Error(Record_Not_Found, $"`{entityName}` with an id `{id}` does not exist");

            internal static Error ValueIsRequired(string type) =>
                new Error(Value_Is_Required, $"a value is required for type `{type}`");

            internal static Error ValueIsTooLong(string type, string value) =>
                new Error(Value_Is_Too_Long, $"value `{value}` is too long for type `{type}`");

            internal static Error ValueIsInvalid(string type, string value) => 
                new Error(Value_Is_Invalid, $"value `{value}` is invalid for type `{type}`");

            public static Error Exception(AggregateException exception)
            {
                MySqlException sqlException = default;
                if(exception.InnerException is DbUpdateException dbex)
                {
                    sqlException = (MySqlException)dbex?.InnerException;
                }
                if(exception.InnerException is MySqlException mex)
                {
                    sqlException = (MySqlException)mex;
                }

                return ExceptionError(sqlException);
            }

            private static Error ExceptionError(MySqlException ex) => 
                new Error("sql.exception", $"{ex.Message}");
            internal static Error InvalidId(string entityName, string id) =>
                new Error(Id_Is_Invalid, $"An id of `{id}` is not valid for a `{entityName}`");
        }
        public static class Profile
        {
            public static Error NameIsInvalid(string name) =>
                new Error("profile.name.is.invalid", $"Profile name `{name}` is invalid");

            public static Error NameIsTooLong(string name) =>
                new Error("profile.name.is.invalid", $"Profile name `{name} is too long");
        }
    }
}