using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullsColor : MonoBehaviour
{
    public enum ColorType
    {
        Purple,
        Green,
        Orange,
        Blue,
        Silver,
        Black,
        Any,
        Count
    }
    
    [System.Serializable]
    public struct SkullPrefab
    {
        public ColorType color;
        public GameObject prefab;
    };

    public SkullPrefab[] skullPrefab;

    private ColorType color;

    public ColorType Color
    {
        get { return color; }
        set { SetColor(value); }
    }

    public int NumColors
    {
        get { return skullPrefab.Length; }
    }

    private SpriteRenderer sprite;
    private Dictionary<ColorType, GameObject> colorPrefabDict;

    void Awake()
    {
        sprite = transform.Find("piece").GetComponent<SpriteRenderer>();

        colorPrefabDict = new Dictionary<ColorType, GameObject>();

        for (int i = 0; i < skullPrefab.Length; i++)
        {
            if (!colorPrefabDict.ContainsKey (skullPrefab[i].color))
            {
                colorPrefabDict.Add(skullPrefab[i].color, skullPrefab[i].prefab);
            }
        }
    }

    public void SetColor(ColorType newColor)
    {
        color = newColor;

        if (colorPrefabDict.ContainsKey (newColor))
        {
            //sprite.sprite = colorPrefabDict [newColor];
        }
    }
}
