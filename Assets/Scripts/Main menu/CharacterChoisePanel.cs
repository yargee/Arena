using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoisePanel : MonoBehaviour
{
    private const string M = "Ì";
    private const string F = "Æ";

    [SerializeField] private Fighter[] _maleFighters;
    [SerializeField] private Fighter[] _femaleFighters;
    [SerializeField] private GameObject _menu;
    [SerializeField] private Image _characterView;
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Button _previousBtn;
    [SerializeField] private Button _sexBtn;
    [SerializeField] private Button _approveBtn;

    private Fighter[] _fighters;

    private bool _male = true;

    private int _index = 0;
    public int Index
    {
        get => _index;
        private set
        {
            if (value >= _fighters.Length)
            {
                _index = 0;
            }
            else if(value < 0 )
            {
                _index = _fighters.Length - 1;
            }
            else
            {
                _index = value;
            }
        }
    }

    private void OnEnable()
    {
        _nextBtn.onClick.AddListener(ShowNext);
        _previousBtn.onClick.AddListener(ShowPrevious);
        _sexBtn.onClick.AddListener(ChangeSex);
        _approveBtn.onClick.AddListener(Approve);

        _fighters = _maleFighters;
        ShowCharacter(_index);
    }
    private void OnDisable()
    {
        _nextBtn.onClick.RemoveListener(ShowNext);
        _previousBtn.onClick.RemoveListener(ShowPrevious);
        _sexBtn.onClick.RemoveListener(ChangeSex);
        _approveBtn.onClick.RemoveListener(Approve);
    }

    private void ShowCharacter(int index)
    {
        _characterView.sprite = _fighters[index].Animator.Renderer.sprite;
    }

    private void ShowNext()
    {
        Index++;
        ShowCharacter(_index);
    }

    private void ShowPrevious()
    {
        Index--;
        ShowCharacter(_index);
    }

    private void ChangeSex()
    {
        _male = !_male;
        _sexBtn.GetComponentInChildren<TMP_Text>().text = _male ? M : F;
        _fighters = _male ? _maleFighters : _femaleFighters;
        ShowCharacter(_index);
    }

    private void Approve()
    {
        var player = _fighters[_index];

        var data = new PlayerData(player.Name, player);
        data.Save();
        transform.DOLocalMoveY(2500, 3);
        ShowMenu();
    }

    private void ShowMenu()
    {
        _menu.transform.DOLocalMoveY(-20, 3);
    }

}
