using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell", order = 0)]
public class Spell : ScriptableObject 
{
    // Defines aquí todo lo que define a un hechizo
    public new string name;
    public string level;

    public float speed;
    public int damage; 

    // Esto es simplemente cómo se ve en las opciones del jugador
    public Sprite thumbnail;

    // Aquí falta una animación del hechizo moviéndose
    // y alome un efecto visual al impactar
    public AnimationClip idleAnimation;

    // Averiguar lo de si está en primer, segundo o tercer lugar.
    // Primer lugar: sprite
    // Segundo lugar: elemento
    // Tercer lugar: efecto especial

    /*
    Para el efecto especial, he pensado guardar los posibles efectos (funciones)
    en un script aparte y que el efecto especial sea un número o algo, que nos
    permita acceder a una función del script.
    Ej.: EfectoEspecial = 3 -> FuncionScript = AumentarDaño(Spell)
    */
}
