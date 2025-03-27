using UnityEngine;
using UnityEngine.UIElements;

public class ExampleResizer: PointerManipulator
{
    private Vector3 m_Start;
    protected bool m_Active;
    private int m_PointerId;
    private Vector2 m_StartSize;

    public ExampleResizer()
    {
        m_PointerId = -1;
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
        m_Active = false;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<WheelEvent>(OnPointerMove);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        target.UnregisterCallback<WheelEvent>(OnPointerMove);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected void OnPointerDown(PointerDownEvent e)
    {
        if (m_Active)
        {
            e.StopImmediatePropagation();
            return;
        }

        if (CanStartManipulation(e))
        {
            m_Start = e.localPosition;
            m_StartSize = target.layout.size;
            m_PointerId = e.pointerId;

            m_Active = true;
            target.CapturePointer(m_PointerId);
            e.StopPropagation();
        }
    }

    protected void OnPointerMove(WheelEvent e)
    {
        if (!m_Active || !target.HasPointerCapture(m_PointerId)) return;

        target.style.height = target.style.height.value.value + e.delta.y;
        target.style.width = target.style.width.value.value + e.delta.y;

        e.StopPropagation();
    }

    protected void OnPointerUp(PointerUpEvent e)
    {
        if (!m_Active || !target.HasPointerCapture(m_PointerId) || !CanStopManipulation(e))
        {
            return;
        }

        m_Active = false;
        target.ReleasePointer(m_PointerId);
        m_PointerId = -1;
        e.StopPropagation();
    }
}