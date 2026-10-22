namespace _3DRenderer.structure;

public class Texture
{
    public float Width;
    public float Height;

    public Vector4[] Data;

    public Texture(float height, float width)
    {
        this.Height = height;
        this.Width = width;
    }
}