using System;

namespace VSGallery.AtomGenerator.Infrastructure
{
    public sealed class Error<TSuccess, TError> : ErrorSuccess<TSuccess, TError>
    {
        public Error(TError value)
        {
            Value = value;
        }

        public override bool IsSuccess => false;
        public TError Value { get; }

        public override void If(Action<TSuccess> success, Action<TError> error)
        {
            error?.Invoke(Value);
        }

        public override void IfError(Action<TError> error)
        {
            error?.Invoke(Value);
        }

        public override void IfSuccess(Action<TSuccess> success)
        {
        }

        public override bool TryGetError(out TError error)
        {
            error = Value;
            return true;
        }

        public override bool TryGetSuccess(out TSuccess success)
        {
            success = default(TSuccess);
            return false;
        }
    }
}
