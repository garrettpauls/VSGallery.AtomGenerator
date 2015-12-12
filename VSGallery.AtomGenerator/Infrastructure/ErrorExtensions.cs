using System.Collections.Generic;
using System.Linq;

namespace VSGallery.AtomGenerator.Infrastructure
{
    public static class ErrorExtensions
    {
        public static IEnumerable<TError> TakeErrors<TSuccess, TError>(this IEnumerable<ErrorSuccess<TSuccess, TError>> source)
        {
            return source
                .OfType<Error<TSuccess, TError>>()
                .Select(error => error.Value);
        }

        public static IEnumerable<TSuccess> TakeSuccess<TSuccess, TError>(this IEnumerable<ErrorSuccess<TSuccess, TError>> source)
        {
            return source
                .OfType<Success<TSuccess, TError>>()
                .Select(result => result.Value);
        }
    }
}
