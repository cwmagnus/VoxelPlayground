using WorldGeneration.Blocks;
using Core.Items;

namespace Core.Blocks
{
    public class StoneBlock : Block
    {
        public StoneBlock() : base(new StoneBlockItem(), "stone")
        {
            Name = "Stone";
            BlockType = BlockType.Opaque;
            Hardness = 5;
        }
    }
}
