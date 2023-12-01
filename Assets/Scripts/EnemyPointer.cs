using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;
    private GameObject pointerGameObject;
    private Sprite arrow;
    private RectTransform pointerRectTransform;
    private Camera uiCamera;

    public EnemyPointer(Transform enemyTransform, GameObject pointerGameObject, Sprite arrow, Camera uiCamera)
    {
        this.enemyTransform = enemyTransform;
        this.pointerGameObject = pointerGameObject;
        this.arrow = arrow;

        pointerRectTransform = pointerGameObject.GetComponent<RectTransform>();
        this.uiCamera = uiCamera;
    }

    public void Update()
    {
        Vector3 pointerScreenPosition = Camera.main.WorldToScreenPoint(enemyTransform.position);
        bool isOffScreen = pointerScreenPosition.x <= 0 ||
                           pointerScreenPosition.x >= Screen.width ||
                           pointerScreenPosition.y <= 0 ||
                           pointerScreenPosition.y >= Screen.height;

        if (isOffScreen)
        {
            RotatePointerTowardsEnemy();

            float margin = 50;
            Vector3 cappedPointerScreenPosition = pointerScreenPosition;
            cappedPointerScreenPosition.x = Mathf.Clamp(cappedPointerScreenPosition.x, margin, Screen.width - margin);
            cappedPointerScreenPosition.y = Mathf.Clamp(cappedPointerScreenPosition.y, margin, Screen.height - margin);

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedPointerScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
    }

    private void RotatePointerTowardsEnemy()
    {
        Vector3 toPosition = enemyTransform.position;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
