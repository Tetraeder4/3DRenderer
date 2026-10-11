namespace _3DRenderer.structure;

public class Camera
{
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }
    private Vector3 CameraUp { get; set; }
    
    public float AspectRatio { get; set; }
    public float NearPlane { get; set; }
    public float FarPlane { get; set; }
    public float FieldOfView { get; set; }
    
    public float OrthographicHeight { get; set; }
    
    
    public Camera()
    {
        Position = Vector3.Zero;
        Rotation = Vector3.Zero;
        CameraUp = Vector3.UnitY;
        
        AspectRatio = 16.0f / 9.0f;
        NearPlane = 0.1f;
        FarPlane = 100.0f;
        
        OrthographicHeight = 5.0f;
        
        FieldOfView = MathF.PI / 4; //45deg in rad
    }

    public void Update(float deltaTime) {}

    public Matrix4x4 GetPerspectiveProjectionMatrix()
    {
        return Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearPlane, FarPlane);
    }

    public Matrix4x4 GetOrthographicProjectionMatrix()
    {
        float height = OrthographicHeight;
        float width = height * AspectRatio;

        return Matrix4x4.CreateOrthographic(width, height, NearPlane, FarPlane);
    }
    
    public Matrix4x4 GetViewMatrix()
    {
        Matrix4x4 rotationMatrix = Matrix4x4.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);
        Vector3 forward = Vector3.Transform(Vector3.UnitZ, rotationMatrix);
        Vector3 up = Vector3.Transform(CameraUp, rotationMatrix);
        
        return Matrix4x4.CreateLookAt(Position, Position + forward, up);
    }
}