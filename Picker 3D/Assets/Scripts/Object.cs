using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private Vector3 location;
    private GameObject prefab;

    public void SetObject(Vector3 location, GameObject prefab) {
        this.location = location;
        this.prefab = prefab;
    }
}
