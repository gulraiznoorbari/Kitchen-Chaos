using KitchenChaos.Data;
using KitchenChaos.Feature.Interaction;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return _kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this._kitchenObjectParent != null)
        {
            this._kitchenObjectParent.ClearKitchenObject(); // clear the previous counter
        }

        this._kitchenObjectParent = kitchenObjectParent;
        if (!kitchenObjectParent.HasKitchenObject())
        {
            kitchenObjectParent.SetKitchenObject(this);
            transform.parent = kitchenObjectParent.GetCounterTopTransform();
            transform.localPosition = Vector3.zero;
        }
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return this._kitchenObjectParent;
    }
    
    public void DestroyKitchenObject()
    {
        _kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(kitchenObjectParent);
    }
}