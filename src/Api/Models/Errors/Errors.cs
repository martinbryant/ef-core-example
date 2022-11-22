using System;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

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

            public static Error ValueIsRequired(string type) =>
                new Error(Value_Is_Required, $"a value is required for type `{type}`");

            public static Error ValueIsTooLong(string type, string value) =>
                new Error(Value_Is_Too_Long, $"value `{value}` is too long for type `{type}`");

            public static Error ValueIsInvalid(string type, string value) => 
                new Error(Value_Is_Invalid, $"value `{value}` is invalid for type `{type}`");

            internal static Error Exception(AggregateException exception)
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
            public static Error InvalidId(string entityName, string id) =>
                new Error(Id_Is_Invalid, $"An id of `{id}` is not valid for a `{entityName}`");
        }
        public static class Profile
        {
            public const string Profile_Already_Exists = "profile.already.exists";

            public static Error Exists(string name) =>
                new Error(Profile_Already_Exists, $"Profile `{name}` already exists"); 
        }

        public static class Depot
        {
            public const string Depot_Already_Exists = "depot.already.exists";
            public static Error DepotAlreadyExists(string depotId) =>
                new Error(Depot_Already_Exists, $"Depot `{depotId}` already exists");
        }
    }
}