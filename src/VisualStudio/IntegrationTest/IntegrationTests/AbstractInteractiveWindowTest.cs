﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.IntegrationTest.Utilities;
using Microsoft.VisualStudio.IntegrationTest.Utilities.OutOfProcess;
using Xunit;

namespace Roslyn.VisualStudio.IntegrationTests
{
    public abstract class AbstractInteractiveWindowTest : AbstractIntegrationTest
    {
        private static readonly char[] LineSeparators = { '\r', '\n' };

        protected readonly CSharpInteractiveWindow_OutOfProc InteractiveWindow;

        protected AbstractInteractiveWindowTest(VisualStudioInstanceFactory instanceFactory)
            : base(instanceFactory, visualStudio => visualStudio.Instance.CSharpInteractiveWindow)
        {
            InteractiveWindow = (CSharpInteractiveWindow_OutOfProc)TextViewWindow;
            ClearInteractiveWindow();
        }

        protected void ClearInteractiveWindow()
        {
            InteractiveWindow.Initialize();
            InteractiveWindow.ClearScreen();
            InteractiveWindow.ShowWindow();
            InteractiveWindow.Reset();
        }

        protected void ClearReplText()
        {
            // Dismiss the pop-up (if any)
            VisualStudio.Instance.ExecuteCommand(WellKnownCommandNames.Edit_SelectionCancel);

            // Clear the line
            VisualStudio.Instance.ExecuteCommand(WellKnownCommandNames.Edit_SelectionCancel);
        }

        protected void DisableSuggestionMode()
            => VisualStudioWorkspaceOutOfProc.SetUseSuggestionMode(false);

        protected void EnableSuggestionMode()
            => VisualStudioWorkspaceOutOfProc.SetUseSuggestionMode(true);

        protected void Reset(bool waitForPrompt = true)
            => InteractiveWindow.Reset(waitForPrompt: true);

        protected void SubmitText(string text, bool waitForPrompt = true)
            => InteractiveWindow.SubmitText(text, waitForPrompt);

        protected void SendKeys(params object[] input)
        {
            VisualStudio.Instance.SendKeys.Send(input);
        }

        protected void InsertCode(string text)
            => InteractiveWindow.InsertCode(text);

        protected void PlaceCaret(string text, int charsOffset = 0, int occurrence = 0, bool extendSelection = false, bool selectBlock = false)
              => InteractiveWindow.PlaceCaret(
                  text,
                  charsOffset,
                  occurrence,
                  extendSelection,
                  selectBlock);

        protected void VerifyLastReplOutput(string expectedReplOutput)
        {
            var lastReplOutput = InteractiveWindow.GetLastReplOutput();
            Assert.Equal(expectedReplOutput, lastReplOutput);
        }

        protected void VerifyLastReplInput(string expectedReplInput)
        {
            var lastReplInput = InteractiveWindow.GetLastReplInput();
            Assert.Equal(expectedReplInput, lastReplInput);
        }

        protected void VerifyLastReplOutputContains(string expectedReplOutput)
        {
            var lastReplOutput = InteractiveWindow.GetLastReplOutput();
            Assert.Contains(expectedReplOutput, lastReplOutput);
        }

        protected void VerifyLastReplOutputEndsWith(string expectedReplOutput)
        {
            var lastReplOutput = InteractiveWindow.GetLastReplOutput();
            Assert.EndsWith(expectedReplOutput, lastReplOutput);
        }

        protected void VerifyReplPromptConsistency(string prompt, string output)
        {
            var replText = InteractiveWindow.GetReplText();
            var replTextLines = replText.Split(LineSeparators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var replTextLine in replTextLines)
            {
                if (!replTextLine.Contains(prompt))
                {
                    continue;
                }

                // The prompt must be at the beginning of the line
                Assert.StartsWith(prompt, replTextLine);

                var promptIndex = replTextLine.IndexOf(prompt, prompt.Length);

                // A 'subsequent' prompt is only allowed on a line containing #prompt
                if (promptIndex >= 0)
                {
                    Assert.StartsWith(prompt + "#prompt", replTextLine);
                    Assert.False(replTextLine.IndexOf(prompt, promptIndex + prompt.Length) >= 0);
                }

                // There must be no output on a prompt line.
                Assert.DoesNotContain(output, replTextLine);
            }
        }

        protected void VerifyCompletionUnexpectedItemDoesNotExist(params string[] unexpectedItems)
        {
            var completionItems = InteractiveWindow.GetCompletionItems();
            foreach (var unexpectedItem in unexpectedItems)
            {
                Assert.DoesNotContain(unexpectedItem, completionItems);
            }
        }

        protected void WaitWhileInteractiveIsRunning()
        {
            while (InteractiveWindow.IsRunning)
            {
                Task.Delay(50);
            }
        }

        protected void SendKeysAndWait(params object[] input)
        {
            InteractiveWindow.ExecuteActionAndWaitReadyForInput(() => SendKeys(input));
        }

        private void ExecuteCommandWait(string commandName, string argument = "")
        {
            InteractiveWindow.ExecuteActionAndWaitReadyForInput(() => ExecuteCommand(commandName, argument));
        }

        protected void WaitForReplOutput(string outputText)
            => InteractiveWindow.WaitForReplOutput(outputText);
    }
}