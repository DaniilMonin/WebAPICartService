using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProtoCart.Data.Common;

namespace ProtoCart.Services.Common.Extensions.Tasks
{
    public static class ParallelsExtensions
    {
        /// <summary>
        /// https://github.com/dotnet/runtime/issues/1946
        /// </summary>
        /// <param name="items"></param>
        /// <param name="degree"></param>
        /// <param name="body"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="captureContext"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static Task<TResult[]> ForEachAsync<TResult, TItem>(
            this IEnumerable<TItem> items,
            int degree,
            Func<TItem, CancellationToken, bool, Task<TItem>> body,
            CancellationToken cancellationToken,
            bool captureContext = false)
            where TItem : class
            where TResult : Aggregator<TItem>, new()
        {
            IList<IEnumerator<TItem>> partitions = Partitioner.Create<TItem>(items).GetPartitions(degree);
            
            IEnumerable<Task<TResult>> tasks = partitions.Select(partition => Task.Run<TResult>(
                async delegate
                {
                    TResult result = new TResult();

                    using (partition)
                    {
                        while (partition.MoveNext())
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            
                            TItem item = body == null ? partition.Current : await body(partition.Current, cancellationToken, captureContext).ConfigureAwait(captureContext);
                            
                            result.Aggregate(item);
                        }

                        return result;
                    }
                }, cancellationToken));

            return Task.WhenAll(tasks);
        }
    }
}