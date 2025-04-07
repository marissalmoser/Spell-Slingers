using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextRise : MonoBehaviour
{
    private RectTransform rt;
    private TMP_Text text;
    private float initialY;
    private Color initialColor;
    [SerializeField] private int riseDistance;
    [SerializeField] private float riseSeconds;
    [SerializeField] private bool destroy;
    [SerializeField] private bool fade;

    public void StartRise(int damage)
    {
        rt = GetComponent<RectTransform>();
        text = GetComponent<TMP_Text>();
        initialY = rt.anchoredPosition.y;

        if (damage > 0)
        {
            text.color = Color.green;
            text.text = "+" + damage.ToString();
        }
        else
        {
            text.text = damage.ToString();
        }

        initialColor = text.color;

        StartCoroutine(Rise());
    }

    private IEnumerator Rise()
    {
        float t = 0;


        while (t < 1)
        {
            t += Time.deltaTime / riseSeconds;
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, Mathf.Lerp(initialY, initialY + riseDistance, t));
            if (fade && (t > 0.5f))
            {
                text.color = initialColor - new Color(0, 0, 0, Mathf.Lerp(0, 1, (t - 0.5f) * 2));
            }
            yield return null;
        }
        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}
