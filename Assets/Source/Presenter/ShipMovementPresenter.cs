using UnityEngine;
using Splatrika.Asteroids.Model;
using Zenject;

public class ShipMovementPresenter : MonoBehaviour
{
    private IShipMovement _movement;


    [Inject]
    public void Init(
        IShipMovement movement)
    {
        _movement = movement;
    }


    private void Update()
    {
        if (Input.GetButtonDown("Left"))
        {
            _movement.StartRotation(-1);
        }
        if (Input.GetButtonDown("Right"))
        {
            _movement.StartRotation(1);
        }
        if (Input.GetButtonUp("Left") || Input.GetButtonUp("Right"))
        {
            _movement.StopRotation();
        }
        if (Input.GetButtonDown("Jump"))
        {
            _movement.StartMovement();
        }
        if (Input.GetButtonUp("Jump"))
        {
            _movement.StopMovement();
        }
    }
}
