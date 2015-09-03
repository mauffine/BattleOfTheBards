using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour
{
    [SerializeField]
    Transform m_transform;
    [SerializeField]
    float m_screenWidth = 1920;
    float m_turnTime;
    bool m_active;//THIS NEEDS TO BE DETACHED FROM PLAYER TURN
	// Use this for initialization
    private static Vector3 pos, locPos;
	void Start() 
    {
        //m_screenWidth /= 2;
        m_turnTime =  1 / TurnTimer.TimePerTurn;//Scales the time for the slider
        m_active = Battle.BattleReference.PlayerTurn;
	}
	
	// Update is called once per frame
	void Update() 
    {
        float modTime = (m_turnTime * Time.deltaTime);
        bool playerTurn =  Battle.BattleReference.PlayerTurn;
        
        m_transform.localPosition += new Vector3(m_screenWidth * modTime, 0, 0);
        if (m_active != playerTurn)
        {
            Vector3 myPos = m_transform.localPosition;
            m_transform.localPosition = new Vector3(-966, myPos.y, myPos.z);
            m_active = playerTurn;
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