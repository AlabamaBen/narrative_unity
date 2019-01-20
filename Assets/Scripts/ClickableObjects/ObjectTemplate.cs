using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectTemplate
{
    public List<GameObject> gameObjectList;
}

/*
 * Scriptable objects
using UnityEngine;

[CreateAssetMenu(fileName = "New Clickable Object")]
public class ObjectTemplate : ScriptableObject
{
    public GameObject obj;
}*/
