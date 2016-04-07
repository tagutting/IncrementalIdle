using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {

    public ItemController Item;
    public Player Player;
    public Button HireButton;
    public Text HireText;
    public double Price;
    public Text ManagerNameText;
    public Image ManagerAvatarImage;
    public Text ManagesText;
    public Text PriceText;
    private bool Purchased = false;

    public void click_HireButton() {
        if (!Purchased && Player.Profit >= Price) {
            Purchased = true;
            Player.Profit -= Price;
            SetPriceText();
            SetHireStatus();
            Item.SetAutoManaged(true);
            Player.SetProfitText();
        }
    }

	// Use this for initialization
	void Start () {
        SetPriceText();
        SetHireStatus();
        SetMangageText();
	}
	
	// Update is called once per frame
	void Update () {
        if (!Purchased) {
            SetHireStatus();
        }
	}

    private void SetPriceText() {
        PriceText.text = Purchased ? "Workin' hard" : string.Format("${0:N}", Price);
    }

    private void SetHireStatus() {
        HireButton.interactable = !Purchased && Player.Profit >= Price;
        HireText.text = Purchased ? "Hired!" : "Hire";
    }

    private void SetMangageText() {
        ManagesText.text = string.Format("Manages Item {0}", Item.GetNumber());
    }
}
