using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] Image[] spellSlots;
    [SerializeField] Sprite emptySlotSprite;
    int filledSlots = 0;

    public Canvas canvas;

    public Transform playerTransform;

    [SerializeField] RectTransform selectorTransform;

    public GameObject UISpellList;
    [SerializeField] private List<GameObject> _spellDisplay = new List<GameObject>();
    private int selected = 0;

    public void InitializeSpellDisplay(List<Spell> _spells)
    {
        // Siempre vas a tener al menos un hechizo (por diseño del juego)

        int[] positions = new int[_spells.Count];

        int initial = (int)( -100 + 50 * Mathf.Round(_spells.Count/2.0f + 0.1f) ); // El + 0.1f es porque Round, si tienes numPar.5 (0.5, 2.5, 4.5, etc) redondea al nº par
        for(int i=0; i < _spells.Count; ++i ) positions[i] = initial - 50*i;
        
        // Creamos el vector de display según los spells que tengamos
        for (int i = 0; i < _spells.Count; ++i)
        {
            _spellDisplay.Add(new GameObject());
            _spellDisplay[i].transform.SetParent(UISpellList.transform);
            
            RectTransform rt = _spellDisplay[i].AddComponent<RectTransform>();
            rt.sizeDelta = Vector2.one * 32; // (1,1) * 32 = (32,32)
            rt.anchorMin = Vector2.up; // up = (0,1) que es el vector que buscamos
            rt.anchorMax = Vector2.up;
            rt.localScale = Vector3.one;
            rt.anchoredPosition = new Vector3(0, positions[i], 0);

            Image im = _spellDisplay[i].AddComponent<Image>();
            im.sprite = _spells[i].thumbnail;            
        }

        selectorTransform.position = _spellDisplay[0].transform.position; // Lo ponemos al principio de la lista    
        selected = 0;
    }


    public void UpdateSelectedSpell(int selectedParam)
    {
        selected = selectedParam;
        // selectorTransform.position = _spellDisplay[selected].transform.position; // Lo ponemos al principio de la lista    
    }


    public void SelectSpell(int selected)
    {
        Image selectedImage = _spellDisplay[selected].GetComponent<Image>();
        spellSlots[filledSlots].sprite = selectedImage.sprite;
        //spellSlots[filledSlots].color = selectedImage.color; Esto no hace falta porque 1 hechizo = 1 imagen
        
        filledSlots++;
    }

    public void EmptySlots()
    {
        for(int i=0; i<filledSlots; ++i)
        {
            spellSlots[i].sprite = emptySlotSprite;
            spellSlots[i].color = Color.white;
        }
        
        filledSlots = 0;
    }

    private Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }

    private float lastSign = 1f;
    private void Update() {
        UISpellList.transform.position = worldToUISpace(canvas, playerTransform.position);
        selectorTransform.position = _spellDisplay[selected].transform.position;

        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        mouse = mouse - playerTransform.position;
        
        if(Mathf.Sign(mouse.x) != lastSign)
        {
            Vector3 lScale = UISpellList.transform.localScale;
            lScale.x *= -1;
            UISpellList.transform.localScale = lScale;
            lastSign = Mathf.Sign(mouse.x);
        }
        
    }
}
