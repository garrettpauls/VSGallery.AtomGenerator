namespace VSGallery.AtomGenerator.Infrastructure
{
    public sealed class ErrorSuccessFactory<TSuccess, TError>
    {
        public ErrorSuccess<TSuccess, TError> Error(TError error)
            => new Error<TSuccess, TError>(error);

        public ErrorSuccess<TSuccess, TError> Success(TSuccess success)
            => new Success<TSuccess, TError>(success);
    }
}
