using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Management;

namespace WorldGeneration
{
    public class Chunk : MonoBehaviour
    {
        private Mesh mesh;
        private MeshCollider meshCollider;
        private Renderer meshRenderer;

        private MeshGenerator marchingCubes = new MeshGenerator(0);
        private List<Vector3> vertices = new List<Vector3>();
        private List<int> triangles = new List<int>();
        List<Vector2> uv = new List<Vector2>();
        private List<ushort> meshedBlockIDs = new List<ushort>();

        private bool needsUpdated;

        public World World { get; set; }
        public Vector3Int ChunkSize { get; set; }
        public Vector3Int ChunkPosition { get; set; }
        public float Scale { get; set; }

        private void Awake()
        {
            meshRenderer = GetComponent<Renderer>();
            mesh = GetComponent<MeshFilter>().mesh;
            meshCollider = GetComponent<MeshCollider>();
        }

        private void Start()
        {
            //meshRenderer.material.mainTexture = GameManager.Instance.BlockTexturePack.Atlas;
        }

        public void Generate()
        {
            ThreadPool.QueueUserWorkItem(callback => GenerateProc());
        }

        public float GetBlock(int x, int y, int z)
        {
            return World.GetBlock(x + ChunkPosition.x, y + ChunkPosition.y, z + ChunkPosition.z);
        }

        private void LateUpdate()
        {
            if (needsUpdated)
            {
                UpdateMesh();
                needsUpdated = false;
            }
        }

        private void UpdateMesh()
        {
            mesh.Clear();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            //mesh.uv = uv.ToArray();
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            meshCollider.sharedMesh = mesh;

            vertices.Clear();
            triangles.Clear();
            //uv.Clear();
        }

        private void GenerateProc()
        {
            marchingCubes.Generate(this, vertices, triangles);

            needsUpdated = true;
        }
    }
}
