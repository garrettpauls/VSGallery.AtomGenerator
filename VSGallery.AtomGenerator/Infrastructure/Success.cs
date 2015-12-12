using System;
using System.Collections;

namespace VSGallery.AtomGenerator.Infrastructure
{
    public sealed class Success<TSuccess, TError> : ErrorSuccess<TSuccess, TError>
    {
        public Success(TSuccess value)
        {
            Value = value;
        }

        public override bool IsSuccess => true;
        public TSuccess Value { get; }

        public override void If(Action<TSuccess> success, Action<TError> error)
        {
            success?.Invoke(Value);
        }

        public override void IfError(Action<TError> error)
        {
        }

        public override void IfSuccess(Action<TSuccess> success)
        {
            success?.Invoke(Value);
        }

        public override bool TryGetError(out TError error)
        {
            error = default(TError);
            return false;
        }

        public override bool TryGetSuccess(out TSuccess success)
        {
            success = Value;
            return true;
        }
    }
}
