using UnityEngine;
using WorldGeneration;

public class ModifyTerrain : MonoBehaviour
{
    private World world;

    private void Start()
    {
        world = FindObjectOfType<World>();
    }

    public void ReplaceBlockCenter(float range, float id)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.distance < range)
            {
                ReplaceBlockAt(hit, id);
            }
        }
    }

    public void AddBlockCenter(float range, float id)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.distance < range)
            {
                AddBlockAt(hit, id);
            }
            Debug.DrawLine(ray.origin, ray.origin + (ray.direction * hit.distance), Color.green, 2);
        }
    }

    public void ReplaceBlockCursor(float id)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            ReplaceBlockAt(hit, id);
            Debug.DrawLine(ray.origin, ray.origin + (ray.direction * hit.distance),
             Color.green, 2);

        }
    }

    public void AddBlockCursor(float id)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            AddBlockAt(hit, id);
            Debug.DrawLine(ray.origin, ray.origin + (ray.direction * hit.distance),
             Color.green, 2);
        }

    }

    public void ReplaceBlockAt(RaycastHit hit, float id)
    {
        Vector3 position = hit.point;
        position += (hit.normal * -0.5f);

        SetBlockAt(position, id);
    }

    public void AddBlockAt(RaycastHit hit, float id)
    {
        Vector3 position = hit.point;
        position += (hit.normal * 0.5f);

        SetBlockAt(position, id);
    }

    public void SetBlockAt(Vector3 position, float id)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        int z = Mathf.RoundToInt(position.z);

        SetBlockAt(x, y, z, id);
    }

    public void SetBlockAt(int x, int y, int z, float id)
    {
        Debug.Log($"{x} {y} {z}");
        world.BlockData[x, y, z] = id;
        UpdateChunkAt(x, y, z);
    }

    public void UpdateChunkAt(int x, int y, int z)
    {
        int updateX = Mathf.FloorToInt(x / world.ChunkSize.x);
        int updateZ = Mathf.FloorToInt(z / world.ChunkSize.z);

        world.Chunks[updateX, updateZ].Generate();

        if (x - (world.ChunkSize.x * updateX) == 0 && updateX != 0)
        {
            world.Chunks[updateX - 1, updateZ].Generate();
        }

        if (x - (world.ChunkSize.x * updateX) == 15 && updateX != world.Chunks.GetLength(0) - 1)
        {
            world.Chunks[updateX + 1, updateZ].Generate();
        }

        if (z - (world.ChunkSize.z * updateZ) == 0 && updateZ != 0)
        {
            world.Chunks[updateX, updateZ - 1].Generate();
        }

        if (z - (world.ChunkSize.z * updateZ) == 15 && updateZ != world.Chunks.GetLength(1) - 1)
        {
            world.Chunks[updateX, updateZ + 1].Generate();
        }
    }
}
