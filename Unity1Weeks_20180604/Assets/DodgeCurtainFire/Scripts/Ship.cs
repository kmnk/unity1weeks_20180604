using UnityEngine;

public class Ship : MonoBehaviour
{

    [SerializeField]
    bool _isPlayer = true;

    [SerializeField]
    Ship _targetShip;

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

        if (!up && !down && !right && !left)
        {
            return;
        }

        var p = t.position;
        var rate = _speed * Time.deltaTime;

        if (up) { p = p + _up * rate; }
        if (down) { p = p + _down * rate; }
        if (right) { p = p + _right * rate; }
        if (left) { p = p + _left * rate; }

        Move(p);
    }

    void Move(Vector3 toPosition)
    {
        var min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        var max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        toPosition.x = Mathf.Clamp(toPosition.x, min.x, max.x);
        toPosition.y = Mathf.Clamp(toPosition.y, min.y, max.y);

        t.position = toPosition;
    }

    int _counter = 0;
    void Cpu()
    {
        _counter++;
        if (_counter > 100)
        {
            if (_counter > 150)
            {
                _rand = Random.Range(0, 3);
                _counter = 0;
            }
            else
            {
                return;
            }
        }
        if (_rand == 0 || _rand == 1)
        {
            _bulletGenerator.HomingCircle(t.position, _targetShip.transform.position);
        }
        if (_rand == 0 || _rand == 2)
        {
            _bulletGenerator.Winder(t.position, _targetShip.transform.position);
        }
    }

    int _rand;
    void Awake()
    {
        _rand = Random.Range(0, 2);
    }

    void Update()
    {
        if (_isPlayer) { Control(); }
        if (!_isPlayer) { Cpu(); }
    }

}
