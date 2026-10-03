namespace _3DRenderer
{
    
}
class Program
{
    static void Main()
    {

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
        
        bool PointInTriangle(Vector2 vertexA, Vector2 vertexB, Vector2 vertexC, Vector2 pointP)
        {
            // check if every point is on the right side of every triangle
            float d1 = EdgeFunction(vertexA, vertexB, pointP);
            float d2 = EdgeFunction(vertexB, vertexC, pointP);
            float d3 = EdgeFunction(vertexC, vertexA, pointP);

            bool has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            bool has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }
        
        float EdgeFunction(Vector2 a, Vector2 b, Vector2 p)
        {
            return (p.X - a.X) * (b.Y - a.Y) - (p.Y - a.Y) * (b.X - a.X); // =signed determinant of 2x2 mat
        }

        //launch viewer
        var viewer = new RenderTargetViewer<Vector3>(target);
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
*/