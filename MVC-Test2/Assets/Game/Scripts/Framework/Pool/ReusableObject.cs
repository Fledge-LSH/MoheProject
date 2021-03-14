using System;
using UnityEngine;

public abstract class ReusableObject : MonoBehaviour, IReusable
{
    public void OnSpawn()
    {
        //throw new NotImplementedException();
    }

    public void UnOnspawn()
    {
        //throw new NotImplementedException();
    }
}
