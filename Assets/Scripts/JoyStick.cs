using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    #region Variables
    [SerializeField] RectTransform backGround = null;
    [SerializeField] RectTransform joyStick = null;

    Vector2 inputVector = Vector2.zero;

    float bgRadius = 0.0f;
    float stickRadius = 0.0f;
    #endregion Variables

    #region Unity Methods
    private void Start()
    {
        bgRadius = backGround.rect.width / 2;
        stickRadius = joyStick.rect.width / 2;
    }
    #endregion Unity Methods

    #region Event Systems Interface
    public void OnDrag(PointerEventData eventData)
    {
        inputVector = (eventData.position - (Vector2)backGround.position);
        inputVector = Vector2.ClampMagnitude(inputVector, bgRadius - stickRadius);

        joyStick.localPosition = inputVector;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joyStick.localPosition = Vector3.zero;
    }
    #endregion Event Systems Interface

    #region Other Methods
    public Vector2 GetInputVector()
    {
        inputVector.Normalize();
        return inputVector;
    }
    #endregion Other Methods
}
