// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.CodeAnalysis.Options;

namespace Microsoft.CodeAnalysis.Completion
{
    internal static class CompletionOptions
    {
        // This is serialized by the Visual Studio-specific LanguageSettingsPersister
        public static readonly PerLanguageOption<bool> HideAdvancedMembers = new PerLanguageOption<bool>(nameof(CompletionOptions), nameof(HideAdvancedMembers), defaultValue: false);

        // This is serialized by the Visual Studio-specific LanguageSettingsPersister
        public static readonly PerLanguageOption<bool> TriggerOnTyping = new PerLanguageOption<bool>(nameof(CompletionOptions), nameof(TriggerOnTyping), defaultValue: true);

        public static readonly PerLanguageOption<bool> TriggerOnTypingLetters = new PerLanguageOption<bool>(nameof(CompletionOptions), nameof(TriggerOnTypingLetters), defaultValue: true,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.TriggerOnTypingLetters"));
        public static readonly PerLanguageOption<bool?> TriggerOnDeletion = new PerLanguageOption<bool?>(nameof(CompletionOptions), nameof(TriggerOnDeletion), defaultValue: null,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.TriggerOnDeletion"));

        public static readonly PerLanguageOption<EnterKeyRule> EnterKeyBehavior =
            new PerLanguageOption<EnterKeyRule>(nameof(CompletionOptions), nameof(EnterKeyBehavior), defaultValue: EnterKeyRule.Default,
                storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.EnterKeyBehavior"));

        public static readonly PerLanguageOption<SnippetsRule> SnippetsBehavior =
            new PerLanguageOption<SnippetsRule>(nameof(CompletionOptions), nameof(SnippetsBehavior), defaultValue: SnippetsRule.Default,
                storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.SnippetsBehavior"));

        // Dev15 options
        public static readonly PerLanguageOption<bool> ShowCompletionItemFilters = new PerLanguageOption<bool>(nameof(CompletionOptions), nameof(ShowCompletionItemFilters), defaultValue: true,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.ShowCompletionItemFilters"));

        public static readonly PerLanguageOption<bool> HighlightMatchingPortionsOfCompletionListItems = new PerLanguageOption<bool>(nameof(CompletionOptions), nameof(HighlightMatchingPortionsOfCompletionListItems), defaultValue: true,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.HighlightMatchingPortionsOfCompletionListItems"));

        public static readonly PerLanguageOption<bool> BlockForCompletionItems = new PerLanguageOption<bool>(
            nameof(CompletionOptions), nameof(BlockForCompletionItems), defaultValue: true,
            storageLocations: new RoamingProfileStorageLocation($"TextEditor.%LANGUAGE%.Specific.{BlockForCompletionItems}"));

        public static readonly PerLanguageOption<bool> ShowNameSuggestions =
            new PerLanguageOption<bool>(nameof(CompletionOptions), nameof(ShowNameSuggestions), defaultValue: true,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.ShowNameSuggestions"));

        public static readonly PerLanguageOption<int> Delay_InitializeCompletion =
            new PerLanguageOption<int>(nameof(CompletionOptions), nameof(Delay_InitializeCompletion), defaultValue: 0,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.Delay_InitializeCompletion"));

        public static readonly PerLanguageOption<int> Delay_GetCompletionContext =
            new PerLanguageOption<int>(nameof(CompletionOptions), nameof(Delay_GetCompletionContext), defaultValue: 0,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.Delay_GetCompletionContext"));

        public static readonly PerLanguageOption<int> Delay_Sort =
            new PerLanguageOption<int>(nameof(CompletionOptions), nameof(Delay_Sort), defaultValue: 0,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.Delay_Sort"));

        public static readonly PerLanguageOption<int> Delay_Update =
            new PerLanguageOption<int>(nameof(CompletionOptions), nameof(Delay_Update), defaultValue: 0,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.Delay_Update"));

        public static readonly PerLanguageOption<int> Delay_ShouldCommitCompletion =
            new PerLanguageOption<int>(nameof(CompletionOptions), nameof(Delay_ShouldCommitCompletion), defaultValue: 0,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.Delay_ShouldCommitCompletion"));

        public static readonly PerLanguageOption<int> Delay_TryCommit =
            new PerLanguageOption<int>(nameof(CompletionOptions), nameof(Delay_TryCommit), defaultValue: 0,
            storageLocations: new RoamingProfileStorageLocation("TextEditor.%LANGUAGE%.Specific.Delay_TryCommit"));

        public static IEnumerable<PerLanguageOption<bool>> GetDev15CompletionOptions()
        {
            yield return ShowCompletionItemFilters;
            yield return HighlightMatchingPortionsOfCompletionListItems;
        }
    }

    internal static class CompletionControllerOptions
    {
        public static readonly Option<bool> FilterOutOfScopeLocals = new Option<bool>(nameof(CompletionControllerOptions), nameof(FilterOutOfScopeLocals), defaultValue: true);
        public static readonly Option<bool> ShowXmlDocCommentCompletion = new Option<bool>(nameof(CompletionControllerOptions), nameof(ShowXmlDocCommentCompletion), defaultValue: true);
    }
}
