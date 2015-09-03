using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour
{
    [SerializeField]
    Transform m_transform;
    [SerializeField]
    float m_screenWidth = 1920;
    float m_turnTime;
    Turn m_turn;
	// Use this for initialization
    private static Vector3 pos, locPos;
	void Start() 
    {
        m_screenWidth /= 2;
        m_turnTime =  1 / TurnTimer.Instance.CastingTime;//Scales the time for the slider
        m_turn = TurnTimer.Instance.CurrentTurn;
	}
	
	// Update is called once per frame
	void Update() 
    {
        float modTime = (m_turnTime * Time.deltaTime);
        Turn turn = TurnTimer.Instance.CurrentTurn;
        
        if (turn == Turn.Casting)
            m_transform.localPosition += new Vector3(m_screenWidth * modTime, 0, 0);
        else
            m_transform.localPosition -= new Vector3(m_screenWidth * modTime, 0, 0);

        if (m_turn != turn)
        {
            Vector3 myPos = m_transform.localPosition;
            if (turn == Turn.Casting)
                m_transform.localPosition = new Vector3(-966, myPos.y, myPos.z);
            else
                m_transform.localPosition = new Vector3(966, myPos.y, myPos.z);
            m_turn = turn;
            Vector3 flipScale = new Vector3(m_transform.localScale.x * -1,100,1);
            m_transform.localScale = flipScale;
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