using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private Vector3Int worldSize;
    private ushort[,,] blockData;

    private void Awake()
    {
        blockData = new ushort[worldSize.x, worldSize.y, worldSize.z];

        for (int x = 0; x < worldSize.x; x++)
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                for (int z = 0; z < worldSize.z; z++)
                {
                    if (x < 4 && y == 8)
                    {
                        blockData[x, y, z] = 2;
                    }
                    else if (y < worldSize.y / 2)
                    {
                        blockData[x, y, z] = 1;
                    }
                    else
                    {
                        blockData[x, y, z] = 0;
                    }
                }
            }
        }
    }

    public ushort GetBlock(int x, int y, int z)
    {
        if (!IsValidPoint(x, y, z))
        {
            return 0;
        }

        return blockData[x, y, z];
    }

    public bool IsValidPoint(int x, int y, int z)
    {
        return !(x >= worldSize.x || x < 0 || y >= worldSize.y || y < 0 || z >= worldSize.z || z < 0);
    }
}