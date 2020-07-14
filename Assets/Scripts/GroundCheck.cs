using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static bool IsGrounded(Collider2D checkedItem, float extraHeight){
        LayerMask ground = LayerMask.GetMask("Floor");
        RaycastHit2D rayCheck = Physics2D.BoxCast(checkedItem.bounds.center, checkedItem.bounds.size, 0f, Vector3.down, extraHeight, ground);
        Color rayColor;
        if (rayCheck.collider != null){
            rayColor = Color.green;
        }else{
            rayColor = Color.red;
        }
        Debug.DrawRay(checkedItem.bounds.center + new Vector3(checkedItem.bounds.extents.x, 0),  Vector3.down * (checkedItem.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(checkedItem.bounds.center - new Vector3(checkedItem.bounds.extents.x, 0),  Vector3.down * (checkedItem.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(checkedItem.bounds.center - new Vector3(checkedItem.bounds.extents.x, checkedItem.bounds.extents.y + extraHeight),  Vector3.right * (checkedItem.bounds.extents.y + extraHeight), rayColor);

        return rayCheck.collider != null;
    }
}
