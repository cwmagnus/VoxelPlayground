using UnityEngine;
using Management.Registry;
using WorldGeneration.Blocks;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TexturePacker texturePacker;

        public static GameManager Instance { get; private set; }
        public BlockRegistry BlockRegistry { get; private set; } = new BlockRegistry();
        public TexturePacker TexturePacker { get => texturePacker; }

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
            BlockRegistry.RegisterBlock(new Block("Air"));
            BlockRegistry.RegisterBlock(new Block("Dirt", "dirt", BlockType.Opaque));
            BlockRegistry.RegisterBlock(new Block("Dirt2", "blue", BlockType.Opaque));
            TexturePacker.GenerateTextureAtlas();
        }
    }
}
