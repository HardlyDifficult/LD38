using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScreen : MonoBehaviour
{
  #region Public Data
  public List<string> creditsMembers = new List<string>();

  public GameObject creditsContentObject;
  public GameObject creditsTextObject;

    public GameObject scrollView;

  // How much time should pass between letters
  public float timePerLetter = 0.1f;
  // How much time the system should wait after a user
  public float restTimePerEnd = 1.0f;

  #endregion

  #region Private Data
  private string _currentText;
  private int _textLength = 0;
  private int _currentTextPosition = 0;
  private float _currentTime = 0.0f;
  private bool _atEndOfLine = false;

  private string _currentCreditsMember = "";
  private int _currentCreditsMemberIndex = 0;

  private Text _currentTextBlock;

  private bool _isDone = false;
  #endregion

  #region Public API
  void Start()
  {
    _currentCreditsMember = creditsMembers[_currentCreditsMemberIndex];
    _textLength = _currentCreditsMember.Length;

    GameObject tStartText = GameObject.Instantiate(creditsTextObject) as GameObject;
    tStartText.transform.SetParent(creditsContentObject.transform);

    tStartText.transform.localPosition = Vector2.zero;

    Text tText = tStartText.GetComponent<Text>();
    tText.text = "";

    _currentTextBlock = tText;

    RectTransform rect = creditsContentObject.GetComponent<RectTransform>();
    rect.sizeDelta = new Vector2(rect.sizeDelta.x, 90);
  }

  void Update()
  {
    if(_isDone)
      return;

    if(_currentCreditsMember != " NONE")
    {
      if(_currentTextPosition < _textLength)
      {
        _currentTime += Time.deltaTime;

        if(!_atEndOfLine)
        {
          if(_currentTime >= timePerLetter)
          {
            char charToAdd = _currentCreditsMember[_currentTextPosition];

            _currentText += charToAdd;

            _currentTextBlock.text = _currentText;

            _currentTextPosition++;
            _currentTime = 0.0f;

            scrollView.transform.FindChild("Scrollbar Vertical").GetComponent<Scrollbar>().value = 0;

            if (_currentTextPosition >= _textLength)
            {
              // next member
              _currentTextPosition = 0;
              _atEndOfLine = true;
              _currentText = "";
            }
          }
        }
        else
        {
          if(_currentTime >= restTimePerEnd)
          {
            _currentCreditsMemberIndex++;

            if(_currentCreditsMemberIndex >= creditsMembers.Count)
            {
              _isDone = true;
              return;
            }

            //spawn new block
            GameObject tNewText = GameObject.Instantiate(creditsTextObject) as GameObject;
            tNewText.transform.SetParent(creditsContentObject.transform);

            tNewText.transform.localPosition = Vector2.zero;

            RectTransform rect = creditsContentObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + 50); // add text size to content panel for scroll

            Text tText = tNewText.GetComponent<Text>();
            tText.text = "";

            _currentTextBlock = tText;

            _textLength = creditsMembers[_currentCreditsMemberIndex].Length;
            _currentCreditsMember = creditsMembers[_currentCreditsMemberIndex];

            _currentTextPosition = 0;
            _currentTime = 0.0f;

            _atEndOfLine = false;
            //creditsContentObject.transform.position += new Vector3(0, 70, 0);
            scrollView.transform.FindChild("Scrollbar Vertical").GetComponent<Scrollbar>().value = 0;
          }
        }
      }
    }
  }

  public void BackButton()
  {
        //TODO: Hook this back up.
    //SoundManager.PlayClick();
    SceneManager.LoadScene("MainMenu");
  }
  #endregion
}
