using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject pointer = Instantiate(transform.Find("PointerTemplate").gameObject);
        pointer.transform.SetParent(transform, false);
        pointer.SetActive(true);
        EnemyPointer enemyPointer = new(enemyTransform, pointer, arrow, uiCamera);
        enemyPointerList.Add(enemyPointer);
        return enemyPointer;
    }
}
