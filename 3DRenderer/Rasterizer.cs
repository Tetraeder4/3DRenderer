namespace _3DRenderer;

public static class Rasterizer
{
    public static Scene PrepareScene(Scene scene)
    {
        Matrix4x4 viewMatrix = scene.Camera.GetViewMatrix();
        Matrix4x4 projectionMatrix = scene.Camera.GetPerspectiveProjectionMatrix();
            
        foreach (RenderObject renderObject in scene.Objects)
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

        return scene;
    }

    public static Matrix4x4 ConstructMvpMatrix(Matrix4x4 modelMatrix, Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix)
    {
        return modelMatrix * viewMatrix * projectionMatrix;
    }

    public static Matrix4x4 ConstructNormalMatrix(Matrix4x4 modelMatrix, Matrix4x4 viewMatrix)
    {
        if (Matrix4x4.Invert(modelMatrix * viewMatrix, out Matrix4x4 invertedMatrix))
        {
            return Matrix4x4.Transpose(invertedMatrix);
        }

        Console.WriteLine("modelview matrix is singular and cannot be inverted to calculate normals");
        return Matrix4x4.Identity;
    }    
    public static bool PointInTriangle(Vector2 vertexA, Vector2 vertexB, Vector2 vertexC, Vector2 pointP)
    {
        // check if point is on the same side of every line
        float d1 = EdgeFunction(vertexA, vertexB, pointP);
        float d2 = EdgeFunction(vertexB, vertexC, pointP);
        float d3 = EdgeFunction(vertexC, vertexA, pointP);

        bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(hasNeg && hasPos);
    }
        
    public static float EdgeFunction(Vector2 a, Vector2 b, Vector2 p)
    {
        return (p.X - a.X) * (b.Y - a.Y) - (p.Y - a.Y) * (b.X - a.X); // =signed determinant of 2x2 mat
    }
}