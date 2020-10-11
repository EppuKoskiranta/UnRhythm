using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh_mountains;

    float[,] noise_map;

    Vector3[] m_vertices;
    int[] m_triangles;
    Color[] m_colors;

    public int mountain_width = 100;
    public int mountain_length = 100;

    public int mountain_x_scale = 1;
    public int mountain_z_scale = 1;

    public float z_thresh = 100;

    public Gradient gradient;

    float min_terrain_height;
    float max_terrain_height;

    public float framerate;

    public float y_offset;
    public float z_offset;
    public float x_offset;
    public float height_amplitude;

    [Range(0, 6)]
    public int levelOfDetail;
    public float noiseScale;
    [Range(1, 20)]
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        mesh_mountains = new Mesh();
        GameObject.FindGameObjectWithTag("MeshMountains").GetComponent<MeshFilter>().mesh = mesh_mountains;

        noise_map = GenerateNoiseMap(mountain_width + 1, mountain_length + 1, seed, noiseScale, octaves, persistance, lacunarity, offset);

        CreateMountains();
        UpdateMeshes();
    }

    public void AddOffset(Vector2 add)
    {
        offset += add;
    }

    private void Update()
    {
        noise_map = GenerateNoiseMap(mountain_width + 1, mountain_length + 1, seed, noiseScale, octaves, persistance, lacunarity, offset);
        CreateMountains();
        UpdateMeshes();
    }

    void CreateMountains()
    {
        m_vertices = new Vector3[(mountain_width + 1) * (mountain_length + 1)];

        for (int i = 0, z = 0; z <= mountain_length; z++)
        {
            for (int x = 0; x <= mountain_width; x++)
            {
                float y = noise_map[x,z] * height_amplitude;
                m_vertices[i] = new Vector3(x * mountain_x_scale + x_offset, y + y_offset, z * mountain_z_scale + z_offset);

                if (y > max_terrain_height)
                {
                    max_terrain_height = y;
                }
                if (y < min_terrain_height)
                {
                    min_terrain_height = y;
                }

                i++;
            }
        }

        m_triangles = new int[mountain_width * mountain_length * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < mountain_length; z++)
        {
            for (int x = 0; x < mountain_width; x++)
            {
                m_triangles[tris + 0] = vert + 0;
                m_triangles[tris + 1] = vert + mountain_width + 1;
                m_triangles[tris + 2] = vert + 1;
                m_triangles[tris + 3] = vert + 1;
                m_triangles[tris + 4] = vert + mountain_width + 1;
                m_triangles[tris + 5] = vert + mountain_width + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        m_colors = new Color[m_vertices.Length];

        for (int i = 0, z = 0; z <= mountain_length; z++)
        {
            for (int x = 0; x <= mountain_width; x++)
            {
                float height = Mathf.InverseLerp(min_terrain_height, max_terrain_height, m_vertices[i].y);
                m_colors[i] = gradient.Evaluate(height);
                i++;
            }
        }

    }

    void UpdateMeshes()
    {
        mesh_mountains.Clear();
        mesh_mountains.vertices = m_vertices;
        mesh_mountains.triangles = m_triangles;
        mesh_mountains.colors = m_colors;
        mesh_mountains.RecalculateNormals();
    }


    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];


        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;


        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }

}
