using System.Collections.Generic;
using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

public class EntityUpdater : MonoBehaviour
{
    private List<IUpdatable> _models;


    [Inject]
    public void Init(List<IUpdatable> models)
    {
        _models = models;
    }


    private void Update()
    {
        _models.ForEach(x => x.Update(Time.deltaTime));
    }
}
