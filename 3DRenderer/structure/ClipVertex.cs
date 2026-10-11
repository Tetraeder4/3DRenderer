namespace _3DRenderer.structure;

public readonly record struct ClipVertex(
    Vector4 Position,
    Vector3? Normal,
    Vector2? TexCoord
);