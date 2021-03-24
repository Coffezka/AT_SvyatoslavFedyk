using System;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;

namespace FlaUI.Core.UITests.Notepad.Screens
{
    public class MainScreen : Window
    {
        public MainScreen(FrameworkAutomationElementBase frameworkAutomationElement) : base(frameworkAutomationElement)
        {
        }

        protected TextBox TextZoom => FindFirstByXPath("/StatusBar/Text[3]").AsTextBox();

        protected TextBox NotepadField => FindFirstByXPath("/Document").AsTextBox();


        public void ZoomIn()
        {
            Keyboard.Pressing(VirtualKeyShort.CONTROL);
            Keyboard.Press(VirtualKeyShort.OEM_PLUS);
            Keyboard.Release(VirtualKeyShort.CONTROL);
        }

        public void ZoomOut()
        {
            Keyboard.Pressing(VirtualKeyShort.CONTROL);
            Keyboard.Press(VirtualKeyShort.OEM_MINUS);
            Keyboard.Release(VirtualKeyShort.CONTROL);
        }

        public void TypeText(String Text) {

            Keyboard.Type(Text);
            //NotepadField.Text = Text;
        }

        public void DrawHighlightZoomStatus() {
            TextZoom.DrawHighlight();
        }

        public void DrawHighlightNotepadField()
        {
            NotepadField.DrawHighlight();
        }

        public string GetNotepadFieldText()
        {
            var Text = NotepadField.Text;
            return Text;
        }

        public int GetZoomPercent()
        {
            var zoomText = TextZoom.Name;
            var zoomNumberString = Regex.Match(zoomText, @"[0-9]+").ToString();
            return Convert.ToInt32(zoomNumberString);
        }


    }
}
