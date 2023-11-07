using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCreator : MonoBehaviour
{
    [SerializeField] private ObjectFactory objectFactory;

    [SerializeField] private Button rockButton;
    [SerializeField] private Button bushButton;
    [SerializeField] private Button treeButton;

    public Transform spawn;

    private void Awake()
    {
        rockButton.onClick.AddListener(() =>
        {
            objectFactory.CreateObject("Rock", spawn);
        });

        bushButton.onClick.AddListener(() =>
        {
            objectFactory.CreateObject("Bush", spawn);
        });

        treeButton.onClick.AddListener(() =>
        {
            objectFactory.CreateObject("Tree", spawn);
        });
    }
}
