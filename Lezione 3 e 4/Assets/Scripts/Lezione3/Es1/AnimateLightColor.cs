using UnityEngine;

public class AnimateLightColor : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] public GameObject lampadina;

    [Header("Setting")]
    public Color Color_A;
    public Color Color_B;
    public float Bounce_Speed;
    public float Pong_Lenght;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lampadina == null) return;

        float t = Mathf.PingPong(Time.time * Bounce_Speed, Pong_Lenght) / Pong_Lenght;
        lampadina.GetComponent<Light>().color = Color.Lerp(Color_A, Color_B, t);
    }
}
