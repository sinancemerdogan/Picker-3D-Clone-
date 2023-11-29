using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(menuName = "Phase Config", fileName = "New Phase Config")]
public class PhaseConfigSO : ScriptableObject
{
    [SerializeField] private int numberOfObjectsToComplete;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private List<Vector3>objectPositionsList;


    public int GetNumberOfObjects() {
        return objectPositionsList.Count;
    }

    public GameObject GetObjectPrefab() {
        return objectPrefab;
    }

    public List<Vector3> GetObjectPositionsList() {
        return objectPositionsList;
    }

    public Vector3 GetObjectPositionWithIndex(int index) {
        return objectPositionsList[index];
    }

    public int GetNumberOfObjectToComplete() {
        return numberOfObjectsToComplete;
    }
}
