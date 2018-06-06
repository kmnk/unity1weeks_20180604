using UnityEngine;

public class Ship : MonoBehaviour
{

    [SerializeField]
    bool _playable = true;

    [SerializeField]
    float _speed = 1f;

    Vector3 _up = Vector3.up;
    Vector3 _down = Vector3.down;
    Vector3 _right = Vector3.right;
    Vector3 _left = Vector3.left;

    Transform _t = null;
    Transform t
    {
        get
        {
            if (_t == null) { _t = transform; }
            return _t;
        }
    }

    void Control()
    {
        var up = Input.GetAxisRaw ("Vertical") > 0;
        var down = Input.GetAxisRaw ("Vertical") < 0;
        var right = Input.GetAxisRaw ("Horizontal") > 0;
        var left = Input.GetAxisRaw ("Horizontal") < 0;

        if (up) { t.position = t.position + _up * _speed; }
        if (down) { t.position = t.position + _down * _speed; }
        if (right) { t.position = t.position + _right * _speed; }
        if (left) { t.position = t.position + _left * _speed; }
    }

    void Update()
    {
        if (_playable) { Control(); }
    }

}
