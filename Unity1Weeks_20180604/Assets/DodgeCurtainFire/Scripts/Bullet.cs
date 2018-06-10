using UnityEngine;

class Bullet : MonoBehaviour
{

    Color32 _color;

    float _speed = 0f;
    Vector3 _direction;
    Vector3 _startPosition;

    Transform _t = null;
    Transform t
    {
        get
        {
            if (_t == null) { _t = transform; }
            return _t;
        }
    }

    Renderer _r = null;
    Renderer r
    {
        get
        {
            if (_r == null) { _r = GetComponent<Renderer>(); }
            return _r;
        }
    }

    SpriteRenderer _sr = null;
    SpriteRenderer sr
    {
        get
        {
            if (_sr == null) { _sr = GetComponent<SpriteRenderer>(); }
            return _sr;
        }
    }

    public void Initialize(
            Vector3 startPosition,
            Vector3 direction,
            float speed,
            Color32 color)
    {
        _startPosition = startPosition;
        _direction = direction;
        _speed = speed;
        sr.color = color;
    }

    void Start()
    {
        t.position = _startPosition;
    }

    void Update()
    {
        t.position += _direction * _speed * Time.deltaTime;
        if (!r.isVisible) { Destroy(gameObject); }
    }

}
