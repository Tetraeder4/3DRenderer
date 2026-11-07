using _3DRenderer.shading;

namespace _3DRenderer.lib;

public static class Globals
{
    public static Scene MainScene = new();
    public static RenderTarget<Vector3> MainRenderTarget = new(64, 64);
}