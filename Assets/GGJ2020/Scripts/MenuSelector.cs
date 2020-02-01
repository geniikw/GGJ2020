using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSelector : MonoBehaviour, IPointerClickHandler
{
    public GameObject menuSet;

    public GameObject BulletPrefab;

    public Transform firePosition;
    public Transform bulletEnd;


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
        Debug.Log("dd");
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

        
        
        go.transform.SetParent(FindObjectOfType<BulletTransform>().transform);
        go.GetComponent<MonoBehaviour>().Move(bulletEnd, 1f);
        Destroy(go,2f);
        menuSet.SetActive(false);
    }



    [TestMethod]
    public void SetFAlse()
    {
        this.enabled = false;
    }

}
