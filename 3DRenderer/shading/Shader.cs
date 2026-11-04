namespace _3DRenderer.shading;

public abstract class Shader
{
    public abstract Vector3 FragmentColor(Vector2 pixelCoord, Vector2? texCoord, Vector3? normal, float depth, int triangleIndex);
}