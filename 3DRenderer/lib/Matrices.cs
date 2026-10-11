namespace _3DRenderer.lib;

public static class Matrices
{
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
 }