using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttach : MonoBehaviour
{
    public Spell spell;
    private SpellDisplay _display;
    private SpellBehaviour _behaviour;

    // Start is called before the first frame update
    void Start()
    {
        _display = this.GetComponent<SpellDisplay>();
        _behaviour = this.GetComponent<SpellBehaviour>();

        if(_display == null) Debug.LogError("No SpellDisplay Component found.");
        else _display.SetSpell(spell);
        if(_behaviour == null) Debug.LogError("No SpellBehaviour Component found.");
        else _behaviour.SetSpell(spell);
    }
}
