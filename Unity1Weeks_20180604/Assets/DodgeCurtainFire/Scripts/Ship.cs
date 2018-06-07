using UnityEngine;

public class Ship : MonoBehaviour
{

    [SerializeField]
    bool _playable = true;

    [SerializeField]
    float _speed = 100f;

    [SerializeField]
    BulletGenerator _bulletGenerator;

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
        var up = Input.GetAxisRaw("Vertical") > 0;
        var down = Input.GetAxisRaw("Vertical") < 0;
        var right = Input.GetAxisRaw("Horizontal") > 0;
        var left = Input.GetAxisRaw("Horizontal") < 0;

        var p = t.position;
        var rate = _speed * Time.deltaTime;

        if (up) { p = p + _up * rate; }
        if (down) { p = p + _down * rate; }
        if (right) { p = p + _right * rate; }
        if (left) { p = p + _left * rate; }

        t.position = p;

        if (Input.GetKey(KeyCode.Space))
        {
            _bulletGenerator.Generate(t.position, Vector3.right, 5f);
        }
    }

    void Update()
    {
        if (_playable) { Control(); }
    }

}
