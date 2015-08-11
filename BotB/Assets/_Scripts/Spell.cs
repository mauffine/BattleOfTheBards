using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour 
{
    float m_lifetime;//How long the spell particles lasts= for
    Vector3 m_velocity;//The velocity
    string m_key;//A key to show what key
	// Use this for initialization
	void Start() 
    {
        m_lifetime = 2.0f;
        m_velocity = new Vector3(-.03f, Random.Range(-.007f, 00f), 0);
	}
	// Update is called once per frame
	void Update() 
    {
        transform.Translate(m_velocity);
        //m_lifetime -= Time.deltaTime;

        //if (m_lifetime <= 0)
        //    GetComponent<ParticleSystem>().enableEmission = false;
        //if (m_lifetime <= -1)
        //    Destroy(gameObject);
	}
    //
    public string Key 
    {
        get {return m_key;}
    }
}
