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

    public GameObject explosionPrefab;


    private void Start()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        menuSet.SetActive(true);
        transform.parent.SetAsLastSibling();
    }

    public void OnDestroyClick()
    {
        AudioManager.i.PlaySound(4);
        enabled = false;
        var go = Instantiate(explosionPrefab);
        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        Destroy(go, 1f);
        Destroy(gameObject, 1f);
    }

    public void OnCoreDestroy()
    {
        AudioManager.i.PlaySound(2);
        Debug.Log("dd");
        BGManager.i.GameEnd();

        Destroy(gameObject); //파괴 이팩트?
    }

    public void OnWin()
    {
        AudioManager.i.PlaySound(3);
        Debug.Log("dd");
        BGManager.i.GameEnd(false);

        Destroy(gameObject); //파괴 이팩트?
    }

    bool isRotate = false;
    public void OnRotate()
    {
        if(isRotate)
            return;
        var euler = transform.eulerAngles;
        euler.z += 90f;
        isRotate = true;
        
        this.Rotation(euler, 0.2f);
        this.StartChain().Wait(0.2f).Call(()=>isRotate = false);

    }

    public void Fire()
    {
        AudioManager.i.PlaySound(1);

        BGManager.i.FireScore();
        var go = Instantiate(BulletPrefab);
        go.transform.SetParent(firePosition);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;



        go.transform.SetParent(FindObjectOfType<BulletTransform>().transform);
        go.GetComponent<MonoBehaviour>().Move(bulletEnd, 1f);
        Destroy(go, 2f);
        menuSet.SetActive(false);
    }



    [TestMethod]
    public void SetFAlse()
    {
        this.enabled = false;
    }

}
