namespace _3DRenderer;

class Program
{
    static void Main()
    {
        // set up scene
        
        // cube1
        RenderObject testObject1 = RenderObjectBuilder.Build("assets/models/cube.obj");
        testObject1.Position = new Vector3(1, 1, 8);
        Globals.MainScene.Objects.Add(testObject1);
        
        testObject1.Update = deltaTime =>
        {
            testObject1.Rotation += new Vector3(deltaTime / 3, deltaTime, deltaTime / 2);
        };
        
        // cube2
        RenderObject testObject2 = RenderObjectBuilder.Build("assets/models/cube.obj");
        testObject2.Position = new Vector3(0, -0.5f, 8);
        Globals.MainScene.Objects.Add(testObject2);

        testObject2.Update = deltaTime =>
        {
            testObject2.Rotation -= new Vector3(deltaTime / 3, deltaTime, deltaTime / 2);
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