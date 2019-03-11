using Management;

namespace WorldGeneration.Blocks
{
    public class Block
    {
        public ushort ID { get; set; }
        public string Name { get; private set; }
        public BlockType BlockType { get; private set; }
        public ushort Hardness { get; private set; }

        public Block(string name, BlockType blockType = BlockType.Transparent, ushort hardness = 0)
        {
            Name = name;
            BlockType = blockType;
            Hardness = hardness;
        }

        public Block(string name, string textureName, BlockType blockType = BlockType.Transparent, ushort hardness = 0)
        {
            Name = name;
            BlockType = blockType;
            Hardness = hardness;
            GameManager.Instance.TexturePacker.AddTextureName(textureName);
        }
    }
}
