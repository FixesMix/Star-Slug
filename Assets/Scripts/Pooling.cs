using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{

    public static Pooling instance;

    private List<GameObject> pooledObj = new List<GameObject>();
    private int poolAmt = 20;

    [SerializeField] private GameObject bulletPrefab;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolAmt; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledObj.Add(obj);
        }
        
    }

    // Update is called once per frame
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObj.Count; i++)
        {
            if (!pooledObj[i].activeInHierarchy)
                return pooledObj[i];
        }
        return null;
    }


    //multiple object pooling classes per bullet type
}
