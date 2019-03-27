using WorldGeneration.Blocks;
using Core.Items;

namespace Core.Blocks
{
    public class AirBlock : Block
    {
        public AirBlock() : base(new AirBlockItem())
        {
            Name = "Air";
            BlockType = BlockType.Transparent;
            Hardness = 0;
        }
    }
}
