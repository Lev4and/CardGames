using ImGuiNET;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.OpenGLGameEngine.Models.UI
{
    public abstract class UIElement
    {
        private protected string _name;
        private protected Vector2? _location;
        private protected ImGuiWindowFlags _imGuiWindowFlags;

        public bool IsActive { get; set; }

        public Keys ToggleKey { get; set; }

        public event EventHandler? Toggle;

        protected UIElement(ImGuiWindowFlags imGuiWindowFlags, string name, Vector2? location = null, 
            Keys toggleKey = Keys.Unknown)
        {
            _name = name;
            _location = location;
            _imGuiWindowFlags = imGuiWindowFlags;

            ToggleKey = toggleKey;
        }

        public virtual void StartRender()
        {
            if (_location is not null)
            {
                ImGui.SetNextWindowPos(new System.Numerics.Vector2(_location.Value.X, _location.Value.Y));
            }

            ImGui.Begin(_name, _imGuiWindowFlags);
        }

        public virtual void EndRender()
        {
            ImGui.End();
        }

        public void OnToggle()
        {
            Toggle?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
