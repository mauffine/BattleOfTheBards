using UnityEngine;
using System.Collections;


public class SpellMenu : MonoBehaviour 
{
    [SerializeField]
    static SpellType m_currentSelection;

    [SerializeField]
    GameObject m_upButton, m_leftButton, m_rightButton;
    SpellMenuButton m_upScript, m_leftScript, m_rightScript;
    Sprite m_top, m_left, m_right;

    [SerializeField]
    GameObject m_attackMenu, m_defenceMenu, m_effectMenu, m_centralMenu;

    public static bool m_showMenu;
    private static SpellMenu s_ref;

	// Use this for initialization
	void Start()
    {
        m_upScript = m_upButton.GetComponent<SpellMenuButton>();
        m_leftScript = m_leftButton.GetComponent<SpellMenuButton>();
        m_rightScript = m_rightButton.GetComponent<SpellMenuButton>();

        m_top = m_upButton.GetComponent<SpriteRenderer>().sprite;
        m_left = m_leftButton.GetComponent<SpriteRenderer>().sprite;
        m_right = m_rightButton.GetComponent<SpriteRenderer>().sprite;
        ShowMenu();
        s_ref = this;
	}
	
	//Updates the menu
	void Update() 
    {
        if(m_showMenu)
        {
            /*
            if (Input.GetKeyDown(KeyCode.S))
                SelectOffence();
            else if (Input.GetKeyDown(KeyCode.A))
                SelectDefence();
            else if (Input.GetKeyDown(KeyCode.D))
                SelectEffect(); */

            if (Input.GetButtonDown("Triangle"))
                SelectOffence();
            else if (Input.GetButtonDown("Square"))
                SelectDefence();
            else if (Input.GetButtonDown("Circle"))
                SelectEffect();

            if (m_currentSelection == SpellType.Offencive)
                SwitchMenu(m_attackMenu);
            if (m_currentSelection == SpellType.Defensive)
                SwitchMenu(m_defenceMenu);
            if (m_currentSelection == SpellType.Effect)
                SwitchMenu(m_effectMenu);
        }

        if (TurnTimer.Instance.CurrentTurn == Turn.Menu)
            ShowMenu();
        else
            HideMenu();  

	}

    public static SpellMenu Instance
    {
        get { return s_ref; }
    }
    ///<summary> Returns the current type of spell the player has selected</summary>
    public static SpellType Selection
    {
        get { return m_currentSelection; }
    }
    public void SelectOffence()
    {
        m_currentSelection = SpellType.Offencive;
        m_upScript.SetSelected();
        m_leftScript.SetUnselected();
        m_rightScript.SetUnselected();
    }
    public void SelectDefence()
    {
        m_currentSelection = SpellType.Defensive;
        m_leftScript.SetSelected();

        m_upScript.SetUnselected();
        m_rightScript.SetUnselected();
    }
    public void SelectEffect()
    {
        m_currentSelection = SpellType.Effect;
        m_rightScript.SetSelected();

        m_upScript.SetUnselected();
        m_leftScript.SetUnselected();
    }
    public void ShowMenu()
    {
        m_showMenu = true;
        m_upScript.Show();
        m_leftScript.Show();
        m_rightScript.Show();
    }
    public void HideMenu()
    {
        m_showMenu = false;
        m_upScript.Hide();
        m_leftScript.Hide();
        m_rightScript.Hide();

        m_upScript.SetUnselected();
        m_leftScript.SetUnselected();
        m_rightScript.SetUnselected();
    }
    private void SwitchMenu(GameObject a_menu)
    {
        a_menu.transform.parent = transform;
        //DEBUG HERE
        m_top = a_menu.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        m_left = a_menu.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        m_right =  a_menu.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;

        m_upScript = a_menu.transform.GetChild(0).GetComponent<SpellMenuButton>();
        m_leftScript = a_menu.transform.GetChild(1).GetComponent<SpellMenuButton>();
        m_rightScript = a_menu.transform.GetChild(2).GetComponent<SpellMenuButton>();

    }
}