using _3DRenderer.shading;

namespace _3DRenderer.lib;

public static class Globals
{
    public static Scene MainScene = new Scene();
    public static RenderTarget<Vector3> MainRenderTarget = new RenderTarget<Vector3>(64, 64);
    public static RandomTriangleColorShader MainShader = new RandomTriangleColorShader();
}