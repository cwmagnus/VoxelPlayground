using System.Collections.Generic;
using UnityEngine;
using WorldGeneration.Blocks;

namespace Management.Registry
{
    public class BlockRegistry
    {
        private ushort nextBlockIndex;
        private List<Block> blockRegistry = new List<Block>();

        public void RegisterBlock(Block block)
        {
            blockRegistry.Add(block);
            block.ID = nextBlockIndex;

            nextBlockIndex++;
        }

        public Block GetBlock(ushort id)
        {
            return blockRegistry[id];
        }

        public ushort? GetBlockID(string blockName)
        {
            foreach (Block block in blockRegistry)
            {
                if (block.Name.Equals(blockName))
                {
                    return block.ID;
                }
            }
            return null;
        }

        public ushort GetNextBlockIndex()
        {
            return nextBlockIndex;
        }
    }
}
