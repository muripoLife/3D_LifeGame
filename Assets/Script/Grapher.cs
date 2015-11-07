using UnityEngine;
using System.Collections;

public class Grapher : MonoBehaviour
{

    public enum FunctionOption
    {
        Linear,
        Exponential,
        Parabola,
        Sine,
        Ripple
    }

    private delegate float FunctionDelegate(Vector3 p, float t);
    private static FunctionDelegate[] functionDelegates = {
        Linear,
        Exponential,
        Parabola,
        Sine,
        Ripple
    };

    public FunctionOption function;

    [Range(10, 100)]
    // 解像度の定義
    public int resolution = 10;
    // 現解像度
    private int currentResolution;
    // パーティクルの座標の配列定義
    private ParticleSystem.Particle[] points;

    private void CreatePoints()
    {
        // 解像度の条件
        currentResolution = resolution;
        points = new ParticleSystem.Particle[resolution * resolution];
        float increment = 1f / (resolution - 1);
        // 座標と色の設定
        int i = 0;
        for (int x = 0; x < resolution; x++)
        {
            for (int z = 0; z < resolution; z++)
            {
                Vector3 p = new Vector3(x * increment, 0f, z * increment);
                points[i].position = p;
                points[i].color = new Color(p.x, p.y, p.z);
                points[i++].size = 0.1f;
            }
        }
    }

    void Update()
    {
        if (currentResolution != resolution || points == null)
        {
            CreatePoints();
        }
        FunctionDelegate f = functionDelegates[(int)function];
        float t = Time.timeSinceLevelLoad;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 p = points[i].position;
            p.y = f(p, t);
            points[i].position = p;
            Color c = points[i].color;
            c.g = p.y;
            points[i].color = c;
        }
        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

    /*
        数式定義
    */
    // 直線
    private static float Linear(Vector3 p, float t)
    {
        return p.x;
    }

    // 放物線(s^2)
    private static float Exponential(Vector3 p, float t)
    {
        return p.x * p.x;
    }

    // 放物線(2x-1)^2
    private static float Parabola(Vector3 p, float t)
    {
        p.x += p.x - 1f;
        p.z += p.z - 1f;
        return 1f - p.x * p.x * p.z * p.z;
    }

    // (sin(2πx + Δ) + 1) / 2
    private static float Sine(Vector3 p, float t)
    {
        return 0.50f +
            0.25f * Mathf.Sin(4f * Mathf.PI * p.x + 4f * t) * Mathf.Sin(2f * Mathf.PI * p.z + t) +
            0.10f * Mathf.Cos(3f * Mathf.PI * p.x + 5f * t) * Mathf.Cos(5f * Mathf.PI * p.z + 3f * t) +
            0.15f * Mathf.Sin(Mathf.PI * p.x + 0.6f * t);
    }

    // Wave
    private static float Ripple(Vector3 p, float t)
    {
        p.x -= 0.5f;
        p.z -= 0.5f;
        float squareRadius = p.x * p.x + p.z * p.z;
        return 0.5f + Mathf.Sin(15f * Mathf.PI * squareRadius - 2f) / (2f + 100f * squareRadius);
    }
}