namespace _3DRenderer.structure;

public class Camera
{
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }
    public float FieldOfView { get; set; }
    
    public Camera()
    {
        Position = Vector3.Zero;
        Rotation = Vector3.Zero;
        
        FieldOfView = MathF.PI / 4;
    }

    public Matrix4x4 GetViewMatrix()
    {
        Matrix4x4 rotationMatrix = Matrix4x4.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);
        Vector3 forward = Vector3.Transform(Vector3.UnitZ, rotationMatrix); 

        return Matrix4x4.CreateLookAt(Position, Position + forward, Vector3.UnitY);
    }
}