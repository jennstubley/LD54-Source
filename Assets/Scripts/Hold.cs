using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    private Ship ship;

    // Start is called before the first frame update
    void Start()
    {
        ship = FindObjectOfType<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogFormat("Box placed in {0}", name);
        ship.AddCargo(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.LogFormat("Box removed from {0}", name);
        ship.RemoveCargo(collision.gameObject);
    }
}
