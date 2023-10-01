using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool beingDragged = false;
    public bool isFalling = false;
    public bool isValidDropPosition = false;
    public Vector3 dragstartPos = Vector3.zero;
    private Collider2D innerCollider;
    private Collider2D outerCollider;
    private VoyageController voyageController;
    private Ship ship;

    // Start is called before the first frame update
    void Start()
    {
        innerCollider = transform.Find("Inner").GetComponent<Collider2D>();
        outerCollider = GetComponent<Collider2D>();
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
        else if (isFalling)
        {
            Bounds bounds = outerCollider.bounds;
            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(bounds.min.x, bounds.min.y - 0.01f), Vector2.down);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.name != "Hold 1" && hit.distance < 0.1f)
                {
                    isFalling = false;
                    AudioController.Instance.PlayDrop();
                }
            }
            if (!isFalling) return;
            hits = Physics2D.RaycastAll(new Vector2(bounds.max.x, bounds.min.y - 0.01f), Vector2.down);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject.name != "Hold 1" && hit.distance < 0.1f)
                {
                    isFalling = false;
                    AudioController.Instance.PlayDrop();
                }
            }
        }
    }

    public void OnMouseDown()
    {
        if (voyageController.IsPaused()) return;
        AudioController.Instance.PlayPickUp();
        isFalling = false;
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
        else
        {
            isFalling = true;
        }
    }
}
