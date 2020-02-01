using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSelector : MonoBehaviour, IPointerClickHandler
{
    public GameObject menuSet;

    public GameObject BulletPrefab;

    public Transform firePosition;


    private void Start()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        menuSet.SetActive(true);
        transform.parent.SetAsLastSibling();
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void OnCoreDestroy()
    {
        BGManager.i.GameEnd();

        Destroy(gameObject); //파괴 이팩트?
    }

    public void OnRotate()
    {
        var euler = transform.eulerAngles;
        euler.z += 90f;
        this.Rotation(euler, 0.2f);


    }

    public void Fire()
    {
        BGManager.i.FireScore();
        var go = Instantiate(BulletPrefab);
        go.transform.SetParent(firePosition);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;

        menuSet.SetActive(false);



    }


    [TestMethod]
    public void SetFAlse()
    {
        this.enabled = false;
    }

}
