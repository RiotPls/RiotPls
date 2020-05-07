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

        public static ValueTask Create<TState>(TState state, bool isSyncPathAvailable, Action<TState> syncResult, Func<TState, Task> asyncResult)
        {
            if (!isSyncPathAvailable)
                return new ValueTask(asyncResult(state));

            syncResult(state);
            return default;
        }

        public static ValueTask Create<TState>(TState state, Func<TState, bool> isSyncPathAvailable, Action<TState> syncResult, Func<TState, Task> asyncResult)
        {
            if (!isSyncPathAvailable(state))
                return new ValueTask(asyncResult(state));

            syncResult(state);
            return default;
        }

        public static ValueTask<T> Create<T>(bool isSyncPathAvailable, Func<T> syncResult, Func<Task<T>> asyncResult)
            => isSyncPathAvailable
            ? new ValueTask<T>(syncResult())
            : new ValueTask<T>(asyncResult());

        public static ValueTask<T> Create<TState, T>(TState state, bool isSyncPathAvailable, Func<TState, T> syncResult, Func<TState, Task<T>> asyncResult) 
            => isSyncPathAvailable
            ? new ValueTask<T>(syncResult(state))
            : new ValueTask<T>(asyncResult(state));

        public static ValueTask<T> Create<TState, T>(TState state, Func<TState, bool> isSyncPathAvailable, Func<TState, T> syncResult, Func<TState, Task<T>> asyncResult)
            => isSyncPathAvailable(state)
            ? new ValueTask<T>(syncResult(state))
            : new ValueTask<T>(asyncResult(state));
    }
}
