using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
    float m_lifetime;
    Vector3 m_transform;
	// Use this for initialization
	void Start () {
        m_lifetime = 2.0f;
        m_transform = new Vector3(.03f, Random.Range(-.01f, 0.007f), 0);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(m_transform);
        m_lifetime -= Time.deltaTime;
        if (m_lifetime <= 0)
            this.GetComponent<ParticleSystem>().enableEmission = false;
        if (m_lifetime <= -1)
            Destroy(gameObject);
	}
}
