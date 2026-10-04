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
}