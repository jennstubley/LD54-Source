using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    public GameObject tooltip;

    // Start is called before the first frame update
    void Start()
    {
        tooltip = transform.GetChild(0).gameObject;
        tooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            tooltip.SetActive(false);
        }
    }

    public void ShowTooltip(string text, Vector3 worldPosition)
    {
        if (Input.GetMouseButton(0))
        {
            tooltip.SetActive(false);
            return;
        }
        tooltip.SetActive(true);
        tooltip.GetComponentInChildren<TMP_Text>().text = text;
        tooltip.transform.position = worldPosition;
    }

    internal void HideTooltip()
    {
        tooltip.SetActive(false);
    }
}
