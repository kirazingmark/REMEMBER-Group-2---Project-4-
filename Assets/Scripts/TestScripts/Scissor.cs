using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scissor : MonoBehaviour 
{
    [Header("Standard Rect Size/Position")]
	public Rect scissorRect = new Rect (0,0,1,1);

    [Header("Zoomed Rect Size/Position")]
    public Rect zoomRect = new Rect(0,0,1,1);

    [Header("Zoomed Toggle")]
    public bool isZoomed = false;

    [Header("Window Backing Toggle")]
    public bool windowBacking = false;

    [Header("Backing Images")]
    public Image standardBacking;
    public Image zoomBacking;

    public static void SetScissorRect( Camera cam, Rect r )
	{		
		if ( r.x < 0 )
		{
			r.width += r.x;
			r.x = 0;
		}
		
		if ( r.y < 0 )
		{
			r.height += r.y;
			r.y = 0;
		}
		
		r.width = Mathf.Min( 1 - r.x, r.width );
		r.height = Mathf.Min( 1 - r.y, r.height );			
			 
		cam.rect = new Rect (0,0,1,1);
		cam.ResetProjectionMatrix ();
		Matrix4x4 m = cam.projectionMatrix;

		cam.rect = r;

		Matrix4x4 m1 = Matrix4x4.TRS( new Vector3( r.x, r.y, 0 ), Quaternion.identity, new Vector3( r.width, r.height, 1 ) );

		Matrix4x4 m2 = Matrix4x4.TRS (new Vector3 ( ( 1/r.width - 1), ( 1/r.height - 1 ), 0), Quaternion.identity, new Vector3 (1/r.width, 1/r.height, 1));
		Matrix4x4 m3 = Matrix4x4.TRS( new Vector3( -r.x  * 2 / r.width, -r.y * 2 / r.height, 0 ), Quaternion.identity, Vector3.one );

		cam.projectionMatrix = m3 * m2 * m; 	
	}	

	void OnPreRender () 
	{
        ZoomToggle();
	}

    void Update()
    {
        ZoomControl();
        BackingControl();
    }

    void ZoomToggle()
    {
        if(!isZoomed)
            SetScissorRect(GetComponent<Camera>(), scissorRect);
        else if(isZoomed)
            SetScissorRect(GetComponent<Camera>(), zoomRect);
    }

    void ZoomControl()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isZoomed)
            {
                isZoomed = true;
                if (windowBacking)
                {
                    standardBacking.enabled = false;
                    zoomBacking.enabled = true;
                }
            } 
            else if (isZoomed)
            {
                isZoomed = false;

                if (windowBacking)
                {
                    standardBacking.enabled = true;
                    zoomBacking.enabled = false;
                }
            }
        }
    }

    void BackingControl()
    {
        if (windowBacking)
        {
            if (!isZoomed)
            {
                standardBacking.enabled = true;
                zoomBacking.enabled = false;
            }
            else if (isZoomed)
            {
                standardBacking.enabled = false;
                zoomBacking.enabled = true;
            }
        }
    }
}