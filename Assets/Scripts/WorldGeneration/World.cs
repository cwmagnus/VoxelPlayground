using System.Collections;
using UnityEngine;

namespace WorldGeneration
{
    public class World : MonoBehaviour
    {
        [SerializeField] private Vector2Int worldSize;
        [SerializeField] private Vector3Int chunkSize;
        private ChunkPool chunkPool;
        private Chunk[,] chunks;

        public float[,,] BlockData { get; set; }
        public Vector3Int ChunkSize { get => chunkSize; }
        public Chunk[,] Chunks { get => chunks; }

        private void Awake()
        {
            chunkPool = GetComponent<ChunkPool>();

            BlockData = new float[worldSize.x, chunkSize.y, worldSize.y];
            chunks = new Chunk[Mathf.FloorToInt(worldSize.x / chunkSize.x),
                Mathf.FloorToInt(worldSize.y / chunkSize.z)];

            for (int x = 0; x < worldSize.x; x++)
            {
                for (int y = 0; y < chunkSize.y; y++)
                {
                    for (int z = 0; z < worldSize.y; z++)
                    {
                        if (y == 16 && x == 8 && z == 8)
                        {
                            BlockData[x, y, z] = .1f;
                        }
                        else if (y < 16)
                        {
                            BlockData[x, y, z] = .1f;
                        }
                        else
                        {
                            BlockData[x, y, z] = -.1f;
                        }
                    }
                }
            }
        }

        public IEnumerator LoadChunks(Vector3 playerPosition, float loadDistance)
        {
            for (int x = 0; x < chunks.GetLength(0); x++)
            {
                for (int z = 0; z < chunks.GetLength(1); z++)
                {
                    float distance = Vector2.Distance(new Vector2(x * chunkSize.x, z * chunkSize.z),
                        new Vector2(playerPosition.x, playerPosition.z));

                    if (distance < loadDistance)
                    {
                        if (chunks[x, z] == null)
                        {
                            LoadChunkColumn(x, z);
                        }
                    }
                    else if (distance > loadDistance)
                    {
                        if (chunks[x, z] != null)
                        {
                            UnloadChunkColumn(x, z);
                        }
                    }
                }

                yield return null;
            }
        }

        public void LoadChunkColumn(int x, int z)
        {
            Vector3Int chunkPosition = new Vector3Int(x * chunkSize.x, 0, z * chunkSize.z);

            GameObject chunkObject = chunkPool.GetAvailableChunkObject();

            if (chunkObject != null)
            {
                chunkObject.name = "PooledChunk";
                chunkObject.SetActive(true);
            }
            else
            {
                chunkObject = Instantiate(chunkPool.ChunkPrefab);
                chunkObject.name = "InstanceChunk";
                chunkObject.transform.parent = transform;
            }

            chunkObject.transform.position = chunkPosition;

            chunks[x, z] = chunkObject.GetComponent<Chunk>();
            chunks[x, z].World = this;
            chunks[x, z].ChunkSize = chunkSize;
            chunks[x, z].ChunkPosition = chunkPosition;
            chunks[x, z].Generate();
        }

        public void UnloadChunkColumn(int x, int z)
        {
            if (chunks[x, z].gameObject.name.Equals("PooledChunk"))
            {
                chunkPool.DisableChunkObject(chunks[x, z].gameObject);
                chunks[x, z] = null;
            }
            else
            {
                Destroy(chunks[x, z].gameObject);
            }
        }

        public float GetBlock(int x, int y, int z)
        {
            if (!IsValidPoint(x, y, z))
            {
                return -1;
            }

            return BlockData[x, y, z];
        }

        public bool IsValidPoint(int x, int y, int z)
        {
            return !(x >= worldSize.x || x < 0 || y >= chunkSize.y || y < 0 || z >= worldSize.y || z < 0);
        }
    }
}
