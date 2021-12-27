using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private CharacterController characterController;
    private Vector3 movement = Vector3.zero;
    private bool getMousePosition = false;
    private Vector2 mousePosition = Vector2.zero;
    private Vector2 mouseOffset = Vector2.zero;
    private float velocity = 0;
    private Transform raycaster;

    Dictionary<string, Vector3> decorPos = new Dictionary<string, Vector3>(5);
    Dictionary<string, Vector3> decorRot = new Dictionary<string, Vector3>(5);

    public TMP_Text left;
    int score = 30;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        raycaster = GetComponent<Transform>();

        decorPos.Add("TreeToy_Snowman", new Vector3(5.06599998f, 4.42299986f, 14.6940002f));
        decorPos.Add("ChristmasSock", new Vector3(3.64599991f, 4.66499996f, 15.5109997f));
        decorPos.Add("CandyCaneWithBow", new Vector3(4.61299992f, 4.17000008f, 16.2119999f));
        decorPos.Add("CandyCaneWithBow (1)", new Vector3(3.25999999f, 4.17000008f, 14.5699997f));
        decorPos.Add("GiftBox_Square_TypeB", new Vector3(4.33099985f, 4.09901428f, 14.4720001f));
        decorPos.Add("GiftBox_Square_TypeA", new Vector3(4.8499999f, 4.19000006f, 16.1289997f));
        decorPos.Add("GiftBox_Rectangle_TypeA", new Vector3(3.93000007f, 4.09901428f, 15.4899998f));
        decorPos.Add("GiftBox_Cylinder_TypeA", new Vector3(4.83099985f, 4.09901428f, 14.9919996f));
        decorPos.Add("GiftBox_Cylinder_TypeB", new Vector3(5.1f, 4.2f, 15.6f));

        decorPos.Add("Candle_Small", new Vector3(5.10400009f, 4.20499992f, 16.0849991f));
        decorPos.Add("Candle_Small (1)", new Vector3(4.59100008f, 4.11800003f, 16.316f));
        decorPos.Add("Candle_Small (2)", new Vector3(3.67535067f, 4.09901428f, 14.5556364f));
        decorPos.Add("Candle_Small (3)", new Vector3(3.86400008f, 4.09901428f, 14.4499998f));
        decorPos.Add("Candle_Small (4)", new Vector3(3.90799999f, 4.09901428f, 14.2749996f));
        decorPos.Add("Candle_Small (5)", new Vector3(4.79220104f, 4.09901428f, 14.5690002f));
        decorPos.Add("Candle_Small (6)", new Vector3(5.1880002f, 4.09901428f, 15.1330004f));

        decorPos.Add("TreeToy_Bell", new Vector3(4.79099989f, 4.89900017f, 15.7700005f));
        decorPos.Add("TreeToy_Bell (1)", new Vector3(4.76999998f, 4.89900017f, 14.8769999f));
        decorPos.Add("TreeToy_Bell (2)", new Vector3(4.15700006f, 5.48000002f, 15.3620005f));
        decorPos.Add("TreeToy_Bell (3)", new Vector3(4.38100004f, 5.8670001f, 15.0279999f));
        decorPos.Add("TreeToy_Bell (4)", new Vector3(5.06599998f, 6.01499987f, 15.4630003f));

        decorPos.Add("TreeToy_Ball", new Vector3(4.55100012f, 5.83799982f, 15.6990004f));
        decorPos.Add("TreeToy_Ball (1)", new Vector3(5.1329999f, 6.23099995f, 15.0430002f));
        decorPos.Add("TreeToy_Ball (2)", new Vector3(5.03200006f, 5.31500006f, 15.0430002f));
        decorPos.Add("TreeToy_Ball (3)", new Vector3(4.10900021f, 6.11299992f, 15.2489996f));
        decorPos.Add("TreeToy_Ball (4)", new Vector3(4.37799978f, 6.38500023f, 15.4820004f));
        decorPos.Add("TreeToy_Ball (5)", new Vector3(4.375f, 5.28700018f, 15.158f));
        decorPos.Add("TreeToy_Ball (6)", new Vector3(5.78999996f, 5.51999998f, 15.79f));
        decorPos.Add("TreeToy_Ball (7)", new Vector3(3.40499997f, 5.82000017f, 15.3120003f));
        decorPos.Add("TreeToy_Ball (8)", new Vector3(4.64699984f, 6.72200012f, 15.0860004f));


        decorRot.Add("TreeToy_Snowman", new Vector3(0f, 117.372f, 0f));
        decorRot.Add("ChristmasSock", new Vector3(-90f, 0f, 0f));
        decorRot.Add("CandyCaneWithBow", new Vector3(90f, 0f, -67.58f));
        decorRot.Add("CandyCaneWithBow (1)", new Vector3(90f, 0f, -130f));
        decorRot.Add("GiftBox_Rectangle_TypeA", new Vector3(0f, -60.52f, 0f));
    }

    void Update()
    {

    }

    public void OnMovement(InputValue ctx)
    {
        Vector2 dir = ctx.Get<Vector2>();
        movement.x = dir.x;
        movement.z = dir.y;

        float buf = movement.x;
        if (characterController.transform.eulerAngles.y >= 135 && characterController.transform.eulerAngles.y < 225)
        {
            movement.z = -movement.z;
            movement.x = -movement.x;            
        }

        if (characterController.transform.eulerAngles.y >= 225 && characterController.transform.eulerAngles.y < 315)
        {
            movement.x = -movement.z;
            movement.z = buf;
        }

        if (characterController.transform.eulerAngles.y >= 45 && characterController.transform.eulerAngles.y < 135)
        {

            movement.x = movement.z;
            movement.z = -buf;
        }
    }

    public void OnRotation(InputValue ctx)
    {
        Vector2 dir = ctx.Get<Vector2>();
        if (!getMousePosition)
        {
            mousePosition = dir;
            getMousePosition = true;
        }
        mouseOffset = dir - mousePosition;
        mousePosition = dir;
    }

    public void FixedUpdate()
    {
        characterController.Move(movement * Time.fixedDeltaTime * speed);


        if (Mathf.Abs(mouseOffset.x) > Mathf.Abs(mouseOffset.y))
            characterController.transform.Rotate(0, mouseOffset.x, 0, Space.World);
        else
        {
            float nextAngle = characterController.transform.eulerAngles.x - mouseOffset.y;
            if (nextAngle <70 || nextAngle>290)
                characterController.transform.Rotate(-mouseOffset.y, 0, 0);
        }
        mouseOffset.y = 0;
        mouseOffset.x = 0;

        if (characterController.transform.position.y > 0.5)
        {
            characterController.Move(new Vector3(0, -0.1f - velocity, 0));
            velocity += 0.001f;
        }
        else
            velocity = 0;

    }


    public void OnPick()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycaster.position, raycaster.forward, out hit, 100))
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.CompareTag("Player"))
            {
                obj.transform.position = decorPos[obj.name];

                if (decorRot.ContainsKey(obj.name))
                    obj.transform.eulerAngles = decorRot[obj.name];
                else
                    obj.transform.eulerAngles = new Vector3(0,0,0);

                score--;
                left.text = score.ToString();

                if (score == 0)
                {
                    left.text = " ";
                }
            }
        }
    }

}
