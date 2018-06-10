using UnityEngine;

public class ShipCore : MonoBehaviour
{

    bool _isPlayer = false;

    public void Initialize(bool isPlayer)
    {
        _isPlayer = isPlayer;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!_isPlayer) { return; }
        if (c.tag != "DamageObject") { return; }
        ScoreCounter.Stop();
    }

}
