using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour 
{
    [SerializeField]
    private Sprite m_sprite = null;
    public SpriteRenderer m_rend;
    [SerializeField]
    Musician m_me = null;
    float m_barLen = -1.0f, m_barFull = -1.0f;
    int m_health;
	// Use this for initialization
	void Start() 
    {
        m_rend.color = Color.green;
        m_sprite = Instantiate(m_sprite);
        m_rend.sprite = m_sprite;
        m_sprite.border.Set(0,0,1000,50);
        m_health = m_me.Health;
        m_barFull = m_barLen = m_sprite.border.z;
        m_me.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update()
    {
        m_barLen = m_barFull;
        m_health = m_me.Health;
        m_barLen -= (m_sprite.border.z / m_health);
        m_sprite.border.Set(m_sprite.border.x, m_sprite.border.y, m_barLen, m_sprite.border.w);
	}
}
