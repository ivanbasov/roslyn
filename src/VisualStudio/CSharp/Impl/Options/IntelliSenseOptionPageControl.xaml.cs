﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Windows;
using ICSharpCode.Decompiler.IL;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.VisualStudio.LanguageServices.Implementation.Options;

namespace Microsoft.VisualStudio.LanguageServices.CSharp.Options
{
    internal partial class IntelliSenseOptionPageControl : AbstractOptionPageControl
    {
        public IntelliSenseOptionPageControl(OptionStore optionStore) : base(optionStore)
        {
            InitializeComponent();

            BindToOption(Show_completion_item_filters, CompletionOptions.ShowCompletionItemFilters, LanguageNames.CSharp);
            BindToOption(Highlight_matching_portions_of_completion_list_items, CompletionOptions.HighlightMatchingPortionsOfCompletionListItems, LanguageNames.CSharp);

            BindToOption(Show_completion_list_after_a_character_is_typed, CompletionOptions.TriggerOnTypingLetters, LanguageNames.CSharp);
            Show_completion_list_after_a_character_is_deleted.IsChecked = this.OptionStore.GetOption(
                CompletionOptions.TriggerOnDeletion, LanguageNames.CSharp) == true;
            Show_completion_list_after_a_character_is_deleted.IsEnabled = Show_completion_list_after_a_character_is_typed.IsChecked == true;

            BindToOption(Never_include_snippets, CompletionOptions.SnippetsBehavior, SnippetsRule.NeverInclude, LanguageNames.CSharp);
            BindToOption(Always_include_snippets, CompletionOptions.SnippetsBehavior, SnippetsRule.AlwaysInclude, LanguageNames.CSharp);
            BindToOption(Include_snippets_when_question_Tab_is_typed_after_an_identifier, CompletionOptions.SnippetsBehavior, SnippetsRule.IncludeAfterTypingIdentifierQuestionTab, LanguageNames.CSharp);

            BindToOption(Never_add_new_line_on_enter, CompletionOptions.EnterKeyBehavior, EnterKeyRule.Never, LanguageNames.CSharp);
            BindToOption(Only_add_new_line_on_enter_with_whole_word, CompletionOptions.EnterKeyBehavior, EnterKeyRule.AfterFullyTypedWord, LanguageNames.CSharp);
            BindToOption(Always_add_new_line_on_enter, CompletionOptions.EnterKeyBehavior, EnterKeyRule.Always, LanguageNames.CSharp);

            BindToOption(Show_name_suggestions, CompletionOptions.ShowNameSuggestions, LanguageNames.CSharp);

            BindToOption(Delay_InitializeCompletion, CompletionOptions.Delay_InitializeCompletion, LanguageNames.CSharp);
            BindToOption(Delay_GetCompletionContext, CompletionOptions.Delay_GetCompletionContext, LanguageNames.CSharp);
            BindToOption(Delay_Sort, CompletionOptions.Delay_Sort, LanguageNames.CSharp);
            BindToOption(Delay_Update, CompletionOptions.Delay_Update, LanguageNames.CSharp);
            BindToOption(Delay_ShouldCommitCompletion, CompletionOptions.Delay_ShouldCommitCompletion, LanguageNames.CSharp);
            BindToOption(Delay_TryCommit, CompletionOptions.Delay_TryCommit, LanguageNames.CSharp);
        }

        private void Show_completion_list_after_a_character_is_typed_Checked(object sender, RoutedEventArgs e)
        {
            Show_completion_list_after_a_character_is_deleted.IsEnabled = Show_completion_list_after_a_character_is_typed.IsChecked == true;
        }

        private void Show_completion_list_after_a_character_is_typed_Unchecked(object sender, RoutedEventArgs e)
        {
            Show_completion_list_after_a_character_is_deleted.IsEnabled = Show_completion_list_after_a_character_is_typed.IsChecked == true;
            Show_completion_list_after_a_character_is_deleted.IsChecked = false;
            Show_completion_list_after_a_character_is_deleted_Unchecked(sender, e);
        }

        private void Show_completion_list_after_a_character_is_deleted_Checked(object sender, RoutedEventArgs e)
        {
            this.OptionStore.SetOption(CompletionOptions.TriggerOnDeletion, LanguageNames.CSharp, value: true);
        }

        private void Show_completion_list_after_a_character_is_deleted_Unchecked(object sender, RoutedEventArgs e)
        {
            this.OptionStore.SetOption(CompletionOptions.TriggerOnDeletion, LanguageNames.CSharp, value: false);
        }
    }
}
