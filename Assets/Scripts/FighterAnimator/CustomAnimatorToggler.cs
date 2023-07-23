using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimatorToggler : MonoBehaviour
{
    private ConstantKeys.Animations _currentAnimation;

    public bool IsPlaying(ConstantKeys.Animations currentAnimation)
    {
        return _currentAnimation == currentAnimation;
    }

    public void SetCurrentAnimation(ConstantKeys.Animations currentAnimation)
    {
        _currentAnimation = currentAnimation;
    }
}
