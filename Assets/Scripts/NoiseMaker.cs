using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    public float m_CurrentNoiseLevel;
    public float noiseReducMulti;

    private void Update()
    {
        if (m_CurrentNoiseLevel > 0)
        {
            m_CurrentNoiseLevel -= Time.deltaTime * noiseReducMulti;
        }
    }
    public void makeNoise(float noisetoMake)
    {
        m_CurrentNoiseLevel -= noisetoMake;
    }
}