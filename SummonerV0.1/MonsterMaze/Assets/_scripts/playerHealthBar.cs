using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthBar : MonoBehaviour {

	private float fillAmount;

    [SerializeField]
	private Image content;
    [SerializeField]
    private float lerpSpeed;
    [SerializeField]
    private Color fullColor;
    [SerializeField]
    private Color lowColor;
    [SerializeField]
    private bool lerpColors;
    [SerializeField]
    private Text valueText;

    void Start()
    {
        if (lerpColors)
        {
            content.color = fullColor;
        }
    }

    //For HP
    public float MaxValue {
		get;
		set;
	}

    //For HP
    public float Value
	{
		set{
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0]+": "+value + "/"+MaxValue;
            fillAmount = Map (value, 0, MaxValue, 0, 1);
		}
	}

    void Update () {
		HandleHpBar ();
  	}

        private void HandleHpBar()
	{
		if (fillAmount != content.fillAmount)
        {
			content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime*lerpSpeed);
		}
        if (lerpColors)
        {
            content.color = Color.Lerp(lowColor, fullColor, fillAmount);
        }
	}

	private float Map(float value, float inMin, float inMax, float outMin,float outMax)
	{
		return (value- inMin)*(outMax-outMin)/(inMax-inMin)+outMin;
		//(78	-	0)*(1	-	0)  /	(230	-	0)  +  0
		// 78  *  1  /  230 = 0.339
	}
}
