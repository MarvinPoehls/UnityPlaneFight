using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPointerWindow : MonoBehaviour
{
    [SerializeField] private Sprite arrow;
    [SerializeField] private Camera uiCamera;

    private List<EnemyPointer> enemyPointerList;

    private void Awake()
    {
        enemyPointerList = new List<EnemyPointer>();
    }
    private void Update()
    {
        foreach (EnemyPointer pointer in enemyPointerList) 
        {
            pointer.Update(); 
        }
    }

    public EnemyPointer CreatePointer(Transform enemyTransform)
    {
        GameObject pointerGameObject = Instantiate(transform.Find("PointerTemplate").gameObject);
        pointerGameObject.transform.SetParent(transform, false);
        pointerGameObject.SetActive(true);
        EnemyPointer enemyPointer = new(enemyTransform, pointerGameObject, arrow, uiCamera);
        enemyPointerList.Add(enemyPointer);
        return enemyPointer;
    }

    public void DestroyPointer(EnemyPointer pointer)
    {
        enemyPointerList.Remove(pointer);
        pointer.DestroySelf();
    }

    public class EnemyPointer
    {
        private Transform enemyTransform;
        private GameObject pointerGameObject;
        private Sprite arrow;
        private RectTransform pointerRectTransform;
        private Image pointerImage;
        private Camera uiCamera;

        public EnemyPointer(Transform enemyTransform, GameObject pointerGameObject, Sprite arrow, Camera uiCamera)
        {
            this.enemyTransform = enemyTransform;
            this.pointerGameObject = pointerGameObject;
            this.arrow = arrow;

            pointerRectTransform = pointerGameObject.GetComponent<RectTransform>();
            pointerImage = pointerGameObject.GetComponent<Image>();
            this.uiCamera = uiCamera;
        }

        public void Update()
        {
            Vector3 pointerScreenPosition = Camera.main.WorldToScreenPoint(enemyTransform.position);
            bool isOffScreen = pointerScreenPosition.x <= 0 ||
                               pointerScreenPosition.x >= Screen.width ||
                               pointerScreenPosition.y <= 0 ||
                               pointerScreenPosition.y >= Screen.height;
            pointerImage.enabled = isOffScreen;

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

        public void DestroySelf()
        {
            Destroy(pointerGameObject);
        }

        public Vector3 GetEnemyPosition()
        {
            return enemyTransform.position;
        }
    }
}
