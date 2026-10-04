namespace _3DRenderer.structure;

  public class RenderTarget<T> (int w, int h)
{
    public T?[,] Buffer =  Initialize(w,h);
    public int Width = w;
    public int Height = h;
    public Vector2 Size = new Vector2(w, h);

    private static T?[,] Initialize(int w, int h)
    {
        if (w <= 0 || h <= 0) 
            throw new ArgumentOutOfRangeException();
        
        T?[,] grid = new T?[w, h];
        for(var y=0; y<h; y++)
        for(var x=0; x<w; x++)
            grid[x, y] = default;
        return grid;
    }
    
    public void Resize(int w, int h)
    {
        if (w <= 0 || h <= 0) 
            throw new ArgumentOutOfRangeException();
        
        int copyWidth = Math.Min(w, Width);
        int copyHeight = Math.Min(h, Height);
        
        T?[,] grid = new T?[w, h];
        
        for(var y=0; y < copyHeight; y++)
        for (var x = 0; x < copyWidth; x++)
        {
                grid[x, y] = Buffer[x, y];
        }
        
        Buffer = grid;
        Width = w;
        Height = h;
        Size = new Vector2(w, h);
    }
}  



