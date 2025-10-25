using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class HelpScreen : MonoBehaviour
{
    [SerializeField] private CharacterMove _walker;
    [SerializeField] private CharacterActions _action;
    [SerializeField] private float _timerShow;
    [SerializeField] private GameObject _helpScreen;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _background;

    private bool _isHidden = false;
    private float _hideTime;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        _walker.Move += OnMove;
        _action.LeaveGround += OnLeaveGround;
    }

    private void OnDisable()
    {
        _walker.Move -= OnMove;
        _action.LeaveGround -= OnLeaveGround;
    }

    private void Update()
    {
        if (_isHidden && _hideTime + _timerShow < Time.time)
            ShowHelpScreen();
        
    }

    private void OnMove(Vector2 vector)
    {
        HideHelpScreen();
    }

    private void OnLeaveGround()
    {
        HideHelpScreen();
    }

    private void HideHelpScreen()
    {
        _hideTime = Time.time;
        if (_isHidden) return;
        _isHidden = true;
        _text.DOFade(0, 1f);
        _background.DOFade(0, 1f).OnComplete(() => _helpScreen.SetActive(false));
    }

    private void ShowHelpScreen()
    {
        _isHidden = false;
        _helpScreen.SetActive(true);
        _text.DOFade(1, 1f);
        _background.DOFade(1, 1f);
    }
}
