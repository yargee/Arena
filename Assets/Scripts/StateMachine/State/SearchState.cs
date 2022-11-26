using System.Collections.Generic;

public class SearchState : State
{
    private void OnEnable()
    {
        Fighter.Targeter.TakeAttackerAsTarget(Fighter);
    }
}
