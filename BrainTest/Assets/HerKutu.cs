using UnityEngine;
using DG.Tweening;


public class HerKutu : MonoBehaviour
{
    public static bool donmeAsamasinda;
    [SerializeField] float AnimKacSaniye;
    
    public void Cevir()
    {
        donmeAsamasinda = true;
        gameObject.transform.DORotate(transform.localEulerAngles + 
            new Vector3(0, 0, 180), AnimKacSaniye).SetEase(Ease.InOutBack).OnComplete(Donuyor_mu);
    }
    
    public void Donuyor_mu()
    {
        
        donmeAsamasinda = false;

    }

}
