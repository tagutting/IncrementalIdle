using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public Text ProfitText;
    public double Profit;
	// Use this for initialization

	void Start () {
        Profit = 0;
        SetProfitText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetProfitText() {
        ProfitText.text = string.Format("${0:N}", Profit);
    }
}

public static class Extensions {
    public static string NumberUnits(this double number) {
        string units = string.Empty;
        return units;
    }
}