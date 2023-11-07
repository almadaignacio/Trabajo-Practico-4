using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] private GameObject objectPrefab;
    public List<GameObject> objectList;
    [SerializeField] private int objectSize;
    public static ObjectPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateObjectPrefab(objectSize);
    }

    public void GenerateObjectPrefab(int amount)
    {
        for (int i = 0; i < objectSize; i++)
        {
            GameObject newObstacle = Instantiate(objectPrefab);
            newObstacle.SetActive(false);
            objectList.Add(newObstacle);
            newObstacle.transform.parent = transform;
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (!objectList[i].activeSelf)
            {
                objectList[i].SetActive(true);
                return objectList[i];
            }
        }
        GenerateObjectPrefab(1);
        objectList[objectList.Count - 1].SetActive(true);
        return objectList[objectList.Count - 1];
    }
}
