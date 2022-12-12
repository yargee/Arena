using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DamageViewer : MonoBehaviour
{
    private Queue<TMP_Text> _templatesPool = new Queue<TMP_Text>();

    private void OnEnable()
    {
        var templates = GetComponentsInChildren<TMP_Text>(true);

        foreach (var template in templates)
        {
            _templatesPool.Enqueue(template);
        }
    }

    public void ShowDamage(int damage, Vector3 position)
    {
        var template = _templatesPool.Dequeue();
        var pos = Camera.main.WorldToScreenPoint(position);

        template.text = damage.ToString();
        template.fontSize = Mathf.Lerp(10, damage, 0.05f);
        template.color = Color.Lerp(Color.yellow, Color.red, damage / 70);
        template.transform.position = pos;
        template.gameObject.SetActive(true);
        template.transform.DOMoveY(pos.y + 100, 1.5f).OnComplete(() => ReturnTemplate(template));
        template.DOColor(Color.clear, 8);
    }

    private void ReturnTemplate(TMP_Text template)
    {
        template.gameObject.SetActive(false);
        _templatesPool.Enqueue(template);
    }
}
