﻿// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Microsoft.CodeAnalysis.FxCopAnalyzers.Performance
{
    /// <summary>
    /// CA1821: Remove empty finalizers
    /// </summary>
    [ExportCodeFixProvider(CA1821DiagnosticAnalyzerRule.RuleId, LanguageNames.CSharp, LanguageNames.VisualBasic)]
    public sealed class CA1821CodeFixProvider : CodeFixProviderBase
    {
        public sealed override ImmutableArray<string> GetFixableDiagnosticIds()
        {
            return ImmutableArray.Create(CA1821DiagnosticAnalyzerRule.RuleId);
        }

        protected sealed override string GetCodeFixDescription(string ruleId)
        {
            return FxCopFixersResources.RemoveEmptyFinalizers;
        }

        internal override Task<Document> GetUpdatedDocumentAsync(Document document, SemanticModel model, SyntaxNode root, SyntaxNode nodeToFix, string diagnosticId, CancellationToken cancellationToken)
        {
            return Task.FromResult(document.WithSyntaxRoot(root.RemoveNode(nodeToFix, SyntaxRemoveOptions.KeepNoTrivia)));
        }
    }
}