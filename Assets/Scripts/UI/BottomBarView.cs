using UnityEngine;
using DG.Tweening;

public class BottomBarView : MonoBehaviour
{
    #region Selection Animation
    [Header("Selector Movement")]
    [SerializeField]
    private Transform SelectionPanel; 
    private float SelectionPanelStartY; 

    [SerializeField]
    private float SelPanXSpeed = 0.5f; 
    [SerializeField]
    private float SelPanYSpeed = 0.1f; 

    private void Start()
    {
        SelectionPanelStartY = SelectionPanel.position.y;
    }

    public void MoveSelector(Transform ButtonPos)
    {
        if (DOTween.IsTweening("SelectorRetracting") == true) 
        {
            DOTween.Kill("SelectorRetracting");
            CancelContent();
        }
        CurrentButton = ButtonPos;

        if (SelectionPanel.position.y == SelectionPanelStartY)
        {
            SelectionPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 300);
            SelectionPanel.position = new Vector3(ButtonPos.position.x, SelectionPanelStartY, ButtonPos.position.z);
            SelectionPanel.DOMove(ButtonPos.position, SelPanYSpeed);
            DOVirtual.Float(200, 300, SelPanXSpeed / 2, v => { SelectionPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(v, 300); }).SetDelay(0.2f).SetEase(ButtonPanelEase);

            DOVirtual.Float(1000, 1200, SelPanXSpeed, v => { ButtonPanel.sizeDelta = new Vector2(v, 300); }).SetDelay(0.2f).SetEase(ButtonPanelEase);
        }
        else
        {
            SelectionPanel.DOMove(ButtonPos.position, SelPanXSpeed);
        }
    }

    public void RetractSelector(Transform ButtonPos)
    {
        if (SelectionPanel.position.y != SelectionPanelStartY && DOTween.IsTweening("SelectorRetracting") == false)
        {
            SelectionPanel.DOMove(new Vector3(ButtonPos.position.x, SelectionPanelStartY, ButtonPos.position.z), SelPanYSpeed).SetId("SelectorRetracting").SetDelay(0.2f).OnStart(() => DOTween.Kill(""));
         
            DOVirtual.Float(300, 200, SelPanXSpeed / 2, v => { SelectionPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(v, 300); }).SetDelay(0.2f).SetId("SelectorRetracting").SetEase(ButtonPanelEase);
            DOVirtual.Float(1200, 1000, SelPanXSpeed, v => { ButtonPanel.sizeDelta = new Vector2(v, 300); }).SetDelay(0.2f).SetId("SelectorRetracting").SetEase(ButtonPanelEase);

            Invoke("FireContent", 0.2f);
        }
    }
    private GameObject CurrentContent;
    private Transform CurrentButton;

    public void SetCurrentContent(GameObject NewContent)
    {
        CurrentContent = NewContent;
    }
    public void FireContent()
    {
        if (CurrentContent != null)
        {
            CurrentContent.SetActive(!CurrentContent.activeSelf);
        }
        else 
        {
            CurrentButton.DOPunchPosition(Vector3.up * 75, 1f);
        }


        //Debug.Log("FireContent");

    }

    public void CancelContent()
    {
        CancelInvoke("FireContent");
        CurrentContent = null;
    }

    #endregion



    #region Backing And Button Movement
    [Space]
    [Space]
    [Space]
    [Header("Backing And Button Movement")]
    [SerializeField]
    private RectTransform ButtonPanel;

    [SerializeField]
    private Transform FarLeftButton;
    [SerializeField]
    private Transform LeftButton;
    [SerializeField]
    private Transform MiddleButton;
    [SerializeField]
    private Transform RightButton;
    [SerializeField]
    private Transform FarRightButton;

    public void ButtonMoveSequence(Transform CurrentButton)
    {
        DOTween.Kill("RetractSequence");
        DOTween.Kill("MoveSequence");

        Sequence sequence = DOTween.Sequence();

        float Time = 0.2f;
        float MoveAmount = 75f;

        sequence.SetId("MoveSequence");

        if (FarLeftButton == CurrentButton)
        {
            sequence.Insert(0, FarLeftButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, FarLeftButton.DOLocalMoveY(+MoveAmount * 0.75f, Time).SetEase(ButtonUpMoveEase));

            sequence.Insert(0, LeftButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, LeftButton.DOLocalMoveX(+MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, MiddleButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, MiddleButton.DOLocalMoveX(+MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, RightButton.DOLocalMove(Vector3.zero, Time).SetEase(ButtonSideMoveEase));
            sequence.Insert(Time, RightButton.DOLocalMoveX(+MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, FarRightButton.DOLocalMove(Vector3.zero, Time).SetEase(ButtonSideMoveEase));
            sequence.Insert(Time, FarRightButton.DOLocalMoveX(+MoveAmount, Time).SetEase(ButtonSideMoveEase));
        }

        if (LeftButton == CurrentButton)
        {
            sequence.Insert(0, LeftButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, LeftButton.DOLocalMoveY(+MoveAmount * 0.75f, Time).SetEase(ButtonUpMoveEase));

            sequence.Insert(0, FarLeftButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, FarLeftButton.DOLocalMoveX(-MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, MiddleButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, MiddleButton.DOLocalMoveX(+MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, RightButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, RightButton.DOLocalMoveX(+MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, FarRightButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, FarRightButton.DOLocalMoveX(+MoveAmount, Time).SetEase(ButtonSideMoveEase));
        }

        if (MiddleButton == CurrentButton)
        {
            sequence.Insert(0, MiddleButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, MiddleButton.DOLocalMoveY(+MoveAmount * 0.75f, Time).SetEase(ButtonUpMoveEase));

            sequence.Insert(0, FarLeftButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, FarLeftButton.DOLocalMoveX(-MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, LeftButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, LeftButton.DOLocalMoveX(-MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, RightButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, RightButton.DOLocalMoveX(+MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, FarRightButton.DOLocalMove(Vector3.zero, Time).SetEase(ButtonSideMoveEase));
            sequence.Insert(Time, FarRightButton.DOLocalMoveX(+MoveAmount, Time).SetEase(ButtonSideMoveEase));
        }

        if (RightButton == CurrentButton)
        {
            sequence.Insert(0, RightButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, RightButton.DOLocalMoveY(+MoveAmount * 0.75f, Time).SetEase(ButtonUpMoveEase));

            sequence.Insert(0, FarLeftButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, FarLeftButton.DOLocalMoveX(-MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, LeftButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, LeftButton.DOLocalMoveX(-MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, MiddleButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, MiddleButton.DOLocalMoveX(-MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, FarRightButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, FarRightButton.DOLocalMoveX(+MoveAmount, Time).SetEase(ButtonSideMoveEase));
        }

        if (FarRightButton == CurrentButton)
        {
            sequence.Insert(0, FarRightButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, FarRightButton.DOLocalMoveY(+MoveAmount * 0.75f, Time).SetEase(ButtonUpMoveEase));

            sequence.Insert(0, FarLeftButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, FarLeftButton.DOLocalMoveX(-MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, LeftButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, LeftButton.DOLocalMoveX(-MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, MiddleButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, MiddleButton.DOLocalMoveX(-MoveAmount, Time).SetEase(ButtonSideMoveEase));

            sequence.Insert(0, RightButton.DOLocalMove(Vector3.zero, Time));
            sequence.Insert(Time, RightButton.DOLocalMoveX(-MoveAmount, Time).SetEase(ButtonSideMoveEase));
        }
    }

    public void ButtonRetractSequence()
    {
        DOTween.Kill("RetractSequence");
        DOTween.Kill("MoveSequence");

        Sequence sequence = DOTween.Sequence();

        float Time = 0.2f;

        sequence.SetId("RetractSequence");

        sequence.Insert(0, FarLeftButton.DOLocalMove(Vector3.zero, Time));
        sequence.Insert(0, LeftButton.DOLocalMove(Vector3.zero, Time));
        sequence.Insert(0, MiddleButton.DOLocalMove(Vector3.zero, Time));
        sequence.Insert(0, RightButton.DOLocalMove(Vector3.zero, Time));
        sequence.Insert(0, FarRightButton.DOLocalMove(Vector3.zero, Time));
    }
    #endregion



    #region Eases / Animation Styles
    [Space]
    [Space]
    [Space]
    [Header("Backing And Button Eases / Animation Styles ")]
    [SerializeField]
    private Ease ButtonSideMoveEase = Ease.Linear;
    [SerializeField]
    private Ease ButtonUpMoveEase = Ease.Linear;
    [SerializeField]
    private Ease ButtonPanelEase = Ease.Linear;

    #endregion
}
