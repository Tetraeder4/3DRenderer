namespace _3DRenderer.shading;

public class MonochormeDepthShader : Shader
{
    public override Vector3 FragmentColor(Vector2 pixelCoord, Vector2? texCoord, Vector3? normal, float depth, int triangleIndex)
    {
        return new Vector3(depth);
    }
}