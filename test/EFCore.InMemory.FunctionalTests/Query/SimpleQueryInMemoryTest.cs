﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class SimpleQueryInMemoryTest : SimpleQueryTestBase<NorthwindQueryInMemoryFixture<NoopModelCustomizer>>
    {
        public SimpleQueryInMemoryTest(
            NorthwindQueryInMemoryFixture<NoopModelCustomizer> fixture,
            // ReSharper disable once UnusedParameter.Local
            ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            TestLoggerFactory.TestOutputHelper = testOutputHelper;
        }

        public override void View_with_nav_defining_query()
        {
            base.View_with_nav_defining_query();
        }
    }
}
