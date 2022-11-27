using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CustomAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CustomAnimation[] _availableAnimations;

    public void PlayAnimation(ConstantKeys.Animations name, UnityAction Callback, bool loop = false)
    {
        StopAllCoroutines();
        StartCoroutine(StartAnimation(name, Callback, loop));
    }

    private IEnumerator StartAnimation(ConstantKeys.Animations name, UnityAction Callback = null, bool loop = false)
    {
        var animation = _availableAnimations.FirstOrDefault(x => x.Name == name);

        Debug.Log(animation + " / " + name);

        animation.SetLoop(loop);

        for (int i = 0; i < animation.Lenght; i++)
        {
            _spriteRenderer.sprite = animation.Get(i);
            yield return new WaitForSeconds(1 / animation.Frequency);

            if (i == animation.Lenght - 1 && animation.Loop)
            {
                i = -1;
            }
        }

        if(Callback != null)
        {
            Callback();
        }
    }
}
