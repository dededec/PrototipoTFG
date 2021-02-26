using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _health;



    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpellManager _spellManager;
    [SerializeField] private Animator _animator;
    // private float lastTranslationX;
    private float lastHorizontal;



    // Start is called before the first frame update
    void Start()
    {
        _spellManager.SetPlayerID(GetInstanceID());
        // lastTranslationX = 0.0f;
        lastHorizontal = 0.0f;
        _health = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _spellManager.SelectSpell();
        }

        if(Input.GetMouseButtonDown(0))
        {
            _spellManager.ShootMix(transform.position);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            _spellManager.ShootSingle(transform.position);
        }
    }

    private void FixedUpdate() 
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        _rigidbody.MovePosition(_rigidbody.position + (Vector2.up * vertical * _speed * Time.deltaTime) + (Vector2.right * horizontal * _speed * Time.deltaTime));

        _animator.SetFloat("speed", Mathf.Abs(vertical) + Mathf.Abs(horizontal)); // Será mayor que 0 cuando uno de los dos sea mayor que 0 (OR Lógico) -> El jugador se mueve

        // Hay que apañar que en idle se quede orientado también
        // He probado que _animator.GetFloat("speed") != 0 y que translation.x != 0 y no furula ninguna
        // De todas formas tampoco es super necesario ahora mismo        
        // Debug.Log("Sign: Horizontal= " + Mathf.Sign(horizontal) + " ; LastHorizontal= " + Mathf.Sign(lastHorizontal) + " ; ValorHorizontal: " + horizontal);
        if(Mathf.Sign(horizontal) != Mathf.Sign(lastHorizontal) && horizontal != 0.0f){ 
            Vector3 lScale = transform.localScale;
            lScale.x *= -1;
            transform.localScale = lScale;
            lastHorizontal = horizontal;
        }
        
          
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Spell")
        {
            if(other.gameObject.GetComponent<SpellBehaviour>().GetPlayerID() != GetInstanceID())
            {
                Debug.Log("OUCH");
                _health -= other.gameObject.GetComponent<SpellAttach>().spell.damage;
                if(_health <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
            
        }
    }
}
