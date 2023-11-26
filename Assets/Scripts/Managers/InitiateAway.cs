using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateAway : MonoBehaviour
{
    public CharacterDatabase cb;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(cb.characters[BotoesManager.Instance.escolha2].torcida);
        Instantiate(cb.characters[BotoesManager.Instance.escolha2].character);
    }
}
