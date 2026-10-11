namespace _3DRenderer.lib;

using _3DRenderer.structure;

public static class SceneTransforms
{
    public static ClipScene TransformToClipSpace(Scene scene)
    {
        Camera camera = scene.Camera;

        Matrix4x4 view = camera.GetViewMatrix();
        Matrix4x4 projection = camera.GetPerspectiveProjectionMatrix();

        ClipRenderObject[] transformedObjects =
            new ClipRenderObject[scene.Objects.Count];

        for (int objectIndex = 0;
             objectIndex < scene.Objects.Count;
             objectIndex++)
        {
            RenderObject renderObject = scene.Objects[objectIndex];

            ReadOnlySpan<Vertex> source = renderObject.SharedMesh.Vertices;
            ReadOnlySpan<int> triangleIndices = renderObject.SharedMesh.TriangleIndices;

            ClipVertex[] transformed = new ClipVertex[source.Length];

            Matrix4x4 model = renderObject.GetModelMatrix();
            Matrix4x4 mvp = Matrices.ConstructMvpMatrix(model, view, projection);
            Matrix4x4 normalMatrix = Matrices.ConstructNormalMatrix(model, view);

            for (int i = 0; i < source.Length; i++)
            {
                Vector4 position = Vector4.Transform(new Vector4(source[i].Position, 1.0f), mvp);

                Vector3? normal = source[i].Normal.HasValue
                    ? Vector3.TransformNormal(source[i].Normal!.Value, normalMatrix)
                    : null;

                transformed[i] = new ClipVertex(position, normal, source[i].TexCoord);
            }
            
            transformedObjects[objectIndex] = new ClipRenderObject(transformed, triangleIndices.ToArray());
        }
        
        return new ClipScene(transformedObjects);
    }
}