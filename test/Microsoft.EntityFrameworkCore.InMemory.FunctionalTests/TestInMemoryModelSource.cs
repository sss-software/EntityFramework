// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.InMemory.FunctionalTests
{
    public class TestInMemoryModelSource : InMemoryModelSource
    {
        private readonly TestModelSource _testModelSource;

        public TestInMemoryModelSource(
            Action<ModelBuilder> onModelCreating,
            IDbSetFinder setFinder,
            ICoreConventionSetBuilder coreConventionSetBuilder,
            CoreModelValidator coreModelValidator)
            : base(setFinder, coreConventionSetBuilder, new ModelCustomizer(), new ModelCacheKeyFactory(), coreModelValidator)
        {
            _testModelSource = new TestModelSource(
                onModelCreating, setFinder, coreConventionSetBuilder, new ModelCustomizer(), new ModelCacheKeyFactory(), coreModelValidator);
        }

        public override IModel GetModel(DbContext context, IConventionSetBuilder conventionSetBuilder, IModelValidator validator, IDbFunctionInitializer dbFunctionInitialzier)
            => _testModelSource.GetModel(context, conventionSetBuilder, validator, dbFunctionInitialzier);

        public static Func<IServiceProvider, InMemoryModelSource> GetFactory(Action<ModelBuilder> onModelCreating)
            => p => new TestInMemoryModelSource(
                onModelCreating,
                p.GetRequiredService<IDbSetFinder>(),
                p.GetRequiredService<ICoreConventionSetBuilder>(),
                p.GetRequiredService<CoreModelValidator>());
    }
}
