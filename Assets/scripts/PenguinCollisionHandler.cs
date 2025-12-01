using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PenguinCollisionHandler : MonoBehaviour
{
    public Transform kannettavaEsine;

    [SerializeField] private float distanceAboveHead = 1.2f;
    [SerializeField] private float dropDistance = 1.4f;

    private CharacterController controller;
    private Collider carriedCollider;
    private Rigidbody carriedRigidbody;

    public GameObject carriedItemUIParent;
    public Image itemIconUI;
    public TMP_Text itemLabelUI;

    public Sprite ductTapeIcon;
    public Sprite crowbarIcon;
    public Sprite toolboxIcon;
    public Sprite flashlightIcon;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Hide UI before picking up anything
        if (carriedItemUIParent != null)
            carriedItemUIParent.SetActive(false);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (kannettavaEsine == null && hit.gameObject.CompareTag("Pickable"))
        {
            kannettavaEsine = hit.transform;

            carriedCollider = kannettavaEsine.GetComponent<Collider>();
            if (carriedCollider)
                carriedCollider.enabled = false;

            carriedRigidbody = kannettavaEsine.GetComponent<Rigidbody>();
            if (carriedRigidbody)
            {
#pragma warning disable CS0618
                carriedRigidbody.velocity = Vector3.zero;
#pragma warning restore CS0618
                carriedRigidbody.angularVelocity = Vector3.zero;

                carriedRigidbody.isKinematic = true;
                carriedRigidbody.useGravity = false;
            }

            // Show UI and update icon/text
            UpdateItemUI(hit.gameObject.name);
        }
    }

    void Update()
    {
        if (kannettavaEsine != null)
        {
            Vector3 headPos = transform.TransformPoint(Vector3.up * distanceAboveHead);
            kannettavaEsine.position = headPos;
        }

        if (Input.GetKeyDown(KeyCode.Q) && kannettavaEsine != null)
        {
            if (carriedCollider)
                carriedCollider.enabled = true;

            if (carriedRigidbody)
            {
                carriedRigidbody.isKinematic = false;
                carriedRigidbody.useGravity = true;
            }

            Vector3 dropPos = transform.TransformPoint(Vector3.forward * dropDistance + Vector3.up * 0.5f);
            kannettavaEsine.position = dropPos;

            // Clear carried item
            kannettavaEsine = null;
            carriedCollider = null;
            carriedRigidbody = null;

            // Hide UI when dropping item
            carriedItemUIParent.SetActive(false);
        }
    }

    void UpdateItemUI(string itemName)
    {
        // Show parent UI
        carriedItemUIParent.SetActive(true);

        switch (itemName)
        {
            case "Ilmastointiteippi":
                itemIconUI.sprite = ductTapeIcon;
                itemLabelUI.text = "TEIPPI";
                break;

            case "Tyokalupakki":
                itemIconUI.sprite = toolboxIcon;
                itemLabelUI.text = "TYÖKALUPAKKI";
                break;

            case "Sorkkarauta":
                itemIconUI.sprite = crowbarIcon;
                itemLabelUI.text = "SORKKARAUTA";
                break;

            case "Taskulamppu":
                itemIconUI.sprite = flashlightIcon;
                itemLabelUI.text = "TASKULAMPPU";
                break;

            default:
                itemLabelUI.text = "";
                break;
        }
    }
}
