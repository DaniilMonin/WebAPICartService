using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProtoCart.Services.Common.Extensions.Tasks
{
    public static class ParallelsExtensions
    {
        //https://github.com/dotnet/runtime/issues/1946
        public static Task ForEachAsync<TItem>(
            this IEnumerable<TItem> items,
            int degree,
            Func<TItem, CancellationToken, bool, Task<TItem>> body,
            CancellationToken cancellationToken,
            bool captureContext = false)
            where TItem : class
        {
            IList<IEnumerator<TItem>> partitions = Partitioner.Create<TItem>(items).GetPartitions(degree);
            
            IEnumerable<Task> tasks = partitions.Select(partition => Task.Run(
                async delegate
                {
                    using (partition)
                    {
                        while (partition.MoveNext())
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            
                            TItem item = await body(partition.Current, cancellationToken, captureContext).ConfigureAwait(captureContext);
                        }
                    }
                }, cancellationToken));

            return Task.WhenAll(tasks);
        }
    }
}