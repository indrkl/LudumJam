using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMaster : MonoBehaviour {
    public Image hpBar;
    ControllerPlayer player;
    public Sprite NoIcon;

    public Image[] slots;

    void Start()
    {
        player = ControllerPlayer.instance;
    }

    void Update()
    {
        hpBar.fillAmount = player.curLife / player.maxLife;
        int i = 0;
        foreach(Image im in slots)
        {
            if(i >= player.GetComponent<PlayerForm>().AvailableForms.Count)
            {
                im.sprite = NoIcon;
            }
            else
            {
                im.sprite = PlayerForm.forms[ player.GetComponent<PlayerForm>().AvailableForms[i]].icon;
            }
            i++;
        }

    }
}
