using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public enum SpellType : byte //What type of spell it is
{
    Offencive,
    Defensive, 
    Effect
}
public enum Element : byte //the element of the spell, probs gonna remove this later for something better
{
    Fire,
    Ice,
    Arcane
}
public enum Turn : byte //Which turn it currently is
{
    Casting,
    Menu,
    SpellEffect,
    Pause
}
///<summary>A musical note in an enum, _ means the note is flat</summary>
public enum Note : byte
{
    A_,
    A,
    B_,
    B,
    C,
    D_,
    D,
    E,
    F_,
    F,
    G,
    BLANK,
};
///<summary>A struct for the time a note was played and the note itself</summary>
public struct TimedNote
{
    public float m_time;
    public Note m_note;
    public bool m_playerOwned; //need a better name...
    public bool m_active; //Richard here; Apparently structs can't be nulled in C#
    public TimedNote(Note a_note, float a_time, bool a_playerOwned = false)
    {
        m_note = a_note;
        m_time = a_time;
        m_playerOwned = a_playerOwned;
        m_active = true;
    }
}
//*********************************************************************************
//actual class starts here
public class Battle : MonoBehaviour
{
    //Attributes
    public static Battle Instance; //singleton instance
    [SerializeField]
    GameObject m_player, m_currentEnemy; //the characters in the scene
    [SerializeField]
    GameObject m_eventBank;
    [SerializeField]
    List<GameObject> m_enemyList;
    public int m_enemyListIndex;
    [SerializeField]
    List<TransitionScreens> m_screenList;
    [HideInInspector]
    public bool m_activeBattle; //IMPORTANT, this variable now controls the activities of many objects in the scene to avoid glitches. When the musician has died, this should be set to false alongside.
    bool m_displayingScreens = false;
    int m_screenTransitionIndex; //can use the enemy index for which transition set
    ScreenTransition m_screenTransition;

    public bool m_win, m_playing; //bools for the end of the battle
    private float m_winTimer = 5;

    //public int m_switchOnEnemy;
    //public GameObject m_turnAssetOff;
    //public GameObject m_turnAssetOn;
    //bool m_assetsSwitched = false;

    //Behavious
    void Awake()
    {
        Instance = this; //set singleton instance to this
    }
    void Start()
    {
        Application.targetFrameRate = 300; //attampt this framerate
        //m_currentEnemy = Instantiate(m_enemyList[0]);
        //m_activeBattle = true;
        //m_enemyListIndex = 1;
        m_activeBattle = false;
        m_displayingScreens = true;
        m_enemyListIndex = 0;
        m_screenTransition = GetComponent<ScreenTransition>();
        m_win = false;

        //past here is the death sequence
        m_screenTransitionIndex = 0;
        m_displayingScreens = true;
        if (m_screenList.Count > m_enemyListIndex)
        {
            m_screenTransition.SetTexture(m_screenList[m_enemyListIndex].m_textures[0]);
            m_screenTransitionIndex++;
            m_screenTransition.SetScreen(true, 0.5f);
        }
        else
        {
            //endgame
        }
    }
    void Update()
    {
        //Debug.Log(m_enemyListIndex);

        if (m_activeBattle)
        {
            if (m_currentEnemy.GetComponent<Musician>().Health <= 0)
            {                
                m_currentEnemy.GetComponent<Musician>().Animate(7); //currently not playing because it's immediately deleted
                m_activeBattle = false;
                //past here is the death sequence
                m_screenTransitionIndex = 0;
                m_displayingScreens = true;
                if (m_enemyListIndex < m_enemyList.Count)
                {
                    m_screenTransition.SetTexture(m_screenList[m_enemyListIndex].m_textures[0]);
                    m_screenTransitionIndex++;
                    m_screenTransition.SetScreen(true, 0.5f);
                }
                else
                {
                    m_activeBattle = false;
                    m_win = true;
                    m_playing = false;
                    m_winTimer = 3;
                    //endgame
                }

            }
            else if(m_player.GetComponent<Musician>().Health <= 0)
            {
                m_activeBattle = false;
                m_win = false;
                m_playing = false;
                m_winTimer = 3;
                //player dies
            }
        }

        //Handles the screen transitions
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Cross")) && m_activeBattle == false && m_displayingScreens) //note the very first list of textures is to be used for the intro
        {
            if(m_enemyListIndex < m_enemyList.Count)
            {
                if(m_screenList[m_enemyListIndex].m_textures.Count > m_screenTransitionIndex)
                {
                    m_screenTransition.SetTexture(m_screenList[m_enemyListIndex].m_textures[m_screenTransitionIndex]);
                    m_screenTransitionIndex++;
                }
                else
                {
                    m_screenTransitionIndex = 0;
                    m_displayingScreens = false;
                    m_screenTransition.SetScreen(false, 0.5f);
                    bool enemiesLeft = SetNextEnemy();
                    m_player.GetComponent<Musician>().Reset();
                    if (!enemiesLeft)
                    {
                        //not currently used, scene is over
                    }
                }
            }
        }

        //Debug.Log(m_enemyListIndex);

        //debug
        if(Input.GetKeyDown(KeyCode.T))
        {
            m_currentEnemy.GetComponent<Musician>().TakeDamage(500);
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            m_player.GetComponent<Musician>().TakeDamage(500);
        }

        if(m_playing == false)
        {
            m_winTimer -= Time.deltaTime;
            if(m_winTimer <= 0)
            {
                if(m_win)
                {
                    m_screenTransition.TransitionToScene("MenuScene");
                    //win, queue transition
                }
                else
                {
                    m_screenTransition.TransitionToScene("MenuScene");
                }
            }
        }
    }

