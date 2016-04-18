using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour {
    public Image hpBar;
    ControllerPlayer player;
    public Sprite NoIcon;

    public Image[] slots;

    public GameObject DeathPanel;

    void Start()
    {
        player = ControllerPlayer.instance;
    }

    void Update()
    {
        if (!player)
        {
            DeathPanel.SetActive(true);
            return;
        }

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
    public string sceneName;
    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
