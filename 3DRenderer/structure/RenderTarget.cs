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

        T?[,] grid = new T?[w, h];

        Buffer = grid;
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
                Buffer[x, y] = value;
            }
        }
    }
}  



