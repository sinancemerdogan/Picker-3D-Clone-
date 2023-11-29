using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour {
    private List<T> pooledObjects = new List<T>();
    private T objectPrefab;
    private Transform parentTransform;

    public ObjectPool(T prefab, Transform parent) {
        objectPrefab = prefab;
        parentTransform = parent;
    }

    public T GetPooledObject() {
        T obj;

        if (pooledObjects.Count > 0) {
            obj = pooledObjects[0];
            pooledObjects.RemoveAt(0);
        }
        else {
            obj = Object.Instantiate(objectPrefab, parentTransform);
        }

        return obj;
    }

    public void ReturnToPool(T obj) {
        pooledObjects.Add(obj);
    }
}
