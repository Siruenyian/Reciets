using System.Collections;
using System.Collections.Generic;
using reciets;
using UnityEngine;

public class Cursor_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite clickSprite;
    [SerializeField] private Sprite pointSprite;
    private SpriteRenderer spriteRenderer;
    public LayerMask selectableLayer;
    public Color highlightColor = Color.yellow;
    private Transform selectedObject;
    private Color[] originalColors;
    public Iinteractable Interactable { get; set; }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pointSprite = spriteRenderer.sprite;
        // Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = pointSprite;

        if (selectedObject != null)
        {
            ResetColor(selectedObject);
            selectedObject = null;
        }

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer))
        {
            selectedObject = hit.transform;

            HighlightObject(selectedObject);

            if (Input.GetMouseButtonDown(0))
            {
                spriteRenderer.sprite = clickSprite;
            }
            //  if (Input.GetMouseButtonUp(0))
            // else
            // {
            //     spriteRenderer.sprite = pointSprite;
            // }
        }


    }
    private void HighlightObject(Transform obj)
    {
        if (obj.TryGetComponent(out Renderer renderer))
        {
            originalColors = new Color[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                originalColors[i] = renderer.materials[i].color;
                renderer.materials[i].color = highlightColor;
            }
        }
    }

    private void ResetColor(Transform obj)
    {
        if (obj.TryGetComponent(out Renderer renderer))
        {
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                renderer.materials[i].color = originalColors[i];
            }
        }
    }

    private void OnObjectClicked(Transform obj)
    {
        // Implement your logic when an object is clicked
        Debug.Log("Object clicked: " + obj.name);
    }
}
