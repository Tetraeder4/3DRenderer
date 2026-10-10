namespace _3DRenderer.lib;

public static class Globals
{
    public static Scene MainScene = new Scene();
    public static RenderTarget<Vector3> MainRenderTarget = new RenderTarget<Vector3>(200, 100);
}