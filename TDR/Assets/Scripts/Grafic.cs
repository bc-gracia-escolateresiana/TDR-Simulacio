﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grafic : MonoBehaviour
{
    [SerializeField] bool alturaGen;

    [SerializeField] Sprite spritePunt;

    [SerializeField] Text xMinT;
    [SerializeField] Text xMidT;
    [SerializeField] Text xMaxT;
    [SerializeField] Text yMinT;
    [SerializeField] Text yMidT;
    [SerializeField] Text yMaxT;

    [SerializeField] Color color;

    public List<float> punts;

    void Start()
    {
    }

    void Update()
    {

    }

    public void CrearGrafic()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        float xMax = punts.Count;
        float xPosInterval = 400f / xMax;

        float yMax = Mathf.Max(punts.ToArray());
        float yMin = Mathf.Min(punts.ToArray());

        if (alturaGen)
        {
            yMax = 1;
            yMin = -1;
        }

        float x = 0;
        float y = 0;

        GameObject puntAnterior = null;
        for (int i = 0; i < punts.Count; i++)
        {
            x += xPosInterval;
            if (alturaGen)
            {
                y = 200 + 200 * punts[i];
            }
            else
            {
                y = 400 * (punts[i] / yMax);
            }

            GameObject punt = CrearPunt(new Vector2(x, y));
            if(puntAnterior != null)
            {
                CrearLinia(puntAnterior.GetComponent<RectTransform>().anchoredPosition, punt.GetComponent<RectTransform>().anchoredPosition);
            }
            puntAnterior = punt;
        }

        xMaxT.text = ((punts.Count * 30f) / 60f).ToString();
        xMidT.text = ((punts.Count * 30f) / 120f).ToString();

        yMaxT.text = yMax.ToString();
        yMidT.text = ((yMax + 0) / 2).ToString();
    }

    void CrearLinia(Vector2 puntA, Vector2 puntB)
    {
        GameObject linia = new GameObject("Linia", typeof(Image));
        linia.transform.SetParent(transform);

        linia.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.5f);

        RectTransform rectTransform = linia.GetComponent<RectTransform>();

        Vector2 dir = (puntB - puntA).normalized;
        float dist = Vector2.Distance(puntA, puntB);

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(dist, 3);
        rectTransform.anchoredPosition = puntA + dir * dist * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, (Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI));
    }

    GameObject CrearPunt(Vector2 posicio)
    {
        GameObject punt = new GameObject("Punt", typeof(Image));
        punt.transform.SetParent(transform);

        punt.GetComponent<Image>().sprite = spritePunt;
        punt.GetComponent<Image>().color = new Color(color.r, color.g, color.b);

        RectTransform rectTransform = punt.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = posicio;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        return punt;
    }
}