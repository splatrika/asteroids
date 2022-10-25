using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public interface IAlienshipConfigurationProvider
    {
        AlienshipConfiguration Congiguration { get; }
    }
}
