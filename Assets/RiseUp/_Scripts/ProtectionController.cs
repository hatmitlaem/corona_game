using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProtectionController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Protection protection;
    private Vector3 lastMousePosition;
    private Vector2 limitSize;
    public bool isDrag = false;
    public float speed = 1f;

    void Start()
    {
        Vector2 canvasSize = transform.parent.GetComponent<RectTransform>().sizeDelta * transform.parent.GetComponent<RectTransform>().localScale.x;
        limitSize = new Vector2(canvasSize.x / 2 - protection.GetComponent<CircleCollider2D>().radius, canvasSize.y / 2 - protection.GetComponent<CircleCollider2D>().radius);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!MainController.IsClassicMode() && MainController.IsLoaded())
        {
            MainController.instance.StartGame();
        }
        if (MainController.IsPlaying())
        {
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDrag = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (MainController.IsPlaying())
        {
            isDrag = true;
        }
    }

    void Update()
    {
        if (isDrag)
        {
            Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 delta = (currMousePos - lastMousePosition).normalized * speed * Time.deltaTime;
            Vector2 newPos = (Vector3)protection.rb.position + delta;
            newPos.x = Mathf.Clamp(newPos.x, -limitSize.x, limitSize.x);
            newPos.y = Mathf.Clamp(newPos.y, -limitSize.y, limitSize.y);

            protection.rb.MovePosition(newPos);
            protection.RemoveVelocity();

            lastMousePosition = currMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
    }
}
