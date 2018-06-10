using UnityEngine;

class BulletGenerator : MonoBehaviour
{

    [SerializeField]
    Bullet origin;

    public void Generate(Vector3 startPosition, Vector3 direction, float speed, Color32 color)
    {
        var obj = GameObject.Instantiate(origin.gameObject);
        var bullet = obj.GetComponent<Bullet>();
        bullet.Initialize(startPosition, direction.normalized, speed, color);
    }

    Color32 _homingBulletColor = new Color32(225, 244, 255, 255);
    public void HomingCircle(
            Vector3 startPosition,
            Vector3 targetPosition,
            float speed=3f,
            float offset=0f)
    {
        var dir = targetPosition - startPosition;
        for (var i = 0; i < 360; i+=6)
        {
            Generate(
                    startPosition,
                    Quaternion.Euler(0, 0, i + offset) * dir,
                    speed,
                    _homingBulletColor);
        }
    }

    Color32 _winderBulletColor = new Color32(255, 225, 255, 255);
    public void Winder(
            Vector3 startPosition,
            Vector3 targetPosition,
            float speed=3f,
            float offset=0f)
    {
        var dir = targetPosition - startPosition;
        for (var i = 0; i < 2; i++)
        {
            Generate(startPosition, Quaternion.Euler(0, 0, 10 * i + 5) * dir, speed, _winderBulletColor);
            Generate(startPosition, Quaternion.Euler(0, 0, -10 * i - 5) * dir, speed, _winderBulletColor);
        }
    }

}
