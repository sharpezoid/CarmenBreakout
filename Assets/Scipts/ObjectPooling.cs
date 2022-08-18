using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public int InitialCount = 50;
    public bool SpawnMoreIfNeeded = false;

    List<GameObject> Pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < InitialCount; i++)
        {
            SpawnObject();
        }
    }

    GameObject SpawnObject()
    {
        GameObject newObj = GameObject.Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
        newObj.GetComponent<PooledObject>().SetOriginPool(this);
        Pool.Add(newObj);
        newObj.SetActive(false);
        return newObj;
    }

    public GameObject GetItem()
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            if (!Pool[i].activeSelf)
            {
                Pool[i].SetActive(true);
                return Pool[i];
            }
        }

        if (SpawnMoreIfNeeded)
        {
            GameObject retObj = SpawnObject();
            retObj.SetActive(true);
            return retObj;
        }

        return null;
    }

    public bool ReturnItem(GameObject _return)
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            if (Pool[i] == _return)
            {
                Pool[i].SetActive(false);
                return true;
            }
        }

        return false;
    }
}
