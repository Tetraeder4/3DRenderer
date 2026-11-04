namespace _3DRenderer.shading;

public class RandomDepthColorShader : Shader
{
    public override Vector3 FragmentColor(Vector2 pixelCoord, Vector2? texCoord, Vector3? normal, float depth, int triangleIndex)
    {
        return Debug.GetRandomColor((int) (depth * 5000.0f));
    }
}