using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Createplane : MonoBehaviour
{
    public GameObject prefab;
    public GameObject pivot;
    public GameObject ebeneprefab;
    public List<GameObject> ebenen;
    public RawImage self;
    public List<RawImage> others;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AddPlane()
    {
        ebenen.Add(Instantiate(ebeneprefab));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
