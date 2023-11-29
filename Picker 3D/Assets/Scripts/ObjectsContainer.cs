using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsContainer : MonoBehaviour
{
    public void InstantiateObjects(GameObject levelObjectPrefab, List<Vector3> objectPositions) {

        for (int i = 0; i < objectPositions.Count; i++) {
            Vector3 objectPosition = objectPositions[i];
            GameObject instantiatedObject = Instantiate(levelObjectPrefab, transform);
            instantiatedObject.transform.localPosition = objectPosition;
        }
    }

    public void ResetObjectContainer() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
}
