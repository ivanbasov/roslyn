﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.IntegrationTest.Utilities;
using Xunit;

namespace Roslyn.VisualStudio.IntegrationTests.CSharp
{
    [Collection(nameof(SharedIntegrationHostFixture))]
    public class CSharpInteractive : AbstractInteractiveWindowTest
    {
        public CSharpInteractive(VisualStudioInstanceFactory instanceFactory)
            : base(instanceFactory)
        {
        }

        [Fact]
        public void BclMathCall()
        {
            SubmitText("Math.Sin(1)");
            WaitForLastReplOutput("0.8414709848078965");
        }

        [Fact]
        public void BclConsoleCall()
        {
            SubmitText(@"Console.WriteLine(""Hello, World!"");");
            WaitForLastReplOutput("Hello, World!");
        }

        [Fact]
        public void ForStatement()
        {
            SubmitText("for (int i = 0; i < 10; i++) Console.WriteLine(i * i);");
            WaitForLastReplOutputContains($"{81}");
        }

        [Fact]
        public void ForEachStatement()
        {
            SubmitText(@"foreach (var f in System.IO.Directory.GetFiles(@""c:\windows"")) Console.WriteLine($""{f}"".ToLower());");
            WaitForLastReplOutputContains(@"c:\windows\win.ini");
        }

        [Fact(Skip = "https://github.com/dotnet/roslyn/issues/17634")]
        public void TopLevelMethod()
        {
            SubmitText(@"int Fac(int x)
{
    return x < 1 ? 1 : x * Fac(x - 1);
}
Fac(4)");
            WaitForLastReplOutput($"{24}");
        }

        [Fact]
        public async Task WpfInteractionAsync()
        {
            SubmitText(@"#r ""WindowsBase""
#r ""PresentationCore""
#r ""PresentationFramework""
#r ""System.Xaml""");

            SubmitText(@"using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;");

            SubmitText(@"var w = new Window();
w.Title = ""Hello World"";
w.FontFamily = new FontFamily(""Calibri"");
w.FontSize = 24;
w.Height = 300;
w.Width = 300;
w.Topmost = true;
w.Visibility = Visibility.Visible;");

            var testValue = Guid.NewGuid();

            SubmitText($@"var b = new Button();
b.Content = ""{testValue}"";
b.Margin = new Thickness(40);
b.Click += (sender, e) => Console.WriteLine(""Hello, World!"");

var g = new Grid();
g.Children.Add(b);
w.Content = g;");

            await AutomationElementHelper.ClickAutomationElementAsync(testValue.ToString(), recursive: true);

            WaitForLastReplOutput("Hello, World!");
            SubmitText("b = null; w.Close(); w = null;");
        }

        [Fact]
        public void TypingHelpDirectiveWorks()
        {
            InteractiveWindow.ShowWindow(waitForPrompt: true);

            // Directly type #help, rather than sending it through SubmitText. We want to actually test
            // that completion doesn't interfere and there aren't problems with the content-type switching.
            VisualStudio.Instance.SendKeys.Send("#help");

            Assert.EndsWith("#help", InteractiveWindow.GetReplText());

            VisualStudio.Instance.SendKeys.Send("\n");
            WaitForReplOutputContains("REPL commands");
        }
    }
}
