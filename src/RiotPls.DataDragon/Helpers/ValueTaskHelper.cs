using System;
using System.Threading.Tasks;

namespace RiotPls.DataDragon.Helpers
{
    internal static class ValueTaskHelper
    {
        public static ValueTask Create(bool isSyncPathAvailable, Action syncResult, Func<Task> asyncResult)
        {
            if (!isSyncPathAvailable)
                return new ValueTask(asyncResult());

            syncResult();
            return default;
        }

        public static ValueTask Create<TState>(bool isSyncPathAvailable, TState state, Action<TState> syncResult, Func<TState, Task> asyncResult)
        {
            if (!isSyncPathAvailable)
                return new ValueTask(asyncResult(state));

            syncResult(state);
            return default;
        }

        public static ValueTask<T> Create<T>(bool isSyncPathAvailable, Func<T> syncResult, Func<Task<T>> asyncResult)
            => isSyncPathAvailable
            ? new ValueTask<T>(syncResult())
            : new ValueTask<T>(asyncResult());

        public static ValueTask<T> Create<TState, T>(bool isSyncPathAvailable, TState state, Func<TState, T> syncResult, Func<TState, Task<T>> asyncResult) 
            => isSyncPathAvailable
            ? new ValueTask<T>(syncResult(state))
            : new ValueTask<T>(asyncResult(state));
    }
}
