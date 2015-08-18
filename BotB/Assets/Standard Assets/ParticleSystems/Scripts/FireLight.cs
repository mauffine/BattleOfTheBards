using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Effects
{
    public class FireLight : MonoBehaviour
    {
        private float m_Rnd;
        private bool m_Burning = true;
        private Light m_Light;


        private void Start()
        {
            m_Rnd = Random.value;
            if(m_Rnd < 0.5f)
                m_Rnd = 0.5f;
            m_Rnd *= 25;
            m_Light = GetComponentInParent<Light>();
        }


        private void Update()
        {
            if (m_Burning)
            {
                float secs = Time.time;
                m_Light.intensity = 1.0f + Mathf.Abs(Mathf.Cos(Time.realtimeSinceStartup) * 0.2f) + 0.2f;
                float x = Mathf.PerlinNoise(m_Rnd + 0 + secs * 0.5f, m_Rnd + 1 + secs * 0.5f) - 0.5f;
                float y = Mathf.PerlinNoise(m_Rnd + 2 + secs * 0.5f, m_Rnd + 3 + secs * 0.5f) - 0.75f;
                float z = Mathf.PerlinNoise(m_Rnd + 4 + secs * 0.5f, m_Rnd + 5 + secs * 0.5f) - 0.5f;
                transform.localPosition = Vector3.up + new Vector3(x, y, z) * 0.25f;
            }
        }


        public void Extinguish()
        {
            m_Burning = false;
            m_Light.enabled = false;
        }
    }
}
