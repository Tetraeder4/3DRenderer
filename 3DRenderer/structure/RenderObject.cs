namespace _3DRenderer.structure;

public class RenderObject
{
    public Mesh SharedMesh { get; set; }
    
    //transform
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero; // pitch yaw roll
    public Vector3 Scale { get; set; } = Vector3.One;

    public RenderObject(Mesh mesh)
    {
        SharedMesh = mesh;
    }
    
    public Matrix4x4 GetModelMatrix()
    {
        return Matrix4x4.CreateScale(Scale) *
               Matrix4x4.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z) *
               Matrix4x4.CreateTranslation(Position);
    }
}