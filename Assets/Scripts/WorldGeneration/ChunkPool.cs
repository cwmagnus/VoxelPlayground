using UnityEngine;

namespace WorldGeneration
{
    public class ChunkPool : MonoBehaviour
    {
        [SerializeField] private GameObject _chunkPrefab;
        [SerializeField] private int _poolSize;
        private GameObject[] _pool;

        public GameObject ChunkPrefab { get => _chunkPrefab; }

        private void Awake()
        {
            _pool = new GameObject[_poolSize];
        }

        private void Start()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                _pool[i] = Instantiate(_chunkPrefab);
                _pool[i].transform.parent = transform;
                _pool[i].SetActive(false);
            }
        }

        public void DisableChunkObject(GameObject poolItem)
        {
            poolItem.SetActive(false);
            poolItem.transform.position = Vector3.zero;
        }

        public GameObject GetAvailableChunkObject()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                if (_pool[i] != null)
                {
                    if (!_pool[i].activeSelf)
                        return _pool[i];
                }
            }
            return null;
        }
    }
}
