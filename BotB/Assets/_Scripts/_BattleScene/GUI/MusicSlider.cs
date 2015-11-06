using UnityEngine;
using System.Collections;

public class MusicSlider : MonoBehaviour
{
    [SerializeField]
    private bool m_active;
    [SerializeField]
    float m_screenWidth = 1920;
    float m_turnTime;
    float m_resetTick;
	// Use this for initialization
    private static Vector3 pos, locPos;
	void Start() 
    {
        m_resetTick = TurnTimer.Instance.CastingTime;
        m_turnTime = (1 / TurnTimer.Instance.CastingTime) + 0.02f;//Scales the time for the MusicSlider, add 0.02f to let the MusicSlider get off the screen before reseting
	}
	
	// Update is called once per frame
	void Update() 
    {
        m_active = (TurnTimer.Instance.CurrentTurn == Turn.Casting);

        if (m_active)
        {
            float modTime = (m_turnTime * Time.deltaTime);

            m_resetTick -= Time.deltaTime;
            bool playerTurn = (m_resetTick < 0);

            transform.localPosition += new Vector3(m_screenWidth * modTime, 0, 0);
            if (playerTurn)
            {
                m_resetTick = TurnTimer.Instance.CastingTime;
                Reset();
            }
        }
        else
            Reset();
        pos = transform.position;
        locPos = transform.localPosition;
	}
    private void Reset()
    {
        Vector3 myPos = transform.localPosition;
        transform.localPosition = new Vector3(-985, myPos.y, myPos.z);
    }
    public static Vector3 Position
    {
        get { return pos; }
    }
    public static Vector3 LocalPosition
    {
        get { return locPos; }
    }
    public bool Active
    {
        get { return m_active;}
        set { m_active = value;}
    }
}