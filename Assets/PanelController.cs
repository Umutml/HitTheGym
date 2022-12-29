using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PanelController : MonoBehaviour, IDragHandler
{
    [SerializeField] GameObject player;
    [SerializeField] float panelTurnSpeed;
    [SerializeField] float moveSpeed;

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.instance.isgameStarted && !GameManager.instance.isGameOver)
        {
            Vector3 tempPos = player.transform.position;
            tempPos.x = Mathf.Clamp(tempPos.x + eventData.delta.x * panelTurnSpeed, -4.5f, 4.5f);
            player.transform.position = tempPos;
        }
    }
    private void Update()
    {
        if (GameManager.instance.isgameStarted && !GameManager.instance.isGameOver)
            player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}

