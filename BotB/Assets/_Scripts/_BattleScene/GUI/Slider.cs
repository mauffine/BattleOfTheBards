using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour
{
    [SerializeField]
    Transform m_transform;
    [SerializeField]
    float m_screenWidth = 1920;
    float m_turnTime;
    float m_resetTick;
	// Use this for initialization
    private static Vector3 pos, locPos;
	void Start() 
    {
        //m_screenWidth /= 2;
        m_resetTick = TurnTimer.Instance.CurrentTime;
        m_turnTime =  1 / TurnTimer.Instance.CurrentTime;//Scales the time for the slider
	}
	
	// Update is called once per frame
	void Update() 
    {
        float modTime = (m_turnTime * Time.deltaTime) / 4;

        m_resetTick -= Time.deltaTime;
        bool playerTurn = (m_resetTick < 0);
        
        m_transform.localPosition += new Vector3(m_screenWidth * modTime, 0, 0);
        if (playerTurn)
        {
            Vector3 myPos = m_transform.localPosition;
            m_resetTick = TurnTimer.Instance.CurrentTime;
            m_transform.localPosition = new Vector3(-966, myPos.y, myPos.z);
        }

        pos = m_transform.position;
        locPos = m_transform.localPosition;
	}

    public static Vector3 Position
    {
        get { return pos; }
    }

    public static Vector3 LocalPosition
    {
        get { return locPos; }
    }
}