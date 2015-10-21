using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour 
{
    [SerializeField]
    private Sprite m_sprite = null;
    public SpriteRenderer m_rend;
    [SerializeField]
    Musician m_me = null;
    float m_barLen = -1.0f;
    float m_barFull = 0.425f;//Magic number for scale
    int m_health;
    int m_fullHealth;
	// Use this for initialization
	void Start()
    {
        m_rend.color = Color.green;
        m_sprite = Instantiate(m_sprite);
        m_rend.sprite = m_sprite;
        m_health = m_me.Health;
        m_barLen = m_barFull;
        m_fullHealth = m_health;
        m_barFull = m_rend.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update()
    {
        m_barLen = m_barFull;
        m_health = m_me.Health;
        m_barLen *= ((float)m_health / m_fullHealth);
        if (float.IsInfinity(m_barLen))
            m_barLen = 0.00000000000000001f;
        else
            m_rend.GetComponent<Transform>().localScale = new Vector3(m_barLen, 1, 0);
	}
}