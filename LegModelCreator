/* Created by Evandro Beqaj
 * Need to have the Animation Rigging package provided by Unity
 * This script creates a limb with its models and rig configuration starting from 3 points
 */

using UnityEngine;
using UnityEngine.Animations.Rigging;

[ExecuteInEditMode]
public class LegModelCreator : MonoBehaviour
{
    public Transform upLegPosition;
    public Transform midLegPosition;
    public Transform lowLegPosition;

    public bool DrawGizmos = true;

    public float firstShapeSize = 0.2f;
    public float secondShapeSize = 0.1f;

    [SerializeField]
    private GameObject model;
    [SerializeField]
    private GameObject upLegPivot;
    [SerializeField]
    private GameObject midLegPivot;
    [SerializeField]
    private GameObject lowLegPivot;

    [SerializeField]
    private GameObject firstShape;
    [SerializeField]
    private GameObject secondShape;

    [SerializeField]
    private RigBuilder rigBuilder;
    [SerializeField]
    private Rig rig;
    [SerializeField]
    private TwoBoneIKConstraint twoBoneIK;

    [SerializeField]
    private GameObject rigGameObject;
    [SerializeField]
    private GameObject twoBoneManager;
    [SerializeField]
    private GameObject hint;

    private void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.color = Color.blue;

            if (upLegPosition != null)
            {
                Gizmos.DrawSphere(upLegPosition.position, 0.05f);
            }

            if (midLegPosition != null)
            {
                Gizmos.DrawSphere(midLegPosition.position, 0.05f);
            }

            if (lowLegPosition != null)
            {
                Gizmos.DrawSphere(lowLegPosition.position, 0.05f);
            }

            if (midLegPosition != null && lowLegPosition != null)
            {
                Gizmos.DrawLine(midLegPosition.position, lowLegPosition.position);
            }

            if (upLegPosition != null && midLegPosition != null)
            {
                Gizmos.DrawLine(upLegPosition.position, midLegPosition.position);
            }
        }
    }

    public void BuildObject()
    {
        Quaternion upLegRotation;
        Quaternion midLegRotation;

        upLegPosition.LookAt(midLegPosition, Vector3.up);
        upLegRotation = upLegPosition.rotation;
        upLegPosition.rotation = Quaternion.identity;

        midLegPosition.LookAt(lowLegPosition, Vector3.up);
        midLegRotation = midLegPosition.rotation;
        midLegPosition.rotation = Quaternion.identity;

        if (model == null)
        {
            model = new GameObject("model");
        }
        
        model.transform.SetParent(transform.parent);

        if (upLegPivot == null)
        {
            upLegPivot = new GameObject("upLegPivot");
        } else
        {
            upLegPivot.transform.SetParent(null);
        }

        upLegPivot.transform.rotation = upLegRotation;
        upLegPivot.transform.position = upLegPosition.position;
        upLegPivot.transform.SetParent(model.transform);

        if (midLegPivot == null)
        {
            midLegPivot = new GameObject("midLegPivot");
        }
        else
        {
            midLegPivot.transform.SetParent(null);
        }

        midLegPivot.transform.rotation = midLegRotation;
        midLegPivot.transform.position = midLegPosition.position;
        midLegPivot.transform.SetParent(upLegPivot.transform);

        if (lowLegPivot == null)
        {
            lowLegPivot = new GameObject("lowLegPivot");
        }
        else
        {
            lowLegPivot.transform.SetParent(null);
        }

        lowLegPivot.transform.rotation = Quaternion.identity;
        lowLegPivot.transform.position = lowLegPosition.position;
        lowLegPivot.transform.SetParent(midLegPivot.transform);

        CreateCube(upLegPivot, midLegPivot, ref firstShape, firstShapeSize);
        CreateCube(midLegPivot, lowLegPivot, ref secondShape, secondShapeSize);

        CreateRig();
    }

    private void CreateCube(GameObject start, GameObject end, ref GameObject model, float size)
    {
        Vector3 distance = start.transform.position - end.transform.position;

        GameObject modelContainer = new GameObject();

        if (model == null)
        {
            model = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }

        modelContainer.transform.SetParent(start.transform);
        modelContainer.transform.localPosition = Vector3.zero;
        modelContainer.transform.localRotation = Quaternion.identity;

        model.transform.SetParent(modelContainer.transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        model.transform.localScale = Vector3.one;

        model.transform.localPosition = new Vector3(0, 0, 0.5f);

        modelContainer.transform.localScale = new Vector3(size, size, distance.magnitude);

        model.transform.SetParent(start.transform);
        model.name = model.transform.parent.name.Replace("Pivot", "Model");

        DestroyImmediate(modelContainer);
    }

    public void CreateRig()
    {
        if (rigBuilder == null)
        {
            rigBuilder = transform.parent.gameObject.AddComponent<RigBuilder>();
        }

        if (rigGameObject == null)
        {
            rigGameObject = new GameObject("rig");
            rigGameObject.transform.SetParent(rigBuilder.transform);
        }

        if (rig == null)
        {
            rig = rigGameObject.AddComponent<Rig>();
            rigBuilder.layers.Add(new RigLayer(rig));
        }

        if (twoBoneManager == null)
        {
            twoBoneManager = new GameObject("ik");
            twoBoneManager.transform.SetParent(rigGameObject.transform);
        }

        twoBoneManager.transform.position = lowLegPosition.position;

        if (twoBoneIK == null)
        {
            twoBoneIK = twoBoneManager.AddComponent<TwoBoneIKConstraint>();
            twoBoneIK.data.root = upLegPivot.transform;
            twoBoneIK.data.mid = midLegPivot.transform;
            twoBoneIK.data.tip = lowLegPivot.transform;
            twoBoneIK.data.target = twoBoneManager.transform;
        }

        if (hint == null)
        {
            hint = new GameObject("hint");
            hint.transform.SetParent(twoBoneManager.transform);
            twoBoneIK.data.hint = hint.transform;
        }

        hint.transform.position = midLegPosition.position + new Vector3(0, 0, 1);
    }
}
