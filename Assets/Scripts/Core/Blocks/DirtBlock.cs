using WorldGeneration.Blocks;
using Core.Items;

namespace Core.Blocks
{
    public class DirtBlock : Block
    {
        public DirtBlock() : base(new DirtBlockItem(), "dirt")
        {
            Name = "Dirt";
            BlockType = BlockType.Opaque;
            Hardness = 3;
        }
    }
}
