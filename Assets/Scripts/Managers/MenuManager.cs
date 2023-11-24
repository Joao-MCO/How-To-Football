using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public int posicao1 = 0, posicao2 = 11;
    public float[] posicaoX;
    public float[] posicaoY;
    public GameObject p1, p2;
    private float x, y;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject, 1f);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log(p1.transform.position);
        Debug.Log(p2.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && posicao1 < 6)
        {
            posicao1 -= 6;
        }

        if (Input.GetKeyDown(KeyCode.S) && posicao1 > 5)
        {
            posicao1 += 6;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if(posicao1 == 0 || posicao1 == 6)
            {
                posicao1 += 5;
            }
            else
            {
                posicao1 -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (posicao1 == 5 || posicao1 == 11)
            {
                posicao1 -= 5;
            }
            else
            {
                posicao1 += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.I) && posicao2 < 6)
        {
            posicao2 -= 6;
        }

        if (Input.GetKeyDown(KeyCode.K) && posicao2 > 5)
        {
            posicao2 += 6;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (posicao2 == 0 || posicao2 == 6)
            {
                posicao2 += 5;
            }
            else
            {
                posicao2 -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (posicao2 == 5 || posicao2 == 11)
            {
                posicao2 -= 5;
            }
            else
            {
                posicao2 += 1;
            }
        }

        if(posicao1 > 5)
        {
           x = posicaoX[posicao1 - 6];
           y = posicaoY[1];
        }
        else
        {
            x = posicaoX[posicao1];
            y = posicaoY[0];
        }

        p1.transform.position = new Vector3(x, y, -1);

        if (posicao2 > 5)
        {
            x = posicaoX[posicao2 - 6];
            y = posicaoY[1];
        }
        else
        {
            x = posicaoX[posicao2];
            y = posicaoY[0];
        }

        p2.transform.position = new Vector3(x, y, -1);

        BotoesManager.Instance.Select(posicao1, posicao2);
    }
}
