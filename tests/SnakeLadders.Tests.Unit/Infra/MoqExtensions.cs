﻿using Moq.Language.Flow;
using System.Collections.Generic;

namespace SnakeLadders.Tests.Unit.Infra
{
    public static class MoqExtensions
    {
        public static void ReturnsInOrder<T, TResult>(this ISetup<T, TResult> setup,
          params TResult[] results) where T : class
        {
            setup.Returns(new Queue<TResult>(results).Dequeue);
        }
    }
}
