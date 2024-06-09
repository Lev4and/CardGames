using ImGuiNET;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.OpenGLGameEngine.Models.UI
{
    public class TextBox : UIElement
    {
        private bool _shouldClear;
        private bool _setFocusWhileActive;

        private string _defaultText;
        private string _inputString = string.Empty;

        private Action<string> _action;

        public TextBox(string defaultText, Action<string> action, ImGuiWindowFlags imGuiWindowFlags, string name, 
            Vector2? location = null, Keys toggleKey = Keys.Unknown, bool setFocusWhileActive = false) : 
                base(imGuiWindowFlags, name, location, toggleKey)
        {
            _defaultText = defaultText;
            _action = action;
            _setFocusWhileActive = setFocusWhileActive;
        }

        public override void StartRender()
        {
            base.StartRender();

            if (_setFocusWhileActive)
            {
                ImGui.SetKeyboardFocusHere();
            }

            if (ImGui.InputTextWithHint(string.Empty, _defaultText, ref _inputString, 256))
            {
                _action.Invoke(_inputString);
            }

            if (_shouldClear)
            {
                _shouldClear = !_shouldClear;
                _inputString = string.Empty;
            }
        }

        public void Clear()
        {
            _shouldClear = true;
        }
    }
}
