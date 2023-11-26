using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateAway : Initiate
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(cb.characters[BotoesManager.Instance.escolha2].torcida);
        player = (GameObject) Instantiate(cb.characters[BotoesManager.Instance.escolha2].character);
        player.transform.localScale = new Vector3(player.transform.localScale.x*fator, player.transform.localScale.y*fator, 1f);
        logo.sprite = cb.characters[BotoesManager.Instance.escolha2].logo.sprite;
    }
}
