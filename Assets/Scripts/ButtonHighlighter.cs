using GBP;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlighter : MonoBehaviour
{
    private Button previousButton;
    [SerializeField] private float scaleAmount = 1.1f;
    [SerializeField] public GameObject defaultButton;
    public GameObject[] parentFilters;
    public GameObject selector;
    private RectTransform _rectTransform, _currentTarget;

    private bool _isDisabled;


    private void Start()
    {
        _isDisabled = !GBPManager.IsTV();

        if (!_isDisabled)
        {
            createSelector();
        }

        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton);
        }
    }

    private void createSelector()
    {
        if (_rectTransform == null && !_isDisabled)
        {
            //GameObject framePrefab = Resources.Load("Selector") as GameObject;
            GameObject framePrefab = selector;
            GameObject obj = Instantiate(selector, transform.position, transform.rotation);
            if(framePrefab == null)
            {
                Debug.Log("It is null");
            }
            if (obj)
            {
                _rectTransform = obj.GetComponent<RectTransform>();
                //  _selectorAnim = obj.GetComponent<BreathingEffect>();
                obj.transform.SetParent(transform);
            }
        }
    }

    private void OnEnable()
    {
        if (defaultButton == null) return;
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    private void Update()
    {
        if (_isDisabled) return;

        var selectedObj = EventSystem.current.currentSelectedGameObject;

        if (!selectedObj) return;

        var selectedAsButton = selectedObj.GetComponent<Button>();
        if (selectedObj.transform.name == "Slider")
        {
            selectedAsButton = selectedObj.transform.Find("Button").GetComponent<Button>();
        }


        if (selectedAsButton && selectedAsButton != previousButton)
        {
            //Debug.Log("Button now" + selectedAsButton, selectedObj);
            if (selectedAsButton.transform.name != "PauseButton")
                HighlightButton(selectedAsButton);
            if (previousButton && previousButton.transform.name == "Video")
                UnHighlightButton(previousButton);
        }
        _rectTransform.position = _currentTarget.TransformPoint(_currentTarget.rect.center);

        previousButton = selectedAsButton;
    }

    private void OnDisable()
    {
        if (_rectTransform != null)
            _rectTransform.localScale = Vector3.zero;
        previousButton = null;
        //   if (previousButton != null) UnHighlightButton(previousButton);
    }

    private void HighlightButton(Button btn)
    {
        if (!_rectTransform) createSelector();

        if (_rectTransform)
        {
            var target = btn.GetComponent<RectTransform>();
            _currentTarget = target;
            if (target.name == "Video")
            {
                btn.transform.localScale = new Vector3(scaleAmount, scaleAmount, scaleAmount);
                _rectTransform.localScale = Vector3.zero;
                /*if (_selectorAnim)
                    _selectorAnim.SetScale(_rectTransform.localScale);*/
                return;
            }

            bool setParent = true;
            if (parentFilters != null)
            {
                foreach (GameObject go in parentFilters)
                {
                    if (go && go.name == target.parent.name)
                    {
                        setParent = false;
                        break;
                    }
                }
            }

            if (target.parent.gameObject.TryGetComponent(out HorizontalLayoutGroup hl) ||
                target.parent.gameObject.TryGetComponent(out VerticalLayoutGroup vl))
            {
                setParent = false;
            }

            if (setParent)
                _rectTransform.SetParent(target.parent);
            else _rectTransform.SetParent(target.parent.parent);
            _rectTransform.localScale = target.localScale;

            _rectTransform.position = target.TransformPoint(target.rect.center);

            _rectTransform.sizeDelta = target.sizeDelta;

            _rectTransform.SetAsLastSibling();
        }
        else
        {
            btn.transform.localScale = new Vector3(scaleAmount, scaleAmount, scaleAmount);
        }
    }

    private void UnHighlightButton(Button butt)
    {
        butt.transform.localScale = new Vector3(1, 1, 1);
    }
}