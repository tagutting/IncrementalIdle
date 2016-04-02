using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class ItemController : MonoBehaviour {
    //UI Components
    public Player Player;
    public Button ProcessButton;
    public Button BuyMoreButton;
    public Text TimerText;
    public Text BuyQuantityText;
    public Text PurchasePriceDecimalText;
    public Text PurchasePriceUnitsText;
    public Text ProfitText;
    public Text QuantityText;
    public Image ProgressPanel;

    //Modifiers
    public double BaseCost;
    public int BaseSecondsToProcess;
    public double BaseProfit;
    public double BaseMultiplier;

    //Private stuff
    private int Quantity;
    private int Number;
    private int SecondsToProcess;
    private double SecondsProcessed = 0;
    private bool IsProcessing = false;
    private bool IsContinuous = false;
    private bool IsProfitPerSecond = false;
    private double Profit = 0;
    private double Multiplier = 1.15;
    private double Cost;

    public void click_ProcessButton() {
        if (!IsProcessing && Quantity > 0) {
            IsProcessing = true;
            SetTimerText();
        }        
    }

    public void click_BuyMoreButton() {
        if (Cost <= Player.Profit) {
            Player.Profit -= Cost;
            Cost *= Multiplier;
            Quantity++;
            Profit = Profit * Multiplier;
            SetQuantityText();
            SetProfitText();
            SetPurchaseText();
            Player.SetProfitText();
        }
    }

    // Use this for initialization
    void Start() {
        string name = gameObject.name;
        if(Regex.IsMatch(name, "\\d+")) {
            Number = Convert.ToInt32(Regex.Match(name, "\\d+").Captures[0].Value);
        } else {
            Number = 0;
        }
        Quantity = (Number == 1) ? 1 : 0;

        SetQuantityText();

        SecondsToProcess = BaseSecondsToProcess;
        SetTimerText();

        Profit = BaseProfit;
        Multiplier = BaseMultiplier;
        SetProfitText();

        Cost = BaseCost;

        if(Cost * 4 < Profit) {
            Cost = Profit / 4 * 3;
        }

        SetPurchaseText();
        SetBuyMoreButtonInteractive();
    }

    // Update is called once per frame
    void Update() {        
        if(IsProcessing) {
            SecondsProcessed += Time.deltaTime;            
            if(SecondsProcessed >= SecondsToProcess) {
                IsProcessing = false;
                SecondsProcessed = 0;
                Player.Profit += Profit;
                Player.SetProfitText();
            }
            SetTimerText();
        }
        SetBuyMoreButtonInteractive();
    }

    private void SetQuantityText() {
        QuantityText.text = Quantity.ToString();
    }

    private void SetTimerText() {
        if (IsProcessing) {
            TimeSpan timeLeft = TimeSpan.FromSeconds(SecondsToProcess - SecondsProcessed);
            TimerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds + 1);
            ProgressPanel.fillAmount = (float)(SecondsProcessed / SecondsToProcess);
        } else {
            TimerText.text = "00:00:00";
            ProgressPanel.fillAmount = 0;
        }
    }

    private void SetProfitText() {
        ProfitText.text = string.Format("{0:N}", Profit);
    }

    private void SetPurchaseText() {
        //Assuming x1 for now
        BuyQuantityText.text = "x1";
        PurchasePriceDecimalText.text = string.Format("{0:N3}", Cost);
        PurchasePriceUnitsText.text = Cost.NumberUnits(); 
    }

    private void SetBuyMoreButtonInteractive() {
        BuyMoreButton.interactable = Player.Profit >= Cost;
    }
}
