using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Todo lo referente a la lógica del hechizo
public class SpellBehaviour : MonoBehaviour
{
    private Spell _spell;

    public Rigidbody2D rb;
    private Vector2 _direction;
    private int _playerID; // Esto identifica al SpellManager que instancia el hechizo (y por tanto a su lanzador)

    private float _speed;
    private float _damage;

    private float _destroyTime = 5f;


    void Start()
    {
        _speed = _spell.speed;
        _damage = _spell.damage;
        
        rb.velocity = _direction * _speed;
        
        Destroy(this.gameObject, _destroyTime);
    }

    public void SetSpell(Spell spell)
    {
        _spell = spell;
    }

    public void SetDirection(Vector2 dir)
    {
        _direction = dir;
    }

    public void SetPlayerID(int id)
    {
        _playerID = id;
    }

    public int GetPlayerID()
    {
        return _playerID;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(other.gameObject.GetComponent<Player>().GetInstanceID() != _playerID) Destroy(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

}


