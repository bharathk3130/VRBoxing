using UnityEngine;
using UnityEngine.InputSystem;

public class DuckingDetection : MonoBehaviour
{
    [SerializeField] Transform _vrCam;

    [SerializeField] float _duckThreshold = 0.5f;

    CapsuleCollider _capsuleCollider;
    
    Vector3 _initialHeadsetPosition;
    bool _headsetPositionMarked;
    bool _isDucking;

    void Start()
    {
        _capsuleCollider = _vrCam.GetComponent<CapsuleCollider>();
    }

    public void MarkHeadsetPosition(InputAction.CallbackContext context)
    {
        if ((context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Performed) && !_headsetPositionMarked)
        {
            _initialHeadsetPosition = _vrCam.position;
            _headsetPositionMarked = true;
        }
    }

    void Update()
    {
        if (!_headsetPositionMarked) return;

        float currentHeadsetY = _vrCam.position.y;
        float duckDistance = _initialHeadsetPosition.y - currentHeadsetY;

        if (duckDistance >= _duckThreshold && !_isDucking)
        {
            Duck();
            _isDucking = true;
        }
        else if (duckDistance < _duckThreshold && _isDucking)
        {
            UnDuck();
            _isDucking = false;
        }
    }

    void Duck()
    {
        _capsuleCollider.enabled = false;
    }

    void UnDuck()
    {
        _capsuleCollider.enabled = true;
    }
}