using UnityEngine;
using System.Collections;

public class BatterAnimation : MonoBehaviour
{
    public Animator BatterAnimator;
    public RectTransform PivotRectT;
    public Transform BatGripT; //TODO - maybe I can just use real x pos of pivot

    public GameObject bat;

    public VoidEvent SwingFinishedEvent;

    private float m_PrevPivotX; //previous pivot position
    private float m_Distance;

    private bool isSwing = false;

    public void Awake()
    {
        m_Distance = BatGripT.position.x - gameObject.transform.position.x;
        m_PrevPivotX = Util.CameraTranform.ScreenToWorldPointCamera(Camera.main, PivotRectT).x;
    }

    public void Update()
    {
        float realPivotX = Util.CameraTranform.ScreenToWorldPointCamera(Camera.main, PivotRectT).x;
        if (realPivotX != m_PrevPivotX)
        {
            bat.SetActive(true);
            transform.position += new Vector3(realPivotX - m_PrevPivotX, 0, 0);
            m_PrevPivotX = realPivotX;
            
        }
    }
    public void EnableSwing()
    {
        isSwing = false;
        
    }
    public void Swing()
    {
        if (!isSwing)
        {
            BatterAnimator.SetTrigger("swing");
            isSwing = true;
            StartCoroutine("wait");
            
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        bat.SetActive(false);
    }

    public void SwingFinished()
    {
        SwingFinishedEvent.Raise();
    }
}
