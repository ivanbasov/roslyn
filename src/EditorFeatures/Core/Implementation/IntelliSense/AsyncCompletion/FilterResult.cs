﻿
// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.Completion;

namespace Microsoft.CodeAnalysis.Editor.Implementation.IntelliSense.AsyncCompletion
{
    internal struct FilterResult
    {
        public readonly CompletionItem CompletionItem;
        public readonly bool MatchedFilterText;

        public FilterResult(CompletionItem completionItem, bool matchedFilterText)
        {
            CompletionItem = completionItem;
            MatchedFilterText = matchedFilterText;
        }
    }
}
