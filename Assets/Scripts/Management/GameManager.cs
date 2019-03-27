using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Management.Registry;
using WorldGeneration.Blocks;
using Items;
using Core;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Registry<Block> BlockRegistry { get; private set; } = new Registry<Block>();
        public Registry<Item> ItemRegistry { get; private set; } = new Registry<Item>();
        public TexturePack BlockTexturePack { get; private set; } = new TexturePack();
        public TexturePack ItemTexturePack { get; private set; } = new TexturePack();

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            LoadResources();
            BlockTexturePack.GenerateTextureAtlas(2000, 5);
            ItemTexturePack.GenerateTextureAtlas(2000, 5);
            SceneManager.LoadScene(1);
        }

        private void LoadResources()
        {
            CorePlugin corePlugin = new CorePlugin();
            corePlugin.RegisterBlocks(BlockRegistry);
            corePlugin.RegisterItems(ItemRegistry);
        }
    }
}
