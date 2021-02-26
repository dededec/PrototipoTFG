using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public Transform playerTransform;
    public float radioCircunferencia = 15.0f;
    public float lerpT = 0.25f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position, 0.75f);

        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        mouse = mouse - playerTransform.position;
        
        mouse.Normalize();
        mouse *= radioCircunferencia;
        mouse.z = -10.0f; // La función de antes pone z a -10 (z de la cámara)
        transform.position = Vector3.Lerp(transform.position, mouse, lerpT);
       
    }
}
