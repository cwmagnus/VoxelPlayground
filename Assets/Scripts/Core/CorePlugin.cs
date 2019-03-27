using PluginSupport;
using Management.Registry;
using WorldGeneration.Blocks;
using Items;
using Core.Blocks;

namespace Core
{
    public class CorePlugin : IPlugin
    {
        public void RegisterBlocks(Registry<Block> blockRegistry)
        {
            blockRegistry.Register(new AirBlock());
            blockRegistry.Register(new DirtBlock());
            blockRegistry.Register(new StoneBlock());
        }

        public void RegisterItems(Registry<Item> itemRegistry)
        {

        }
    }
}
