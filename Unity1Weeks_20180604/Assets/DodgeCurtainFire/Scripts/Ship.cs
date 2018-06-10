using UnityEngine;

public class Ship : MonoBehaviour
{

    [SerializeField]
    bool _isPlayer = true;

    [SerializeField]
    ShipCore _core;

    [SerializeField]
    float _speed = 100f;

    [SerializeField]
    BulletGenerator _bulletGenerator;

    Vector3 _up = Vector3.up;
    Vector3 _down = Vector3.down;
    Vector3 _right = Vector3.right;
    Vector3 _left = Vector3.left;

    Transform _t = null;
    public Transform T
    {
        get
        {
            if (_t == null) { _t = transform; }
            return _t;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!_isPlayer) { return; }
        if (c.tag != "DamageObject") { return; }
        ScoreCounter.Count();
    }

    void Player()
    {
        var up = Input.GetAxisRaw("Vertical") > 0;
        var down = Input.GetAxisRaw("Vertical") < 0;
        var right = Input.GetAxisRaw("Horizontal") > 0;
        var left = Input.GetAxisRaw("Horizontal") < 0;

        if (!up && !down && !right && !left)
        {
            return;
        }

        var p = T.position;
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

        T.position = toPosition;
    }

    float _time = 6f;
    void Cpu()
    {
        _time += Time.deltaTime;

        if (ScoreCounter.Stopped) { return; }

        if (_time > 7f)
        {
            var _rand = Random.Range(0, 4);
            if (_rand == 0 || _rand == 1)
            {
                _bulletGenerator.HomingCircle(T.position);
            }
            if (_rand == 0 || _rand == 2)
            {
                _bulletGenerator.Winder(T.position);
            }
            if (_rand == 0 || _rand == 3)
            {
                _bulletGenerator.Basic(T.position);
            }
            _time = 0f;
        }
    }

    void Awake()
    {
        _core.Initialize(_isPlayer);
    }

    void Update()
    {
        if (_isPlayer) { Player(); }
        if (!_isPlayer) { Cpu(); }
    }

}
