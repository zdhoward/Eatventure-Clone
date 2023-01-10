using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeWorkstationWindow : Window
{
    public static UpgradeWorkstationWindow Instance;

    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI levelLabel;
    [SerializeField] TextMeshProUGUI orderItemNameLabel;
    [SerializeField] TextMeshProUGUI rankLabel;
    [SerializeField] ProgressBar progressBar;
    [SerializeField] TextMeshProUGUI orderItemCostLabel;
    [SerializeField] TextMeshProUGUI orderItemCookTimeLabel;
    [SerializeField] TextMeshProUGUI upgradeCostLabel;

    public bool isOpen { get; private set; }

    [SerializeField] WorkstationUpgrader upgrader;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance of UpgradeWorkstationWindow in this scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        Modifiers.OnModifierChanged += Modifiers_OnModifierChanged;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isOpen)
                CloseWindow();
            else
                OpenWindow(upgrader);
        }
    }

    void LoadDataForWorkstationUpgrader(WorkstationUpgrader upgrader)
    {
        string coinSpriteAsset = "<sprite name=\"CoinIcon\">";
        string timerSpriteAsset = "<sprite=\"timerDark\" index=0>";

        int upgradeLevel = Modifiers.Instance.GetUpgradeLevel(upgrader.orderItem);

        // Load level number
        levelLabel.text = "Level " + upgradeLevel;

        // Load OrderItem name
        orderItemNameLabel.text = upgrader.orderItem.itemName;

        // Load Rank
        int rank = ((upgradeLevel % 25) % 5);
        string rankString = "";
        for (int i = 0; i < rank; i++)
            rankString += "*";
        rankLabel.text = rankString;

        // Load RankProgress
        float progress = (upgradeLevel % 25f) / 25;
        progressBar.SetFillAmount(progress);

        // Load OrderItem Cost
        orderItemCostLabel.text = coinSpriteAsset + upgrader.orderItem.GetCost();

        // Load OrderItem Cooktime
        orderItemCookTimeLabel.text = timerSpriteAsset + upgrader.orderItem.GetCookTime();

        // Load Upgrade Cost
        upgradeCostLabel.text = coinSpriteAsset + Modifiers.Instance.GetUpgradeCost(upgrader.orderItem);

    }

    public void OpenWindow(WorkstationUpgrader upgrader)
    {
        this.upgrader = upgrader;
        LoadDataForWorkstationUpgrader(upgrader);
        panel.SetActive(true);
        isOpen = true;
        CloseWindowsOnClick.Instance.WindowOpened();
    }

    public override void CloseWindow()
    {
        panel.SetActive(false);
        isOpen = false;
    }

    public void TryUpgrade()
    {
        Modifiers.Instance.TryUpgradeLevel(upgrader.orderItem);
    }

    void Modifiers_OnModifierChanged()
    {
        if (isOpen)
            LoadDataForWorkstationUpgrader(upgrader);
    }
}
