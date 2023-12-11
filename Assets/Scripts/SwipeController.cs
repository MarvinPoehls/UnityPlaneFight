using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        currentPage = 1;
        targetPosition = worlds.localPosition;
        dragThreshould = Screen.width / 15;
        UpdateNavBar();
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPosition += pageStep;
            MovePage();
        }
    }

    public void Previous() 
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPosition -= pageStep;
            MovePage();
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
