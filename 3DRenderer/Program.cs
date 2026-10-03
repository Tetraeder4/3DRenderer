namespace _3DRenderer
{
    
}
class Program
{
    static void Main()
    {

        var target = new RenderTarget<Vector3>(800, 600);
        
        for (int y = 0; y < target.Height; y++)
        {
            for (int x = 0; x < target.Width; x++)
            {
                target.Buffer[x, y] = new Vector3(
                    (float)x / target.Width,
                    (float)y / target.Height,
                    0.5f
                );
            }
        }

        //launch viewer
        var viewer = new RenderTargetViewer<Vector3>(target);
        viewer.Run();
    }
}