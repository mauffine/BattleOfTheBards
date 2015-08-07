using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour 
{
    float m_lifetime;//How long the spell particles lasts= for
    Vector3 m_transform;//The transform
    string m_key;//A key to show what key
	// Use this for initialization
	void Start() 
    {
        m_lifetime = 2.0f;
        m_transform = new Vector3(0.03f, Random.Range(-0.01f, 0.007f), 0);
	}
	// Update is called once per frame
	void Update() 
    {
        this.transform.Translate(m_transform);
        m_lifetime -= Time.deltaTime;

        if (m_lifetime <= 0)
            this.GetComponent<ParticleSystem>().enableEmission = false;
        if (m_lifetime <= -1)
            Destroy(gameObject);
	}
    //
    public string Key 
    {
        get {return m_key;}
    }
}
