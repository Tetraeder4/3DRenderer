namespace _3DRenderer;

class Program
{
    static void Main()
    {
        // set up scene
        RenderObject testCube = lib.Debug.CreateCube();
        testCube.Position = new Vector3(0, 0, 3);
        Globals.MainScene.Objects.Add(testCube);
        
        

        //launch viewer
        var viewer = new RenderTargetViewer<Vector3>(Globals.MainRenderTarget);
        
        viewer.OnUpdate = deltaTime =>
        {
            Globals.MainScene.Camera.AspectRatio = (float)Globals.MainRenderTarget.Width / Globals.MainRenderTarget.Height;
            
            Globals.MainScene.Update(deltaTime);
            Rasterizer.Render(Globals.MainScene, Globals.MainRenderTarget);
        };
        
        viewer.Run();
    }
}