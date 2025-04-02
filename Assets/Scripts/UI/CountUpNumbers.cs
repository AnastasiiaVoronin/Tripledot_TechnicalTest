using UnityEngine;
using DG.Tweening;
using TMPro;

public class CountUpNumbers : MonoBehaviour
{
    [SerializeField]
    private TMP_Text TextField;
    [SerializeField]
    private int LowNumber = 0;
    [SerializeField]
    private int HighNumber = 250;
    [SerializeField]
    private float Time = 1;
    [SerializeField]
    private float Delay = 1;
    public void CountUp()
    {
        //DOTween.Kill("CountUp");
        TextField.text = 0.ToString();
        DOVirtual.Int(LowNumber, HighNumber, Time, v => { TextField.text = v.ToString(); }).SetDelay(Delay).SetId("CountUp");
    }

    public void ResetText()
    {
        DOTween.Rewind("CountUp");
        TextField.text = 0.ToString();
    }
}
