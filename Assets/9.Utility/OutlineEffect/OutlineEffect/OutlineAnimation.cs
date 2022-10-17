using UnityEngine;
using OutlineSpace;


public class OutlineAnimation : MonoBehaviour
{

    private OutlineEffect outlineEffect;

    [SerializeField] Color initialColor;
    
    bool pingPong = false;
    private void Awake()
    {
        outlineEffect = FindObjectOfType<OutlineEffect>();

        //initialColor = outlineEffect.lineColor0;
    }


    private void OnEnable()
    {
        outlineEffect.lineColor0 = initialColor;
    }

    void Update()
    {
        Color c = outlineEffect.lineColor0;

        if (pingPong)
        {
            c.a += Time.deltaTime;

            if (c.a >= 1)
                pingPong = false;
        }
        else
        {
            c.a -= Time.deltaTime;

            if (c.a <= 0)
                pingPong = true;
        }

        c.a = Mathf.Clamp01(c.a);
        outlineEffect.lineColor0 = c;
        outlineEffect.UpdateMaterialsPublicProperties();
    }

    private void OnDisable()
    {
        outlineEffect.lineColor0 = initialColor;
    }
}