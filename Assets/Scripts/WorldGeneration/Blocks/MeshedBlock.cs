using UnityEngine;

namespace WorldGeneration.Blocks
{
    public abstract class MeshedBlock : Block
    {
        public MeshedBlock(string name, string textureName) : base(name, textureName, BlockType.Meshed)
        {

        }
    }
}
