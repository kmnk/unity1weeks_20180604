using UnityEngine;

class BulletGenerator : MonoBehaviour
{

    [SerializeField]
    Bullet origin;

    public void Generate(Vector3 startPosition, Vector3 direction, float speed)
    {
        var obj = GameObject.Instantiate(origin.gameObject);
        var bullet = obj.GetComponent<Bullet>();
        bullet.Initialize(startPosition, direction, speed);
    }

}
