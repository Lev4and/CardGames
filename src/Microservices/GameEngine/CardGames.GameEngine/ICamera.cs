using OpenTK.Mathematics;

namespace CardGames.GameEngine
{
    public interface ICamera
    {
        float AspectRatio { get; set; }

        float Pitch { get; set; }

        float Yaw { get; set; }

        float Fov { get; set; }

        Vector3 Position { get; set; }

        Vector3 Front { get; }

        Vector3 Up { get; }

        Vector3 Right { get; }

        Matrix4 GetViewMatrix();

        Matrix4 GetProjectionMatrix();
    }
}
