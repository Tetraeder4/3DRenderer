using System.Collections.Specialized;
using Mesh = _3DRenderer.structure.Mesh;

namespace _3DRenderer;

public static class RenderObjectBuilder
{
    public static RenderObject Build(string filename)
    {
        return Build(filename, Vector3.Zero, Vector3.Zero , Vector3.One);
    }
    
    public static RenderObject Build(string filename, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        string objString = lib.Directory.GetTestFileContentFromName(filename);
        RenderObject renderObject = new RenderObject(CreateMeshFromObjString(objString));
        
        renderObject.Position = position;
        renderObject.Rotation = rotation;
        renderObject.Scale = scale;
        
        return renderObject;
    }


    
    public static Mesh CreateMeshFromObjString(string objString)
    {
        List<Vector3> allPositions = new();
        List<Vector3> allNormals = new();
        List<Vector2> allTextureCoordinates = new();
        
        List<Vertex> vertices = new();
        List<int> triangleIndices = new();
        
        Dictionary<(int position, int texCoord, int normal), int> vertexMap = new();
        

        foreach (string lineRaw in SplitByLine(objString))
        {
            string line = lineRaw.Trim();
            if (line.StartsWith("v "))
            {
                string[] components = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                // check for . or , as a decimal separator
                allPositions.Add(new Vector3(
                    float.Parse(components[1], System.Globalization.CultureInfo.InvariantCulture),
                    float.Parse(components[2], System.Globalization.CultureInfo.InvariantCulture),
                    float.Parse(components[3], System.Globalization.CultureInfo.InvariantCulture)
                ));
            }
            
            else if (line.StartsWith("vn "))
            {
                string[] components = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                allNormals.Add(new Vector3(
                    float.Parse(components[1], System.Globalization.CultureInfo.InvariantCulture),
                    float.Parse(components[2], System.Globalization.CultureInfo.InvariantCulture),
                    float.Parse(components[3], System.Globalization.CultureInfo.InvariantCulture)
                ));
            }
            
            else if (line.StartsWith("vt "))
            {
                string[] components = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                allTextureCoordinates.Add(new Vector2(
                    float.Parse(components[1], System.Globalization.CultureInfo.InvariantCulture),
                    float.Parse(components[2], System.Globalization.CultureInfo.InvariantCulture)
                ));
            }
            
            else if (line.StartsWith("f "))
            {
                string[] faceIndexGroups = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                List<int> faceIndices = new();
                
                for (int i = 1; i < faceIndexGroups.Length; i++)
                {
                    string[] vertexGroup = faceIndexGroups[i].Split('/');
                    
                    int positionIndex = int.Parse(vertexGroup[0]) - 1;
                    int texCoordIndex = vertexGroup.Length > 1 && vertexGroup[1] != "" ? int.Parse(vertexGroup[1]) - 1 : -1;
                    int normalIndex = vertexGroup.Length > 2 && vertexGroup[2] != "" ? int.Parse(vertexGroup[2]) - 1 : -1;

                    var key = (positionIndex, texCoordIndex, normalIndex);

                    if (!vertexMap.TryGetValue(key, out int index)) //add if new vertex
                    {
                        index = vertices.Count;

                        vertices.Add(new Vertex(
                            allPositions[positionIndex],
                            normalIndex >= 0 ? allNormals[normalIndex] : null,
                            texCoordIndex >= 0 ? allTextureCoordinates[texCoordIndex] : null
                        ));

                        vertexMap.Add(key, index);
                    }

                    faceIndices.Add(index);
                }
                
                // triangulate polygon with triangle fan
                for (int i = 1; i < faceIndices.Count - 1; i++)
                {
                    triangleIndices.Add(faceIndices[0]);
                    triangleIndices.Add(faceIndices[i]);
                    triangleIndices.Add(faceIndices[i + 1]);
                }
            }
        }
        return new Mesh(vertices.ToArray(), triangleIndices.ToArray());
    }

    private static string[] SplitByLine(string input)
     {
         return input.Split(["\r\n", "\n"], StringSplitOptions.None);
     }

    
}