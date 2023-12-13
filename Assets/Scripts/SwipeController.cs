using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private int maxPage;
    private int currentPage;
    private Vector3 targetPosition;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private RectTransform worlds;

    [SerializeField] private float tweenTime;
    [SerializeField] private LeanTweenType tweenType;
    private float dragThreshould;

    [SerializeField] Image[] navbarImages;
    [SerializeField] Color selected, notSelected;

    private float timeStamp;

    private void Awake()
    {
        currentPage = 1;
        targetPosition = worlds.localPosition;
        dragThreshould = Screen.width / 15;
        UpdateNavBar();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Previous();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Next();
        }
    }

    public void Next()
    {
        if (timeStamp < Time.time)
        {
            if (currentPage < maxPage)
            {
                currentPage++;
                targetPosition += pageStep;
            }
            else
            {
                currentPage = 1;
                targetPosition -= pageStep * (maxPage - 1);
            }
            MovePage();
            timeStamp = Time.time + tweenTime - 0.1f;
        }
    }

    public void Previous() 
    {
        if (timeStamp < Time.time)
        {
            if (currentPage > 1)
            {
                currentPage--;
                targetPosition -= pageStep;
            }
            else
            {
                currentPage = maxPage;
                targetPosition += pageStep * (maxPage - 1);
            }
            MovePage();
            timeStamp = Time.time + tweenTime - 0.1f;
        }
    }

    private void MovePage()
    {
        worlds.LeanMoveLocal(targetPosition, tweenTime).setEase(tweenType);
        UpdateNavBar();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x)
                Previous();
            else
                Next();
        }
        else
        {
            MovePage();
        }
    }

    private void UpdateNavBar()
    {
        foreach (var option in navbarImages)
        {
            option.color = notSelected;
        }
        navbarImages[currentPage - 1].color = selected;
    }
}
