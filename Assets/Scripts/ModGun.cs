

using UnityEngine;

public class ModGun : MonoBehaviour
{
    public Transform controller;
    public LayerMask targetLayer;
    public float maxDistance = 100f;

    private LineRenderer line;
    private bool gripHeld;
    private bool triggerPressed;

    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.012f;
        line.endWidth = 0.012f;
        line.material = new Material(
            Shader.Find("Sprites/Default")
        );
        line.enabled = false;
    }

    void Update()
    {
        if (controller == null) return;

        // Connect these to VR controller input later.
        line.enabled = gripHeld;

        if (!gripHeld) return;

        Vector3 start = controller.position;
        Vector3 direction = controller.forward;

        bool validTarget = Physics.Raycast(
            start,
            direction,
            out RaycastHit hit,
            maxDistance,
            targetLayer
        );

        line.SetPosition(0, start);
        line.SetPosition(
            1,
            validTarget
                ? hit.point
                : start + direction * maxDistance
        );

        Color color = validTarget
            ? Color.green
            : Color.red;

        line.startColor = color;
        line.endColor = color;

        if (validTarget && triggerPressed)
        {
        
