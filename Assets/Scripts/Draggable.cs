using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool beingDragged = false;
    public bool isValidDropPosition = false;
    public Vector3 dragstartPos = Vector3.zero;
    private Collider2D innerCollider;
    private VoyageController voyageController;
    private Ship ship;

    // Start is called before the first frame update
    void Start()
    {
        innerCollider = transform.Find("Inner").GetComponent<Collider2D>();
        voyageController = FindObjectOfType<VoyageController>();
        ship = FindObjectOfType<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beingDragged)
        {
            Collider2D[] results = new Collider2D[3];
            // Allow one hit (the hold). Anything more is an invalid position.
            isValidDropPosition = innerCollider.OverlapCollider(new ContactFilter2D().NoFilter(), results) <= 1;
            //Debug.Log(isValidDropPosition ? "Good position...." : "Invalid position!");
            //foreach (Collider2D res in results)
            //{
            //    if (res == null) continue;
            //    Debug.Log(res.gameObject.name);
            //}
            GetComponent<SpriteRenderer>().color = isValidDropPosition ? Color.white : Color.red;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, -1);
        }
    }

    public void OnMouseDown()
    {
        if (voyageController.IsPaused()) return;
        Debug.LogFormat("Clicked on {0}", name);

        /*Supplies supplies = GetComponent<Supplies>();
        if (supplies != null && !ship.ContainsSupplies(supplies) && supplies.Cost > voyageController.Money)
        {
            Debug.Log("Can't afford that!");
            return;
        }*/

        beingDragged = true;
        GetComponent<Rigidbody2D>().simulated = false;
        dragstartPos = transform.position;
    }

    public void OnMouseUp()
    {
        if (!beingDragged) return;
        beingDragged = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<SpriteRenderer>().color =  Color.white;
        if (!isValidDropPosition)
        {
            transform.position = dragstartPos;
        }
    }
}
