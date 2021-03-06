﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.Expressions.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTransformers
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used 
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class DbFunctionTransformer : IExpressionTransformer<MethodCallExpression>
    {
        private Dictionary<MethodInfo, IDbFunction> _dbFunctions;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used 
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual ExpressionType[] SupportedExpressionTypes
        {
            get { return new[] { ExpressionType.Call }; }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used 
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public DbFunctionTransformer([NotNull] IEnumerable<IDbFunction> dbFunctions)
        {
            Check.NotNull(dbFunctions, nameof(dbFunctions));

            _dbFunctions = dbFunctions.ToDictionary(dbf => dbf.MethodInfo);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used 
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual Expression Transform(MethodCallExpression expression)
        {
            IDbFunction dbFunction;

            if (_dbFunctions.TryGetValue(expression.Method, out dbFunction))
            {
                var dbFunc = new DbFunctionExpression(dbFunction, expression);

                dbFunction.AfterDbFunctionExpressionCreate(dbFunc);

                return dbFunc;
            }

            return expression;
        }
    }
}
