using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TouchMove : MonoBehaviour
{
    [SerializeField] bool IsJoystick = true;
    public static Vector3 MoveDir = Vector2.zero;

    [SerializeField] VariableJoystick joystick;

    Vector2 startTouchPosition, currentPosition;
    bool stopTouch = false;
    bool EnableKeyboard = false;

    public float swipeRange;
    public float tapRange;

    private void Start()
    {
#if UNITY_EDITOR
        EnableKeyboard = true;
#elif UNITY_ANDROID && !UNITY_EDITOR
        EnableKeyboard = false;
#endif
    }

    private void Update()
    {
        if (EnableKeyboard)
        {
            // Keyboard Inputs
            if(!IsJoystick)
                KeyboardHandler();
            else
                JoystickHandler();
        }
        else
        {
            //Swipe();
            JoystickHandler();
        }
    }

    void KeyboardHandler()
    {
        MoveDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
    }

    void JoystickHandler()
    {
        MoveDir = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
    }

    void Swipe()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if (!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    MoveDir = new Vector2(-1, 0);
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    MoveDir = new Vector2(1, 0);
                    stopTouch = true;
                }
                else if (Distance.y > swipeRange)
                {
                    MoveDir = new Vector2(0, 1);
                    stopTouch = true;
                }
                else if (Distance.y < -swipeRange)
                {
                    MoveDir = new Vector2(0, -1);
                    stopTouch = true;
                }
            }
            else
            {
                MoveDir = Vector2.zero;
            }
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
        }
    }
}
