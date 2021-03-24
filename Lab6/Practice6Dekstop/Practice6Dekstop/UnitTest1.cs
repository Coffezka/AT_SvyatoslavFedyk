using NUnit.Framework;
using FlaUI;
using FlaUI.Core;
using FlaUI.Core.Definitions;
using System.Runtime.InteropServices.ComTypes;
using FlaUI.Core.Input;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;
using System.Threading;
using FlaUI.Core.Conditions;
using System.Windows.Forms;
using System;
using FlaUI.Core.WindowsAPI;

namespace FlaUI.Core.UITests.Notepad.Screens
{
    public class FlaUITest
    {

        Application app;
        UIA3Automation automation;
        MainScreen window;

        [SetUp]
        public void Setup()
        {
            app = Application.Launch(@"C:\WINDOWS\system32\notepad.exe");
            automation = new UIA3Automation();
            window = app.GetMainWindow(automation).As<MainScreen>();
        }

        [Test]
        public void ZoomTest() {
            

            Assert.That(window.GetZoomPercent(), Is.EqualTo(100));
            window.DrawHighlightZoomStatus();


            window.ZoomIn();
            window.ZoomIn();
            Assert.That(window.GetZoomPercent(), Is.EqualTo(120));
            window.DrawHighlightZoomStatus();

            Thread.Sleep(500);

            window.ZoomOut();
            Assert.That(window.GetZoomPercent(), Is.EqualTo(110));
            window.DrawHighlightZoomStatus();

            Thread.Sleep(500);

        }

        [Test]
        public void TypeTest() {

            var bugText = "“It is as much of a mistake to underestimate yourself as to overestimate yourself.” ― Christie Golden, Arthas: Rise of the Lich King";
            var Text = "Geralt";

            window.DrawHighlightNotepadField();
            window.TypeText(Text);

            Assert.That(window.GetNotepadFieldText(),Is.EqualTo(Text));

        }

        [TearDown]
        public void Down() {
            app.Close();
        }


    }
}