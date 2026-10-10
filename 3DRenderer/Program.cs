namespace _3DRenderer;

class Program
{
    static void Main()
    {
        // set up scene
        RenderObject testCube = lib.Debug.CreateCube();
        testCube.Position = new Vector3(0, 0, 5);
        Globals.MainScene.Objects = [testCube];
        
        

        //launch viewer
        var viewer = new RenderTargetViewer<Vector3>(Globals.MainRenderTarget);
        
        viewer.OnUpdate = deltaTime =>
        {
            Globals.MainScene.Update(deltaTime);
            Rasterizer.Render(Globals.MainScene, Globals.MainRenderTarget);
        };
        
        viewer.Run();
    }
}

/*
//checkerboard
var target = new RenderTarget<bool>(800, 600);
        
        for (int y = 0; y < target.Height; y++)
        {
            for (int x = 0; x < target.Width; x++)
            {
                target.Buffer[x, y] = (x + y) % 2 == 0;
            }
        }

        //launch viewer
        var viewer = new RenderTargetViewer<bool>(target);
        viewer.Run();
        
        
//hello triangle
var target = new RenderTarget<Vector3>(100, 100);
        
        Vector2 vertexA = new Vector2(0.5f * (target.Width - 1), 1f * (target.Height - 1));
        Vector2 vertexB = new Vector2(1f * (target.Width - 1), 0);
        Vector2 vertexC = new Vector2(0f * (target.Width - 1), 0);
        
        for (int y = 0; y < target.Height; y++)
        {
            for (int x = 0; x < target.Width; x++)
            {
                if (PointInTriangle(vertexA, vertexB, vertexC, new Vector2(x,y)))
                { 
                    target.Buffer[x, y] = new Vector3(
                    (float)x / target.Width,
                    (float)y / target.Height,
                    0.5f
                    );   
                }
                
            }
        }
        
        //launch viewer
        var viewer = new RenderTargetViewer<Vector3>(target);
        viewer.Run();
*/