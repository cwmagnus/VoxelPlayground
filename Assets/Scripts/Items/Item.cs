using Management;

namespace Items
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public Item() { }

        public Item(string texturePath)
        {
            GameManager.Instance.ItemTexturePack.AddTexture($"Textures/Items/{texturePath}");
        }
    }
}
