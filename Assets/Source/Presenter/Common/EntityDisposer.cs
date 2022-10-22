using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EntityDisposer : MonoBehaviour
{
    private List<IDisposable> _models;


    [Inject]
    public void Init(List<IDisposable> models)
    {
        _models = models;
    }


    private void OnDestroy()
    {
        _models.ForEach(x => x.Dispose());
    }
}
