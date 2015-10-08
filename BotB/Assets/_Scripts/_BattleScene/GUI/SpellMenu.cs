using UnityEngine;
using System.Collections;


public class SpellMenu : MonoBehaviour 
{
    [SerializeField]
    static SpellType m_currentSelection;

    static Element m_spellElement; //probs temp
    [SerializeField]
    GameObject m_offenceButton;
    SpellMenuButton m_offenceButtonScript;
    [SerializeField]
    GameObject m_defenceButton;
    SpellMenuButton m_defenceButtonScript;
    [SerializeField]
    GameObject m_effectSelection;
    [SerializeField]
    GameObject m_fireButton, m_iceButton, m_arcaneButton;
    SpellMenuButton m_effectSelectionScript;
    public static bool m_showMenu;
    public static SpellMenu s_ref;

	// Use this for initialization
	void Start() 
    {
        ShowMenu();
        m_offenceButtonScript = m_offenceButton.GetComponent<SpellMenuButton>();
        m_defenceButtonScript = m_defenceButton.GetComponent<SpellMenuButton>();
        m_effectSelectionScript = m_effectSelection.GetComponent<SpellMenuButton>();
        s_ref = this;
        
	}
	
	//Updates the menu
	void Update() 
    {
        if (TurnTimer.Instance.CurrentTurn == Turn.Menu)
            ShowMenu();
        else
            HideMenu();  

        if(m_showMenu)
        {
            if (Input.GetKeyDown(KeyCode.S))
                SelectOffence();
            else if (Input.GetKeyDown(KeyCode.A))
                SelectDefence();
            else if (Input.GetKeyDown(KeyCode.D))
                SelectEffect();
            else if (Input.GetKeyDown(KeyCode.Q))
                SelectIce();
            else if (Input.GetKeyDown(KeyCode.W))
                SelectFire();
            else if (Input.GetKeyDown(KeyCode.E))
                SelectArcane();
            if (Input.GetButtonDown("Triangle"))
                SelectOffence();
            else if (Input.GetButtonDown("Square"))
                SelectDefence();
            else if (Input.GetButtonDown("Circle"))
                SelectEffect();
            else if (Input.GetAxis("D-Pad Y") <= -1.0f)
                SelectIce();
            else if (Input.GetAxis("D-Pad X") >= 1.0f)
                SelectFire();
            else if (Input.GetAxis("D-Pad Y") >= 1.0f)
                SelectArcane();
        }
	}

    public static SpellMenu Instance
    {
        get { return s_ref; }
    }
    ///  <summary> Returns the current type of spell the player has selected</summary>
    public static SpellType Selection
    {
        get { return m_currentSelection; }
    }
    public static Element SelectedElement
    {
        get { return m_spellElement; }
    }
    public void SelectOffence()
    {
        m_currentSelection = SpellType.Offencive;
        m_offenceButtonScript.SetSelected();

        m_defenceButtonScript.SetUnselected();
        m_effectSelectionScript.SetUnselected();
    }
    public void SelectDefence()
    {
        m_currentSelection = SpellType.Defensive;
        m_defenceButtonScript.SetSelected();

        m_offenceButtonScript.SetUnselected();
        m_effectSelectionScript.SetUnselected();
    }
    public void SelectEffect()
    {
        m_currentSelection = SpellType.Effect;
        m_effectSelectionScript.SetSelected();

        m_offenceButtonScript.SetUnselected();
        m_defenceButtonScript.SetUnselected();
    }
    //temp elements
    public void SelectIce()
    {
        m_spellElement = Element.Ice;
        m_fireButton.GetComponent<SpriteRenderer>().enabled = false;
        m_iceButton.GetComponent<SpriteRenderer>().enabled = true;
        m_arcaneButton.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void SelectFire()
    {
        m_spellElement = Element.Fire;
        m_fireButton.GetComponent<SpriteRenderer>().enabled = true;
        m_iceButton.GetComponent<SpriteRenderer>().enabled = false;
        m_arcaneButton.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void SelectArcane()
    {
        m_spellElement = Element.Arcane;
        m_fireButton.GetComponent<SpriteRenderer>().enabled = false;
        m_iceButton.GetComponent<SpriteRenderer>().enabled = false;
        m_arcaneButton.GetComponent<SpriteRenderer>().enabled = true;
    }
    public void ShowMenu()
    {
        m_showMenu = true;
        m_offenceButton.GetComponent<SpellMenuButton>().Show();
        m_defenceButton.GetComponent<SpellMenuButton>().Show();
        m_effectSelection.GetComponent<SpellMenuButton>().Show();
    }
    public void HideMenu()
    {
        m_showMenu = false;
        m_offenceButton.GetComponent<SpellMenuButton>().Hide();
        m_defenceButton.GetComponent<SpellMenuButton>().Hide();
        m_effectSelection.GetComponent<SpellMenuButton>().Hide();

        m_offenceButtonScript.SetUnselected();
        m_defenceButtonScript.SetUnselected();
        m_effectSelectionScript.SetUnselected();
    }
}
