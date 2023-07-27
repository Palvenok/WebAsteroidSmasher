using System.Collections;
using UnityEngine;

public sealed class ObjectPool<T> : IEnumerable where T : UnityEngine.Object
{
    private T[] _pool;
    private int _count;
    private int _currentIndex = -1;
    private GameObject _poolContainer;

    public int Count => _count;
    public GameObject ContainerObject => _poolContainer;
    public T Current
    {
        get { return _pool[_currentIndex < 0 ? 0 : _currentIndex]; }
    }

    public IEnumerator GetEnumerator() => new ObjectIterator(this);

    /// <summary>
    /// Create pool container as parrent and instantiate items by count
    /// </summary>
    /// <param name="poolName">Default name: ObjectPoolContainer</param>
    public ObjectPool(T objectPrefab, int count, string poolName = "PoolContainer")
    {
        _count = count;
        _pool = new T[count];

        _poolContainer = new GameObject();
        ///TODO: fix custom pool name
        _poolContainer.name = objectPrefab.name + poolName;

        Init(objectPrefab, _poolContainer.transform);

    }

    /// <summary>
    /// Instantiate items by count, use specified Transform as parrent
    /// </summary>
    public ObjectPool(T objectPrefab, int count, Transform parent)
    {
        _count = count;
        _pool = new T[count];
        _poolContainer = parent.gameObject;

        Init(objectPrefab, parent);

    }

    private void Init(T objectPrefab, Transform parent)
    {
        _currentIndex = -1;
        for (int i = 0; i < _count; i++)
        {
            _pool[i] = Object.Instantiate(objectPrefab, parent);
        }
    }

    /// <summary>
    /// Return item by id and set as current item <br/>
    /// id &gt; pool.Count : return last item <br/>
    /// id &lt; 0 : return first (id: 0) item
    /// </summary>
    public T GetByIndex(int index)
    {
        index = index < 0 ? -1 : index >= _count ? _count - 1 : index;
        _currentIndex = index;

        return Current;
    }

    /// <summary>
    /// Return next item <br/>
    /// Cyclically
    /// </summary>
    public T GetNext()
    {
        _currentIndex++;
        if (_currentIndex >= _count) _currentIndex = 0;
        return Current;
    }

    /// <summary>
    /// Clear pool, destroy pool container
    /// </summary>
    public void Clear()
    {
        if (_poolContainer)
            Object.Destroy(_poolContainer);
        _pool = null;
        _count = 0;
        _currentIndex = -1;
    }

    private class ObjectIterator : IEnumerator
    {
        private ObjectPool<T> _objectPool;
        private int _currentIndex = -1;

        public ObjectIterator(ObjectPool<T> objectPool)
        {
            _objectPool = objectPool;
        }

        public object Current => _objectPool._pool[_currentIndex];

        public bool MoveNext()
        {
            _currentIndex++;
            if (_currentIndex < _objectPool._count) return true;
            else return false;
        }

        public void Reset()
        {
            _currentIndex = -1;
        }
    }
}
