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
    SpellEffect
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
    [SerializeField] GameObject m_player, m_currentEnemy; //the characters in the scene
    public bool m_win, m_playing; //bools for the end of the battle
    private float m_winTimer = 5;
    //Behavious
    void Awake()
    {
        Instance = this; //set singleton instance to this
    }
    void Start()
    {
        Application.targetFrameRate = 300; //attampt this framerate
    }
    void Update()
    {
        if(m_currentEnemy.GetComponent<Musician>().Health <= 0)
        {
			m_currentEnemy.GetComponent<Musician>().Animate(7);
            if (m_winTimer <= 0)
                Application.Quit();

            TextGen.Instance.YouWin();
            m_winTimer -= Time.deltaTime;
        }
        if ((TurnTimer.Instance.CurrentTime + 0.35f) * 95.0f * (1.0f / 60.0f) % 1 > 0.9f || (TurnTimer.Instance.CurrentTime + 0.35f) * 95.0f * (1.0f / 60.0f) % 1 < 0.1f)
            TextGen.Instance.DisplayRating("Beat", new Vector2(1, 0), 1, Color.white);
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
                m_player.GetComponent<Musician>().Animate(1);
            else if (a_note.m_note == Note.B)
                m_player.GetComponent<Musician>().Animate(2);
            else if (a_note.m_note == Note.C)
                m_player.GetComponent<Musician>().Animate(3);
            else if (a_note.m_note == Note.D)
                m_player.GetComponent<Musician>().Animate(1);
            else if (a_note.m_note == Note.E)
                m_player.GetComponent<Musician>().Animate(4);
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
                playerRef.Animate(5);
            }
        }
        else
        {
            TheSlime slimeRef = m_currentEnemy.GetComponent<TheSlime>();
            slimeRef.TakeDamage(a_damage);
            TextGen.Instance.TakeDamage(a_damage * -1, slimeRef.transform.position, m_currentEnemy.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.y);
            if (a_animate)
                slimeRef.Animate(6);
        }
    }

    public void AccuracyText(float a_accuracy)
    {
        if (a_accuracy >= 90)
            TextGen.Instance.DisplayRating("Perfect", Vector2.zero, 1, Color.white);
        else if (a_accuracy >= 80 && a_accuracy < 90)
            TextGen.Instance.DisplayRating("Great", Vector2.zero, 1, Color.yellow);
        else if (a_accuracy >= 70 && a_accuracy < 80)
            TextGen.Instance.DisplayRating("Good", Vector2.zero, 1, Color.blue);
        else if (a_accuracy >= 60 && a_accuracy < 80)
            TextGen.Instance.DisplayRating("Okay", Vector2.zero, 1, Color.magenta);
        else
            TextGen.Instance.DisplayRating("Poor", Vector2.zero, 1, Color.red);
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