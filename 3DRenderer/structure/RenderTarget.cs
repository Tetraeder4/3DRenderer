namespace _3DRenderer.structure;

  public class RenderTarget<T> (int w, int h)
{
    public T?[,] ColorBuffer =  InitializeColor(w,h);
    public float[,] DepthBuffer = InitializeDepth(w,h);
    public int Width = w;
    public int Height = h;
    public Vector2 Size = new Vector2(w, h);

    private static T?[,] InitializeColor(int w, int h)
    {
        if (w <= 0 || h <= 0) 
            throw new ArgumentOutOfRangeException();
        
        T?[,] grid = new T?[w, h];
        for(var y=0; y<h; y++)
        for(var x=0; x<w; x++)
            grid[x, y] = default;
        return grid;
    }
    
    private static float[,] InitializeDepth(int w, int h)
    {
        if (w <= 0 || h <= 0)
            throw new ArgumentOutOfRangeException();

        float[,] depth = new float[w, h];

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                depth[x, y] = float.PositiveInfinity;
            }
        }

        return depth;
    }
    
    public void Resize(int w, int h)
    {
        if (w <= 0 || h <= 0)
            throw new ArgumentOutOfRangeException();

        ColorBuffer = new T?[w, h];
        DepthBuffer = new float[w, h];
        Width = w;
        Height = h;
        Size = new Vector2(w, h);
    }
    
    public void Clear(T? value = default)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                ColorBuffer[x, y] = value;
                DepthBuffer[x, y] = float.PositiveInfinity;

            }
        }
    }
}  



