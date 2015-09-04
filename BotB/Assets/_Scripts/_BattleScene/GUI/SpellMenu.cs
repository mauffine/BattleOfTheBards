using UnityEngine;
using System.Collections;

public enum MenuSelection
{
    offence,
    defence,
    effect
}

public class SpellMenu : MonoBehaviour 
{
    [SerializeField]
    static MenuSelection m_currentSelection;

    [SerializeField]
    GameObject m_leftSelection;
    SpellMenuButton m_leftSelectionScript;
    [SerializeField]
    GameObject m_upSelection;
    SpellMenuButton m_upSelectionScript;
    [SerializeField]
    GameObject m_rightSelection;
    SpellMenuButton m_rightSelectionScript;
    public static bool m_showMenu;


	// Use this for initialization
	void Start() 
    {
        ShowMenu();
        m_leftSelectionScript = m_leftSelection.GetComponent<SpellMenuButton>();
        m_upSelectionScript = m_upSelection.GetComponent<SpellMenuButton>();
        m_rightSelectionScript = m_rightSelection.GetComponent<SpellMenuButton>();
        //m_leftSelection = (GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note A"), new Vector3(0, 0, 0), new Quaternion(0, 1, 0, 0));
        //m_leftSelection.transform.SetParent(this.transform.parent);
        //m_leftSelection.transform.localPosition = new Vector3(0, 0, 0);
        
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (TurnTimer.Instance.CurrentTurn == Turn.Menu)
            ShowMenu();
        else
            HideMenu();  

        if(m_showMenu)
        {
            if(Input.GetKey(KeyCode.A))
            {
                m_leftSelectionScript.SetSelected();
            }
            else
            {
                m_leftSelectionScript.SetUnselected();
            }

            if (Input.GetKey(KeyCode.S))
            {
                m_upSelectionScript.SetSelected();
            }
            else
            {
                m_upSelectionScript.SetUnselected();
            }

            if (Input.GetKey(KeyCode.D))
            {
                m_rightSelectionScript.SetSelected();
            }
            else
            {
                m_rightSelectionScript.SetUnselected();
            }

        }
	}

    public static MenuSelection SpellType
    {
        get { return m_currentSelection; }
    }

    public static void SelectOffence()
    {
        m_currentSelection = MenuSelection.offence;
    }

    public static void SelectDefence()
    {
        m_currentSelection = MenuSelection.defence;
    }

    public static void SelectEffect()
    {
        m_currentSelection = MenuSelection.effect;
    }

    public void ShowMenu()
    {
        m_showMenu = true;
        m_leftSelection.GetComponent<SpellMenuButton>().Show();
        m_upSelection.GetComponent<SpellMenuButton>().Show();
        m_rightSelection.GetComponent<SpellMenuButton>().Show();
        
    }

    public void HideMenu()
    {
        m_showMenu = false;
        m_leftSelection.GetComponent<SpellMenuButton>().Hide();
        m_upSelection.GetComponent<SpellMenuButton>().Hide();
        m_rightSelection.GetComponent<SpellMenuButton>().Hide();
    }
}
