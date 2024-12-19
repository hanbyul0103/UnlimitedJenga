using UnityEngine;

public class TrashCanUI : MonoBehaviour
{
    public int Block { get; private set; }
    public bool canDestroy = false;

    private void Awake()
    {
        Block = LayerMask.NameToLayer("Block");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Block)
        {
            if (canDestroy)
            {
                SoundManager.Instance.PlaySFX("MainBlockCome");
                Destroy(collision.gameObject);
            }
        }
    }
}
