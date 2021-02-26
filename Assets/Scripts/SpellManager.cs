using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpellManager : MonoBehaviour
{

    [SerializeField] private UIManager _uiManager;
    [SerializeField] private int _playerID;

    [SerializeField] private List<Spell> _spells = new List<Spell>();
    [SerializeField] private GameObject _spellPrefab;
    private Spell[] chosenSpells = new Spell[3];

    int selected = 0;
    int numSpellsChosen = 0;

    /* SCROLL VARIABLES */
    [SerializeField] int _mouseScrollSensibility = 0;
    int numUpScrolls = 0;
    int numDownScrolls = 0;

    private void Start() 
    {    
        _spellPrefab.gameObject.SetActive(false);
        _uiManager.InitializeSpellDisplay(_spells);  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ScrollDownSpell();
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ScrollUpSpell();
        }

    }

    /****************** FUNCIONES PÚBLICAS ******************/

    public void SetPlayerID(int id)
    {
        _playerID = id;
    }

    public void SelectSpell()
    {
        if(numSpellsChosen >= 3) return;
        chosenSpells[numSpellsChosen] = _spells[selected];
        numSpellsChosen++;
        _uiManager.SelectSpell(selected);
    }

    public void ShootMix(Vector3 position)
    {

        if(numSpellsChosen <= 0) return;

        if(numSpellsChosen == 1)
        {
            ShootSingle(chosenSpells[0], position);
        }
        else
        {
            //*****************************************************
            // Obviamente hay que hacer que dispare la mezcla con todo lo que ello conlleva
            //*****************************************************
            // Placeholder para lo que haya que hacer
            ShootSingle(position);
        }

        // Vacíamos todo para poder hacer 
        for(int i=0; i<numSpellsChosen; ++i)
        {
            chosenSpells[i] = null;
        }
        _uiManager.EmptySlots();
        numSpellsChosen = 0;
        
    }

    public void ShootSingle(Vector3 position)
    {
        Vector3 direction = Input.mousePosition;
        direction = Camera.main.ScreenToWorldPoint(direction);
        direction.z = 0.0f; // La función de antes pone z a -10 (z de la cámara)
        direction = direction - position;
        direction.Normalize();

        InstantiateSpell(_spells[selected], position, direction);
        
    }

    public void ShootSingle(Spell spell, Vector3 position)
    {
        Vector3 direction = Input.mousePosition;
        direction = Camera.main.ScreenToWorldPoint(direction);
        direction.z = 0.0f; // La función de antes pone z a -10 (z de la cámara)
        direction = direction - position;
        direction.Normalize();

        InstantiateSpell(spell, position, direction);
        
    }


    /****************** FUNCIONES PRIVADAS ******************/

    private void ScrollDownSpell()
    {
        numDownScrolls++;
        if(numUpScrolls != 0) numUpScrolls = 0; // Para un cambio de sentido en el scroll
        if(numDownScrolls >= _mouseScrollSensibility) // Si superas la sensibilidad, cambias de hechizo selected
        {
            selected++;
            if(selected >= _spells.Count) selected = _spells.Count -1;
            // selected = (selected + 1) % _spells.Count; // Esto es para que dé la vuelta
            numDownScrolls = 0;
        }
        
        _uiManager.UpdateSelectedSpell(selected);
    }

    private void ScrollUpSpell()
    {
        numUpScrolls++;
        if(numDownScrolls != 0) numDownScrolls = 0; // Para un cambio de sentido en el scroll
        if(numUpScrolls >= _mouseScrollSensibility) // Si superas la sensibilidad, cambias de hechizo selected
        {
            selected = (selected - 1) % _spells.Count;
            numUpScrolls = 0;
            // if(selected < 0) selected = _spells.Count -1; // Si quieres que dé la vuelta
            if(selected < 0) selected = 0;
        }

        _uiManager.UpdateSelectedSpell(selected);
    }

    private void InstantiateSpell(Spell spellType, Vector3 position, Vector3 direction)
    {
        // Obviamente hay que averiguar que dispare según el hechizo y tal
        GameObject spell = (GameObject) Instantiate(_spellPrefab, position + direction * 0.5f, Quaternion.identity);
        
        spell.GetComponent<SpellAttach>().spell = spellType;
        spell.GetComponent<SpellBehaviour>().SetDirection(direction);
        spell.GetComponent<SpellBehaviour>().SetPlayerID(_playerID);
        
        spell.SetActive(true);
    }
    

}
