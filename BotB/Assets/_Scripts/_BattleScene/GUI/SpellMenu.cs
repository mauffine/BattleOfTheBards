using UnityEngine;
using System.Collections;


public class SpellMenu : MonoBehaviour 
{
    private enum SelectorType
    {
        Offencive,
        Defensive,
        Effect,
        None
    };
    SelectorType m_castingType;
    private enum MenuState
    {
        TypeMenu,
        CastingMenu,
        Casting
    };
    MenuState m_guiState;

    [SerializeField]
    GameObject m_upButton, m_leftButton, m_rightButton;
    SpellMenuButton m_upScript, m_leftScript, m_rightScript;
    Sprite m_top, m_left, m_right;

    [SerializeField]
    GameObject m_attackMenu, m_defenceMenu, m_effectMenu, m_centralMenu, m_currentMenu;
    private bool m_pressed = false;
    private static SpellMenu s_ref;
	// Use this for initialization
	void Start()
    {
        m_guiState = MenuState.TypeMenu;
        m_castingType = SelectorType.None;

        m_upScript = m_upButton.GetComponent<SpellMenuButton>();
        m_leftScript = m_leftButton.GetComponent<SpellMenuButton>();
        m_rightScript = m_rightButton.GetComponent<SpellMenuButton>();

        m_top = m_upButton.GetComponent<SpriteRenderer>().sprite;
        m_left = m_leftButton.GetComponent<SpriteRenderer>().sprite;
        m_right = m_rightButton.GetComponent<SpriteRenderer>().sprite;
        m_currentMenu = m_centralMenu;
        ShowMenu();
        SwitchMenu(m_attackMenu);
        HideMenu();
        SwitchMenu(m_defenceMenu);
        HideMenu();
        SwitchMenu(m_effectMenu);
        HideMenu();
        SwitchMenu(m_centralMenu);
        s_ref = this;
	}	
	//Updates the menu
	void Update() 
    {
        if(m_guiState == MenuState.TypeMenu)
        {
            ShowMenu();
            if (m_castingType == SelectorType.None)
                SwitchMenu(m_centralMenu);
            m_pressed = false;
        }
        else if (m_guiState == MenuState.CastingMenu)
        {            
            if (!m_pressed)
            {
            if (Input.GetKeyDown(KeyCode.S)){
                SelectOffence(); m_pressed = true;}
            else if (Input.GetKeyDown(KeyCode.A)){
                SelectDefence(); m_pressed = true;}
            else if (Input.GetKeyDown(KeyCode.D)){
                SelectEffect(); m_pressed = true;}

            if (Input.GetButtonDown("Triangle")){
                SelectOffence(); m_pressed = true;}
            else if (Input.GetButtonDown("Square")){
                SelectDefence(); m_pressed = true;}
            else if (Input.GetButtonDown("Circle")){
                SelectEffect(); m_pressed = true;}
            }

            if (m_castingType == SelectorType.Offencive)
                SwitchMenu(m_attackMenu);
            if (m_castingType == SelectorType.Defensive)
                SwitchMenu(m_defenceMenu);
            if (m_castingType == SelectorType.Effect)
                SwitchMenu(m_effectMenu); 

            ShowMenu();
        }
        else if (m_guiState == MenuState.Casting)
        {
            HideMenu();
            m_currentMenu = m_centralMenu;
            m_castingType = SelectorType.None;
        }

        if (TurnTimer.Instance.CurrentTurn == Turn.Casting)
            m_guiState = MenuState.Casting;
	}

    public static SpellMenu Instance
    {
        get { return s_ref; }
    }
    ///<summary>Returns the current type of spell the player has selected</summary>
    public static SpellType Selection
    {
        get 
        {
            SpellType returnVal;
            returnVal = (Instance.m_castingType == SelectorType.Defensive) ? SpellType.Defensive : (Instance.m_castingType == SelectorType.Effect) ? SpellType.Effect : SpellType.Offencive;
            return returnVal;        
        }
    }
    public void SelectOffence()
    {
        m_guiState = MenuState.CastingMenu;
        m_castingType = SelectorType.Offencive;

        m_upScript.SetSelected();
        m_leftScript.SetUnselected();
        m_rightScript.SetUnselected();
    }
    public void SelectDefence()
    {
        m_guiState = MenuState.CastingMenu;
        m_castingType = SelectorType.Defensive;

        m_leftScript.SetSelected();
        m_upScript.SetUnselected();
        m_rightScript.SetUnselected();
    }
    public void SelectEffect()
    {
        m_guiState = MenuState.CastingMenu;

        m_castingType = SelectorType.Effect;

        m_rightScript.SetSelected();
        m_upScript.SetUnselected();
        m_leftScript.SetUnselected();
    }
    public void ShowMenu()
    {
        m_upScript.Show();
        m_leftScript.Show();
        m_rightScript.Show();
    }
    public void HideMenu()
    {
        m_guiState = MenuState.TypeMenu;
        m_upScript.Hide();
        m_leftScript.Hide();
        m_rightScript.Hide();

        m_upScript.SetUnselected();
        m_leftScript.SetUnselected();
        m_rightScript.SetUnselected();
    }
    private void SwitchMenu(GameObject a_menu)
    {
        m_upScript.Hide();
        m_leftScript.Hide();
        m_rightScript.Hide();

        m_currentMenu = a_menu;
        m_guiState = MenuState.CastingMenu;

        a_menu.transform.parent = transform;
        //DEBUG HERE
        m_top = a_menu.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        m_left = a_menu.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        m_right =  a_menu.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;

        m_upScript = a_menu.transform.GetChild(0).GetComponent<SpellMenuButton>();
        m_leftScript = a_menu.transform.GetChild(1).GetComponent<SpellMenuButton>();
        m_rightScript = a_menu.transform.GetChild(2).GetComponent<SpellMenuButton>();

        m_upScript.Show();
        m_leftScript.Show();
        m_rightScript.Show();
    }

    public bool UpSelected
    {
        get {return m_upButton.GetComponent<SpellMenuButton>().m_selected; }
    }
    public bool LeftSelected
    {

        //m_showMenu = true;
        //m_offenceButton.GetComponent<SpellMenuButton>().Show();
        //m_defenceButton.GetComponent<SpellMenuButton>().Show();
        //m_effectSelection.GetComponent<SpellMenuButton>().Show();
        get { return m_leftButton.GetComponent<SpellMenuButton>().m_selected; }
    }
    public bool RightSelected
    {
        //m_showMenu = false;
        //m_offenceButton.GetComponent<SpellMenuButton>().Hide();
        //m_defenceButton.GetComponent<SpellMenuButton>().Hide();
        //m_effectSelection.GetComponent<SpellMenuButton>().Hide();

        //m_offenceButtonScript.SetUnselected();
        //m_defenceButtonScript.SetUnselected();
        //m_effectSelectionScript.SetUnselected();
        get { return m_rightButton.GetComponent<SpellMenuButton>().m_selected; }
    }
}
