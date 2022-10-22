using UnityEngine;
using Splatrika.Asteroids.Model;
using Zenject;

public class ShipMovementPresenter : MonoBehaviour
{
    private IShipMovement _movement;
    private IPosition _position;
    private IRotation _rotation;
    private Transform _transform;


    [Inject]
    public void Init(
        IShipMovement movement,
        IPosition position,
        IRotation rotation)
    {
        _movement = movement;
        _position = position;
        _rotation = rotation;

        _transform = GetComponent<Transform>();

        _rotation.Rotated += OnRotated;
        _position.Moved += OnMoved;
    }


    private void OnDestroy()
    {
        _rotation.Rotated -= OnRotated;
        _position.Moved -= OnMoved;
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


    private void OnMoved(Vector2 position)
    {
        _transform.position = (Vector3)position;
    }


    private void OnRotated(float rotation)
    {
        _transform.rotation = Quaternion.Euler(
            new Vector3(0, 0, rotation * Mathf.Rad2Deg * -1));
    }
}
