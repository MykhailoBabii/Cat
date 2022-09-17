using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class s_WalkerCamera : MonoBehaviour
{
    /*****************************************************************************************
* Game camera script
* 
* @author N-Game Studios Ltd. 
* Research & Development	
* 
*****************************************************************************************/
    public float turnSpeed;
    public float moveSpeed;
    public float zoomSpeed;
    public float mouseTurnMultiplier;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;
    public float curAngleV;
    public float curAngleH;
    private float dz;
    private float dx;
    public float minCorner;
    public float maxCorner;
    public float minTilt;
    public float maxTilt;
    private GameObject myPhotoGO;
    public Camera myOrtoCamera;
    public float smoothTime;
    private Vector3 velocity;
    public virtual void Start()
    {
        this.curAngleV = 0f;
        this.curAngleH = 0f;
        this.minTilt = 0f;
        this.maxTilt = 0f;
        this.myPhotoGO = GameObject.Find("walkerCamera/photo");
    }

    public virtual void moveCamera(float hStep, float vStep)
    {
        Vector3 curentCameraPoint = this.transform.position;
        if (vStep != 0f)
        {
            this.makeVectorProjections(vStep, this.curAngleV);
            this.letUsStep(curentCameraPoint);
        }
        if (hStep != 0f)
        {
            this.makeVectorProjections(hStep, this.curAngleH);
            this.letUsStep(curentCameraPoint);
        }
    }

    public virtual void zoomCamera(float cameraDistance)
    {
        if (cameraDistance != 0f)
        {
            Vector3 curentCameraPoint = this.transform.position;
            if (((curentCameraPoint.y - cameraDistance) > this.minY) && ((curentCameraPoint.y - cameraDistance) < this.maxY))
            {
                this.transform.Translate(Vector3.up * -cameraDistance);
            }
            else
            {
                if ((curentCameraPoint.y - cameraDistance) < this.minY)
                {
                    this.transform.position = new Vector3(this.transform.position.x, this.minY, this.transform.position.z);
                }
                else
                {
                    if ((curentCameraPoint.y - cameraDistance) > this.maxY)
                    {
                        this.transform.position = new Vector3(this.transform.position.x, this.maxY, this.transform.position.z);
                    }
                }
            }
        }
    }

    public virtual void Update()
    {
        float deltaT = Time.deltaTime;
        float cameraDistance = 0;
        //var rotationValue:float = 0;
        float tiltValue = 0; //Угол наклона камеры
        Vector3 curentCameraPoint = this.transform.position;
        Vector3 mPos = Input.mousePosition;
        Vector3 TargetOffset = new Vector3(0, 0.5f, 0);
        if (((mPos.x >= 0) && (mPos.x <= Screen.width)) && ((mPos.y >= 0) && (mPos.y <= Screen.height)))
        {
            if ((Input.GetAxis("Horizontal") != 0f) || (Input.GetAxis("Vertical") != 0f))
            {
                 // moving the character
                float horizontalStep = (Input.GetAxis("Horizontal") * deltaT) * this.moveSpeed;
                float verticalStep = (Input.GetAxis("Vertical") * deltaT) * this.moveSpeed;
                if (verticalStep != 0f)
                {
                    this.makeVectorProjections(verticalStep, this.curAngleV);
                    this.letUsStep(curentCameraPoint);
                }
                if (horizontalStep != 0f)
                {
                    this.makeVectorProjections(horizontalStep, this.curAngleH);
                    this.letUsStep(curentCameraPoint);
                }
            }
            if (Input.GetMouseButton(0))
            {
                float horizontalStepMouse = (((Input.GetAxis("Mouse X") * deltaT) * this.moveSpeed) * -1) * 2.2f;
                float verticalStepMouse = (((Input.GetAxis("Mouse Y") * deltaT) * this.moveSpeed) * -1) * 2.2f;
                if (verticalStepMouse != 0f)
                {
                    this.makeVectorProjections(verticalStepMouse, this.curAngleV);
                    this.letUsStep(curentCameraPoint);
                }
                if (horizontalStepMouse != 0f)
                {
                    this.makeVectorProjections(horizontalStepMouse, this.curAngleH);
                    this.letUsStep(curentCameraPoint);
                }
            }
            if (!Global._isPopUpOpen && !Global._dlgRun)
            {
                if (Input.GetAxisRaw("Mouse ScrollWheel") != 0f)
                {
                    this.myOrtoCamera.orthographicSize = this.myOrtoCamera.orthographicSize - (Input.GetAxisRaw("Mouse ScrollWheel") * this.zoomSpeed);
                }
                else
                {
                    if (Input.GetKey(KeyCode.PageUp))
                    {
                        this.myOrtoCamera.orthographicSize = this.myOrtoCamera.orthographicSize - 0.1f;
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.PageDown))
                        {
                            this.myOrtoCamera.orthographicSize = this.myOrtoCamera.orthographicSize + 0.1f;
                        }
                    }
                }
            }
            if (this.myOrtoCamera.orthographicSize != 0f)
            {
                if (this.myOrtoCamera.orthographicSize < this.minY)
                {
                    this.myOrtoCamera.orthographicSize = this.minY;
                }
                else
                {
                    if (this.myOrtoCamera.orthographicSize > this.maxY)
                    {
                        this.myOrtoCamera.orthographicSize = this.maxY;
                    }
                }
            }
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle = angle + 360f;
        }
        if (angle > 360f)
        {
            angle = angle - 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }

    private void rotateSomeLerpCamera(float curY)
    {
        float inter = ((curY - this.minY) / (this.maxY - this.minY)) * 1f;
        this.myPhotoGO.transform.localRotation = Quaternion.Euler(Mathf.Lerp(this.minCorner, this.maxCorner, inter), 0, 0);
    }

    private void letUsStep(Vector3 cameraPoint)
    {
        if (((((cameraPoint.z + this.dz) >= this.minZ) && ((cameraPoint.z + this.dz) <= this.maxZ)) && ((cameraPoint.x + this.dx) >= this.minX)) && ((cameraPoint.x + this.dx) <= this.maxX))
        {
            this.transform.Translate(this.dx, 0, this.dz, Space.World);
        }
        else
        {
            // ================ for sliding by the ends of field ================
            if ((((cameraPoint.z + this.dz) < this.minZ) && ((cameraPoint.x + this.dx) >= this.minX)) && ((cameraPoint.x + this.dx) <= this.maxX))
            {

                {
                    float _176 = this.minZ;
                    Vector3 _177 = this.transform.position;
                    _177.z = _176;
                    this.transform.position = _177;
                }
                this.transform.Translate(this.dx, 0, 0, Space.World);
            }
            else
            {
                if ((((cameraPoint.z + this.dz) > this.maxZ) && ((cameraPoint.x + this.dx) >= this.minX)) && ((cameraPoint.x + this.dx) <= this.maxX))
                {

                    {
                        float _178 = this.maxZ;
                        Vector3 _179 = this.transform.position;
                        _179.z = _178;
                        this.transform.position = _179;
                    }
                    this.transform.Translate(this.dx, 0, 0, Space.World);
                }
                else
                {
                    if ((((cameraPoint.x + this.dx) < this.minX) && ((cameraPoint.z + this.dz) >= this.minZ)) && ((cameraPoint.z + this.dz) <= this.maxZ))
                    {

                        {
                            float _180 = this.minX;
                            Vector3 _181 = this.transform.position;
                            _181.x = _180;
                            this.transform.position = _181;
                        }
                        this.transform.Translate(0, 0, this.dz, Space.World);
                    }
                    else
                    {
                        if ((((cameraPoint.x + this.dx) > this.maxX) && ((cameraPoint.z + this.dz) >= this.minZ)) && ((cameraPoint.z + this.dz) <= this.maxZ))
                        {

                            {
                                float _182 = this.maxX;
                                Vector3 _183 = this.transform.position;
                                _183.x = _182;
                                this.transform.position = _183;
                            }
                            this.transform.Translate(0, 0, this.dz, Space.World);
                        }
                        else
                        {
                            // ================= finish restricted points ======================
                            if ((cameraPoint.z + this.dz) < this.minZ)
                            {
                                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.minZ);
                            }
                            else
                            {
                                if ((cameraPoint.z + this.dz) > this.maxZ)
                                {
                                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.maxZ);
                                }
                                else
                                {
                                    if ((cameraPoint.x + this.dx) < this.minX)
                                    {
                                        this.transform.position = new Vector3(this.minX, this.transform.position.y, this.transform.position.z);
                                    }
                                    else
                                    {
                                        if ((cameraPoint.x + this.dx) > this.maxX)
                                        {
                                            this.transform.position = new Vector3(this.maxX, this.transform.position.y, this.transform.position.z);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void makeVectorProjections(float oneStep, float mAngle)
    {
        this.dz = 0;
        this.dx = 0;
        if ((mAngle >= 0) && (mAngle < 90))
        {
            this.dz = oneStep * Mathf.Sin(mAngle * Mathf.Deg2Rad);
            this.dx = oneStep * Mathf.Cos(mAngle * Mathf.Deg2Rad);
        }
        else
        {
            if ((mAngle >= 90) && (mAngle < 180))
            {
                this.dz = oneStep * Mathf.Cos((mAngle - 90) * Mathf.Deg2Rad);
                this.dx = -oneStep * Mathf.Sin((mAngle - 90) * Mathf.Deg2Rad);
            }
            else
            {
                if ((mAngle >= 180) && (mAngle < 270))
                {
                    this.dz = -oneStep * Mathf.Sin((mAngle - 180) * Mathf.Deg2Rad);
                    this.dx = -oneStep * Mathf.Cos((mAngle - 180) * Mathf.Deg2Rad);
                }
                else
                {
                    if ((mAngle >= 270) && (mAngle < 360))
                    {
                        this.dz = -oneStep * Mathf.Cos((mAngle - 270) * Mathf.Deg2Rad);
                        this.dx = oneStep * Mathf.Sin((mAngle - 270) * Mathf.Deg2Rad);
                    }
                }
            }
        }
    }

    private void keepOneCircle()
    {
         // vertical axis
        if (this.curAngleV >= 360)
        {
            this.curAngleV = this.curAngleV - 360;
        }
        else
        {
            if (this.curAngleV < 0)
            {
                this.curAngleV = this.curAngleV + 360;
            }
        }
        // horizontal axis
        if (this.curAngleH >= 360)
        {
            this.curAngleH = this.curAngleH - 360;
        }
        else
        {
            if (this.curAngleH < 0)
            {
                this.curAngleH = this.curAngleH + 360;
            }
        }
    }

    public s_WalkerCamera()
    {
        this.smoothTime = 2.5f;
        this.velocity = Vector3.zero;
    }

}