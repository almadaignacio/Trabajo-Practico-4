using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    [SerializeField] private ObjectScript[] objectScript;
    private Dictionary<string, ObjectScript> objectsByName;

    private void Awake()
    {
        objectsByName = new Dictionary<string, ObjectScript>();
        foreach (var objectsScript in objectScript)
        {
            objectsByName.Add(objectsScript.objectName, objectsScript);
        }
    }

    public ObjectScript CreateObject(string objectName, Transform spawn)
    {
        if (objectsByName.TryGetValue(objectName, out ObjectScript objectPrefab))
        {
            ObjectScript objectInstance = Instantiate(objectPrefab, spawn.position, Quaternion.identity);
            return objectInstance;
        }
        else
        {
            Debug.LogWarning($"The object '{objectName}' doesn´t exist in the data base.");
            return null;
        }
    }
}
