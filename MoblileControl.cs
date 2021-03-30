using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoblileControl : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    [SerializeField] private Image _joystickBackGround;
    [SerializeField] private Image _joystick;
    private Vector2 _inputvector;
    private void Start()
    {
        _joystickBackGround.GetComponent<Image>();
        _joystick.transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackGround.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / _joystickBackGround.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _joystickBackGround.rectTransform.sizeDelta.y);

            _inputvector = new Vector2(pos.x * 2 , pos.y * 2 );
            _inputvector = (_inputvector.magnitude > 1.0f) ? _inputvector.normalized : _inputvector;

            _joystick.rectTransform.anchoredPosition = new Vector2(_inputvector.x * (_joystickBackGround.rectTransform.sizeDelta.x / 2), _inputvector.y * (_joystickBackGround.rectTransform.sizeDelta.y / 2));
        }
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        _inputvector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public float Horizontal()
    {
        if (_inputvector.x != 0) return _inputvector.x;
        else return (Input.GetAxis("Horizontal"));
    }
    public float Vertical()
    {
        if (_inputvector.y != 0) return _inputvector.y;
        else return (Input.GetAxis("Vertical"));
    }
}
