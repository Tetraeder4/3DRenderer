namespace _3DRenderer.structure;

public class Scene
{
    public Camera Camera { get; set; }
    public RenderObject[] Objects { get; set; }

    public Scene()
    {
        Camera = new Camera();
        Objects = [];
    }

    public void Update(float deltaTime)
    {
        Camera.Update(deltaTime);

        foreach (RenderObject renderObject in Objects)
            renderObject.Update(deltaTime);
    }
    public Scene TransformSceneToClipSpace()
    {
        Matrix4x4 viewMatrix = Camera.GetViewMatrix();
        Matrix4x4 projectionMatrix = Camera.GetPerspectiveProjectionMatrix();
            
        foreach (RenderObject renderObject in Objects)
        {
            Matrix4x4 modelMatrix = renderObject.GetModelMatrix();
            Matrix4x4 mvpMatrix = ConstructMvpMatrix(modelMatrix, viewMatrix, projectionMatrix);
            Matrix4x4 normalMatrix = ConstructNormalMatrix(modelMatrix, viewMatrix);
            
            var vertices = renderObject.SharedMesh.Vertices;
            
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 clipPosition = Vector3.Transform(vertices[i].Position, mvpMatrix);
                
                Vector3? clipNormal = vertices[i].Normal != null 
                    ? Vector3.TransformNormal(vertices[i].Normal!.Value, normalMatrix) 
                    : null;
                
                vertices[i] = new Vertex(clipPosition, clipNormal, vertices[i].TexCoord);
            }
        }
        return this;
    }
    
    private Matrix4x4 ConstructMvpMatrix(Matrix4x4 modelMatrix, Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix)
    {
        return modelMatrix * viewMatrix * projectionMatrix;
    }

    private Matrix4x4 ConstructNormalMatrix(Matrix4x4 modelMatrix, Matrix4x4 viewMatrix)
    {
        if (Matrix4x4.Invert(modelMatrix * viewMatrix, out Matrix4x4 invertedMatrix))
        {
            return Matrix4x4.Transpose(invertedMatrix);
        }

        Console.WriteLine("modelview matrix is singular and cannot be inverted to calculate normals");
        return Matrix4x4.Identity;
    }    
}