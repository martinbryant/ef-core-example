using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace ef_core_example.Models
{
    public static class TaskExt
    {
        public static Task<Result<TEntity, Error>> ToResult<T, TEntity>(this Task<T> task, TEntity entity) =>
            task.ContinueWith(t => 
                t.Status == TaskStatus.Faulted 
                    ? Result.Failure<TEntity, Error>(Errors.General.Exception(t.Exception))
                    : Result.Success<TEntity, Error>(entity));

        public static Task<Result<T, Error>> ToResult<T>(this Task<T> task) =>
            task.ContinueWith(t => 
                t.Status == TaskStatus.Faulted 
                    ? Result.Failure<T, Error>(Errors.General.Exception(t.Exception))
                    : Result.Success<T, Error>(t.Result));
    }
}