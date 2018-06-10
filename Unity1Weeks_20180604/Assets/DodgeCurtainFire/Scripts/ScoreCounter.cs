using UnityEngine;

public class ScoreCounter : MonoBehaviour
{

    public static int Score = 0;

    public static bool Stopped = false;

    public static void Count()
    {
        if (Stopped) { return; }
        Score++;
    }

    public static void Stop()
    {
        Stopped = true;
    }

    public static void Reset()
    {
        Score = 0;
        Stopped = false;
    }

    [SerializeField]
    private float _updateInterval = 0.5f;

    private float _accum;
    private int _frames;
    private float _timeleft;
    private float _fps;

    void Update()
    {
        if (Stopped && Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        _timeleft -= Time.deltaTime;
        _accum += Time.timeScale / Time.deltaTime;
        _frames++;

        if ( 0 < _timeleft ) return;

        _fps = _accum / _frames;
        _timeleft = _updateInterval;
        _accum = 0;
        _frames = 0;
    }

    private void OnGUI()
    {
        GUILayout.Label(
            "FPS: " + _fps.ToString( "f2" )
            + "\nSCORE: " + Score
            + (Stopped ? " GAME OVER !" : ""));
    }

}
