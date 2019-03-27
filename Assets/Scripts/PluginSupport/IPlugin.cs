using Management.Registry;
using WorldGeneration.Blocks;
using Items;

namespace PluginSupport
{
    public interface IPlugin
    {
        void RegisterBlocks(Registry<Block> blockRegistry);
        void RegisterItems(Registry<Item> itemRegistry);
    }
}
