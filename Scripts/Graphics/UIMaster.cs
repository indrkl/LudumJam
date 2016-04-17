using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMaster : MonoBehaviour {
    public Image hpBar;
    ControllerPlayer player;

    void Start()
    {
        player = ControllerPlayer.instance;
    }

    void Update()
    {
        hpBar.fillAmount = player.curLife / player.maxLife;
    }
}
