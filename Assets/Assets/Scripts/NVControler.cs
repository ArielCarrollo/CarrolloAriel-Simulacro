using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NVControler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] public int NV = 0;

    // Update is called once per frame
    void Update()
    {
        text.text = "Nivel: " + NV;
    }
}
