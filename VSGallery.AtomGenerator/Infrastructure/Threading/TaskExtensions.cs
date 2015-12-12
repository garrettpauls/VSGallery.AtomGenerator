using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VSGallery.AtomGenerator.Infrastructure.Threading
{
    public static class TaskExtensions
    {
        public static Task<TResult[]> WhenAll<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            return Task.WhenAll(tasks);
        }
    }
}
