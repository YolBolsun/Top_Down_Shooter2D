using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    public Sprite pistolSprite;
    public Sprite rifleSprite;
    public Sprite shotgunSprite;
    public Sprite helmetSprite;
    public Sprite armorSprite;
    public Sprite bootsSprite;
    public Sprite grenadeSprite;
    public Sprite otherSprite;

    public Transform pfWorldItem;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
