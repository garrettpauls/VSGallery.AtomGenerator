using System;

namespace VSGallery.AtomGenerator.Infrastructure
{
    public abstract class ErrorSuccess<TSuccess, TError>
    {
        internal ErrorSuccess()
        {
        }

        public abstract bool IsSuccess { get; }

        public abstract void If(Action<TSuccess> success, Action<TError> error);

        public abstract void IfError(Action<TError> error);

        public abstract void IfSuccess(Action<TSuccess> success);

        public abstract bool TryGetError(out TError error);

        public abstract bool TryGetSuccess(out TSuccess success);
    }
}
