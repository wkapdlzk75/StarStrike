using System;
using System.Collections;
using UnityEngine;

public class UnSafeArea : MonoBehaviour
{
    RectTransform _rectTransformTop;
    RectTransform _rectTransformBottom;
    //RectTransform _rectTransformLeft;
    //RectTransform _rectTransformRight;

    Rect _safeArea;
    Vector2 _minAnchor;
    Vector2 _maxAnchor;

    public GameObject top;
    public GameObject bottom;
    //public GameObject left;
    //public GameObject right;

    public enum ETransform
    {
        top,
        bottom,
        left,
        right
    }

    void Awake()
    {
        _rectTransformTop = top.GetComponent<RectTransform>();
        _rectTransformBottom = bottom.GetComponent<RectTransform>();
        //_rectTransformLeft = left.GetComponent<RectTransform>();
        //_rectTransformRight = right.GetComponent<RectTransform>();

        UnSafeAreaFunc(_rectTransformTop, ETransform.top);
        UnSafeAreaFunc(_rectTransformBottom, ETransform.bottom);

        // UnSafeArea 테스트용
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        while (true)
        {
            UnSafeAreaFunc(_rectTransformTop, ETransform.top);
            UnSafeAreaFunc(_rectTransformBottom, ETransform.bottom);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void UnSafeAreaFunc(RectTransform _rectTransform, ETransform _direction)
    {
        _safeArea = Screen.safeArea;
        _minAnchor = _safeArea.position;
        _maxAnchor = _minAnchor + _safeArea.size;

        Direction(_direction);

        _maxAnchor.x /= Screen.width;

        _rectTransform.anchorMin = _minAnchor;
        _rectTransform.anchorMax = _maxAnchor;
    }

    void Direction(ETransform _direction)
    {
        if (_direction == ETransform.top)
        {
            _minAnchor.y = _maxAnchor.y / Screen.height;
            _maxAnchor.y = 1;
        }
        else if (_direction == ETransform.bottom)
        {
            _maxAnchor.y = _minAnchor.y / Screen.height;
            _minAnchor.y = 0;
        }
        /*else if(left)
        {

        }else if (right)*/
    }

    /*
    public void UnSafeAreaUp()
    {
        _safeArea = Screen.safeArea;
        _minAnchor = _safeArea.position;
        _maxAnchor = _minAnchor + _safeArea.size;

        //_minAnchor.x /= Screen.width;
        _minAnchor.y = _maxAnchor.y / Screen.height;
        //_maxAnchor.x /= Screen.width;
        _maxAnchor.y = 1;

        _rectTransform.anchorMin = _minAnchor;
        _rectTransform.anchorMax = _maxAnchor;
    }

    public void UnSafeAreaDown()
    {
        _safeArea = Screen.safeArea;
        _minAnchor = _safeArea.position;
        _maxAnchor = _minAnchor + _safeArea.size;

        //_minAnchor.x /= Screen.width;
        //_minAnchor.y /= Screen.height;
        //_maxAnchor.x /= Screen.width;
        _maxAnchor.y = _minAnchor.y / Screen.height;
        _minAnchor.y = 0;

        _rectTransform.anchorMin = _minAnchor;
        _rectTransform.anchorMax = _maxAnchor;
    }*/

    // 미사용, 미완성
    /*
    public void UnSafeAreaLeft()
    {
        _safeArea = Screen.safeArea;
        _minAnchor = _safeArea.position;
        _maxAnchor = _minAnchor + _safeArea.size;

        _minAnchor.x /= Screen.width;
        _minAnchor.y /= Screen.height;
        _maxAnchor.x /= Screen.width;
        _maxAnchor.y /= Screen.height;

        _rectTransform.anchorMin = _minAnchor;
        _rectTransform.anchorMax = _maxAnchor;
    }

    public void UnSafeAreaRight()
    {
        _safeArea = Screen.safeArea;
        _minAnchor = _safeArea.position;
        _maxAnchor = _minAnchor + _safeArea.size;

        _minAnchor.x /= Screen.width;
        _minAnchor.y /= Screen.height;
        _maxAnchor.x /= Screen.width;
        _maxAnchor.y /= Screen.height;

        _rectTransform.anchorMin = _minAnchor;
        _rectTransform.anchorMax = _maxAnchor;
    }*/
}