    bool SetNextEnemy() //There must be at least one frame before running
    {
        //if (!m_assetsSwitched && m_enemyListIndex == m_switchOnEnemy)
        //{
        //    m_turnAssetOff.SetActive(false);
        //    m_turnAssetOn.SetActive(true);
        //    m_assetsSwitched = true;
        //}

        if(m_enemyListIndex < m_enemyList.Count) //one off?
        {
            Destroy(m_currentEnemy); //
            m_currentEnemy = Instantiate(m_enemyList[m_enemyListIndex]);
            Debug.Log(m_enemyListIndex);
            Debug.Log(m_screenTransitionIndex);
            m_enemyListIndex++;
            LoadAudioEvents();
            m_activeBattle = true;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ReceiveKey(TimedNote a_note)
    {
        //send the keypress notifications to the following scripts
        SpellSystem.Instance.ReceiveKey(a_note);
        SoundManager.Reference.ReceiveNote(a_note);
        NoteVisualiser.Reference.ReceiveNote(a_note);
        if (a_note.m_playerOwned == true)
        {
            if (a_note.m_note == Note.A)
                m_player.GetComponent<Musician>().Animate((short)Random.Range(1.0f, 7.0f));
            else if (a_note.m_note == Note.B)
                m_player.GetComponent<Musician>().Animate((short)Random.Range(1.0f, 7.0f));
            else if (a_note.m_note == Note.C)
                m_player.GetComponent<Musician>().Animate((short)Random.Range(1.0f, 7.0f));
            else if (a_note.m_note == Note.D)
                m_player.GetComponent<Musician>().Animate((short)Random.Range(1.0f, 7.0f));
            else if (a_note.m_note == Note.E)
                m_player.GetComponent<Musician>().Animate((short)Random.Range(1.0f, 7.0f));
        }
        else
        {
            m_currentEnemy.GetComponent<Musician>().Animate((short)Random.Range(1, 4));
        }
    }
    public void ReceiveTurnOver()
    {

        //notify these scripts that the casting turn is over
        SpellSystem.Instance.CastSpells();
    }
    public void DealDamage(int a_damage, bool a_toPlayer, bool a_animate = true)
    {
        //deal damage to the approriate character in the scene
        if (a_toPlayer)
        {
            TheBard playerRef = m_player.GetComponent<TheBard>();
            playerRef.TakeDamage(a_damage);
            TextGen.Instance.TakeDamage(a_damage * -1, playerRef.transform.position, m_player.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.y);
            if (a_animate)
            {
                playerRef.Animate((short)Random.Range(17.0f,18.0f));
            }
        }
        else
        {
            Musician slimeRef = m_currentEnemy.GetComponent<Musician>();
            slimeRef.TakeDamage(a_damage);
            TextGen.Instance.TakeDamage(a_damage * -1, slimeRef.transform.position, m_currentEnemy.GetComponentInChildren<MeshRenderer>().bounds.size.y);
            if (a_animate)
                slimeRef.Animate(6);
        }
    }
    public void AccuracyText(float a_accuracy)
    {
        if (a_accuracy >= 90)
            TextGen.Instance.DisplayRating("Perfect", new Vector2(MusicSlider.Position.x, 195), 1, Color.white);
        else if (a_accuracy >= 80 && a_accuracy < 90)
            TextGen.Instance.DisplayRating("Great", new Vector2(MusicSlider.Position.x, 195), 1, Color.yellow);
        else if (a_accuracy >= 70 && a_accuracy < 80)
            TextGen.Instance.DisplayRating("Good", new Vector2(MusicSlider.Position.x, 195), 1, Color.blue);
        else if (a_accuracy >= 60 && a_accuracy < 70)
            TextGen.Instance.DisplayRating("Okay", new Vector2(MusicSlider.Position.x, 195), 1, Color.magenta);
        else
            TextGen.Instance.DisplayRating("Poor", new Vector2(MusicSlider.Position.x, 195), 1, Color.red);
    }
    public void LoadAudioEvents()
    {
        int arrayMod = (m_enemyListIndex * 4);
        Debug.Log(m_enemyListIndex);
        FMODUnity.StudioEventEmitter[] enemyEvents = {null,null,null,null};
        enemyEvents[0] = m_eventBank.GetComponentsInChildren<FMODUnity.StudioEventEmitter>()[m_enemyListIndex - 1];
        enemyEvents[1] = m_eventBank.GetComponentsInChildren<FMODUnity.StudioEventEmitter>()[m_enemyListIndex];
        enemyEvents[2] = m_eventBank.GetComponentsInChildren<FMODUnity.StudioEventEmitter>()[m_enemyListIndex + 1];
        enemyEvents[3] = m_eventBank.GetComponentsInChildren<FMODUnity.StudioEventEmitter>()[m_enemyListIndex + 2];
        m_currentEnemy.GetComponent<Musician>().AddEvents(enemyEvents);
    }
    public GameObject PlayerRef
    {
        get { return m_player; }
    }
    public GameObject EnemyRef
    {
        get { return m_currentEnemy; }
    }
}

[System.Serializable]
struct TransitionScreens
{
    public bool m_useTransition;
    public List<Texture2D> m_textures;
}