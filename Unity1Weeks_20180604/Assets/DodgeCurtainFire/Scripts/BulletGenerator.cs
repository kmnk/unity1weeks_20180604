using System.Collections;
using UnityEngine;

class BulletGenerator : MonoBehaviour
{

    [SerializeField]
    Bullet _origin;

    [SerializeField]
    Ship _target;

    public IEnumerator Static(
            Vector3 startPosition,
            Vector3 direction,
            float duration,
            float speed,
            Color32 color)
    {
        var time = 0f;
        for (var i = 0; i < duration / 100; i++)
        {
            time += Time.deltaTime;
            if (time > duration) { break; }

            var obj = GameObject.Instantiate(_origin.gameObject);
            var bullet = obj.GetComponent<Bullet>();
            bullet.Initialize(startPosition, direction.normalized, speed, color);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator Dynamic(
            Vector3 startPosition,
            System.Func<Vector3> direction,
            float duration,
            float speed,
            Color32 color)
    {
        var time = 0f;
        for (var i = 0; i < duration / 200; i++)
        {
            time += Time.deltaTime;
            if (time > duration) { break; }

            var obj = GameObject.Instantiate(_origin.gameObject);
            var bullet = obj.GetComponent<Bullet>();
            bullet.Initialize(startPosition, direction().normalized, speed, color);

            yield return new WaitForSeconds(0.2f);
        }
    }

    Color32 _homingBulletColor = new Color32(225, 244, 255, 255);
    public void HomingCircle(
            Vector3 startPosition,
            float duration=5000f,
            float speed=2f,
            float offset=0f)
    {
        for (var i = 0; i < 360; i+=6)
        {
            var j = i;
            StartCoroutine(Dynamic(
                    startPosition,
                    () => {
                        var dir = _target.T.position - startPosition;
                        return Quaternion.Euler(0, 0, j + offset) * dir;
                    },
                    duration,
                    speed,
                    _homingBulletColor));
        }
    }

    Color32 _winderBulletColor = new Color32(255, 225, 255, 255);
    public void Winder(
            Vector3 startPosition,
            float speed=2f,
            float duration=5000f,
            float offset=0f)
    {
        for (var i = 0; i < 2; i++)
        {
            var j = i;
            StartCoroutine(Dynamic(
                startPosition,
                () =>
                {
                    var dir = _target.T.position - startPosition;
                    return Quaternion.Euler(0, 0, 10 * j + 5) * dir;
                },
                duration,
                speed,
                _winderBulletColor));
            StartCoroutine(Dynamic(
                startPosition,
                () =>
                {
                    var dir = _target.T.position - startPosition;
                    return Quaternion.Euler(0, 0, -10 * j - 5) * dir;
                },
                duration,
                speed,
                _winderBulletColor));
        }
    }

    Color32 _basicBulletColor = new Color32(0, 0, 0, 230);
    public void Basic(
            Vector3 startPosition,
            float speed=3f,
            float duration=5000f,
            float offset=0f)
    {
        var dir = _target.T.position - startPosition;
        StartCoroutine(Static(startPosition, dir, duration, speed, _basicBulletColor));
    }

}
