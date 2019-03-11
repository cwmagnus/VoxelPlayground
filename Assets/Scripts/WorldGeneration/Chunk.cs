using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Management;
using WorldGeneration.Blocks;

namespace WorldGeneration
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private Vector3Int chunkSize;
        [SerializeField] private World World;

        private Mesh mesh;
        private MeshCollider meshCollider;
        private Renderer meshRenderer;

        private List<Vector3> vertices = new List<Vector3>();
        private List<int> indices = new List<int>();
        private List<Vector2> uv = new List<Vector2>();
        private int faceCount;

        private bool meshNeedsUpdated;

        //public World World { get; set; }

        private void Awake()
        {
            meshRenderer = GetComponent<Renderer>();
            mesh = GetComponent<MeshFilter>().mesh;
            meshCollider = GetComponent<MeshCollider>();
        }

        private void Start()
        {
            meshRenderer.material.mainTexture = GameManager.Instance.TexturePacker.LoadedTextureAtlas;
            GenerateMesh();
        }

        public void GenerateMesh()
        {
            ThreadPool.QueueUserWorkItem(callback => GenerateMeshThreadProc());
        }

        private void GenerateMeshThreadProc()
        {
            for (int x = 0; x < chunkSize.x; x++)
            {
                for (int y = 0; y < chunkSize.y; y++)
                {
                    for (int z = 0; z < chunkSize.z; z++)
                    {
                        if (GameManager.Instance.BlockRegistry.GetBlock(World.GetBlock(x, y, z)).BlockType == BlockType.Opaque)
                        {
                            if (GameManager.Instance.BlockRegistry.GetBlock(World.GetBlock(x, y + 1, z)).BlockType == BlockType.Transparent)
                            {
                                GenerateBlockFaceTop(x, y, z, World.GetBlock(x, y, z));
                            }

                            if (GameManager.Instance.BlockRegistry.GetBlock(World.GetBlock(x, y - 1, z)).BlockType == BlockType.Transparent)
                            {
                                GenerateBlockFaceBottom(x, y, z, World.GetBlock(x, y, z));
                            }

                            if (GameManager.Instance.BlockRegistry.GetBlock(World.GetBlock(x + 1, y, z)).BlockType == BlockType.Transparent)
                            {
                                GenerateBlockFaceEast(x, y, z, World.GetBlock(x, y, z));
                            }

                            if (GameManager.Instance.BlockRegistry.GetBlock(World.GetBlock(x - 1, y, z)).BlockType == BlockType.Transparent)
                            {
                                GenerateBlockFaceWest(x, y, z, World.GetBlock(x, y, z));
                            }

                            if (GameManager.Instance.BlockRegistry.GetBlock(World.GetBlock(x, y, z + 1)).BlockType == BlockType.Transparent)
                            {
                                GenerateBlockFaceNorth(x, y, z, World.GetBlock(x, y, z));
                            }

                            if (GameManager.Instance.BlockRegistry.GetBlock(World.GetBlock(x, y, z - 1)).BlockType == BlockType.Transparent)
                            {
                                GenerateBlockFaceSouth(x, y, z, World.GetBlock(x, y, z));
                            }
                        }
                    }
                }
            }

            meshNeedsUpdated = true;
        }

        private void Update()
        {
            if (meshNeedsUpdated)
            {
                UpdateMesh();
                meshNeedsUpdated = false;
            }
        }

        private void UpdateMesh()
        {
            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.uv = uv.ToArray();
            mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
            mesh.RecalculateBounds();

            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = mesh;

            vertices.Clear();
            indices.Clear();
            uv.Clear();

            faceCount = 0;
        }

        private void GenerateBlockFaceTop(int x, int y, int z, ushort voxelID)
        {
            vertices.Add(new Vector3(x, y, z + 1));
            vertices.Add(new Vector3(x + 1, y, z + 1));
            vertices.Add(new Vector3(x + 1, y, z));
            vertices.Add(new Vector3(x, y, z));

            GenerateFaceData(voxelID, BlockFaceDirection.Top);
        }

        private void GenerateBlockFaceBottom(int x, int y, int z, ushort voxelID)
        {
            vertices.Add(new Vector3(x, y - 1, z));
            vertices.Add(new Vector3(x + 1, y - 1, z));
            vertices.Add(new Vector3(x + 1, y - 1, z + 1));
            vertices.Add(new Vector3(x, y - 1, z + 1));

            GenerateFaceData(voxelID, BlockFaceDirection.Bottom);
        }

        private void GenerateBlockFaceNorth(int x, int y, int z, ushort voxelID)
        {
            vertices.Add(new Vector3(x + 1, y - 1, z + 1));
            vertices.Add(new Vector3(x + 1, y, z + 1));
            vertices.Add(new Vector3(x, y, z + 1));
            vertices.Add(new Vector3(x, y - 1, z + 1));

            GenerateFaceData(voxelID, BlockFaceDirection.North);
        }

        private void GenerateBlockFaceEast(int x, int y, int z, ushort voxelID)
        {
            vertices.Add(new Vector3(x + 1, y - 1, z));
            vertices.Add(new Vector3(x + 1, y, z));
            vertices.Add(new Vector3(x + 1, y, z + 1));
            vertices.Add(new Vector3(x + 1, y - 1, z + 1));

            GenerateFaceData(voxelID, BlockFaceDirection.East);
        }

        private void GenerateBlockFaceSouth(int x, int y, int z, ushort voxelID)
        {
            vertices.Add(new Vector3(x, y - 1, z));
            vertices.Add(new Vector3(x, y, z));
            vertices.Add(new Vector3(x + 1, y, z));
            vertices.Add(new Vector3(x + 1, y - 1, z));

            GenerateFaceData(voxelID, BlockFaceDirection.South);
        }

        private void GenerateBlockFaceWest(int x, int y, int z, ushort voxelID)
        {
            vertices.Add(new Vector3(x, y - 1, z + 1));
            vertices.Add(new Vector3(x, y, z + 1));
            vertices.Add(new Vector3(x, y, z));
            vertices.Add(new Vector3(x, y - 1, z));

            GenerateFaceData(voxelID, BlockFaceDirection.West);
        }

        private void GenerateFaceData(ushort voxelID, BlockFaceDirection voxelFaceDirection)
        {
            indices.Add(faceCount * 4);
            indices.Add(faceCount * 4 + 1);
            indices.Add(faceCount * 4 + 2);
            indices.Add(faceCount * 4 + 3);

            Rect textureRect = GameManager.Instance.TexturePacker.GetTextureCoordnate(voxelID);
            Debug.Log(textureRect);

            uv.Add(new Vector2(textureRect.x, textureRect.y));
            uv.Add(new Vector2(textureRect.x + textureRect.width, textureRect.y));
            uv.Add(new Vector2(textureRect.x + textureRect.width, textureRect.y + textureRect.height));
            uv.Add(new Vector2(textureRect.x, textureRect.y + textureRect.height));

            faceCount++;
        }
    }
}
