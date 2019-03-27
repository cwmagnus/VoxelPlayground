using Management;
using Items;

namespace WorldGeneration.Blocks
{
    public abstract class Block
    {
        public string Name { get; protected set; }
        public BlockType BlockType { get; protected set; }
        public byte Hardness { get; protected set; }

        public Block(Item inventoryItem)
        {
            GameManager.Instance.ItemRegistry.Register(inventoryItem);
        }

        public Block(Item inventoryItem, string texturePath)
        {
            GameManager.Instance.ItemRegistry.Register(inventoryItem);
            GameManager.Instance.BlockTexturePack.AddTexture($"Textures/Blocks/{texturePath}");
        }
    }
}
