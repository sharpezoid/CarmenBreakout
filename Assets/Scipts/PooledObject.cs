using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    ObjectPooling OriginPool;

    public void SetOriginPool(ObjectPooling _pool)
    {
        OriginPool = _pool;
    }
}
