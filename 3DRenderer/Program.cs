using _3DRenderer.shading;

namespace _3DRenderer;

class Program
{
    static void Main()
    {
        // set up scene
        
        // cube1
        RenderObject testObject1 = RenderObjectBuilder.Build("assets/models/suzane.obj");
        testObject1.Position = new Vector3(0, 0, 4);
        testObject1.Shader = new MonochormeDepthShader();
        Globals.MainScene.Objects.Add(testObject1);
        
        testObject1.Update = deltaTime =>
        {
            testObject1.Rotation += new Vector3(deltaTime / 3, deltaTime, deltaTime / 2);
        };
        
        //launch viewer
        var viewer = new RenderTargetViewer<Vector3>(Globals.MainRenderTarget);
        
        viewer.Update = deltaTime =>
        {
            Globals.MainScene.Camera.AspectRatio = (float)Globals.MainRenderTarget.Width / Globals.MainRenderTarget.Height;
            
            Globals.MainScene.Update(deltaTime);
            Rasterizer.Render(Globals.MainScene, Globals.MainRenderTarget);
        };
        
        viewer.Run();
    }
}