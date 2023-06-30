using UnityEngine;
using System.Collections;

public class BatterAnimation : MonoBehaviour
{
    public static BatterAnimation Instance { get; private set; }
    public Animator BatterAnimator;
    public RectTransform PivotRectT;
    public Transform BatGripT; //TODO - maybe I can just use real x pos of pivot

    public GameObject bat;
    public GameObject Leonard;
    public VoidEvent SwingFinishedEvent;

    private float m_PrevPivotX; //previous pivot position
    private float m_Distance;

    private bool isSwing = false;

    public void Awake()
    {
        Instance = this;
        m_Distance = BatGripT.position.x - gameObject.transform.position.x;
        m_PrevPivotX = Util.CameraTranform.ScreenToWorldPointCamera(Camera.main, PivotRectT).x;
    }

    public void Update()
    {
        float realPivotX = Util.CameraTranform.ScreenToWorldPointCamera(Camera.main, PivotRectT).x;
        if (realPivotX != m_PrevPivotX)
        {
            bat.SetActive(true);
            Vector3 positionOffset = new Vector3(realPivotX - m_PrevPivotX, 0, 0);
            transform.position += positionOffset;
            m_PrevPivotX = realPivotX;

            if (positionOffset != Vector3.zero)
            {
                Leonard.transform.position += positionOffset;
                Debug.Log("Player Moved");
            }
        }

        if (Leonard.transform.rotation != Quaternion.Euler(0f, -90f, 0))
        {
            Leonard.transform.rotation = Quaternion.Euler(0f, -90f, 0);
            Debug.Log("Player Rotated");
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
            Leonard.transform.rotation = Quaternion.Euler(0f, -90f, 0);
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
