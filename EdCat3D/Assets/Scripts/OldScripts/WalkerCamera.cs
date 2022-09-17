using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class WalkerCamera : MonoBehaviour
{
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
    public virtual void Start()
    {
        this.curAngleV = 90f;
        this.curAngleH = 0f;
        this.minTilt = 30f;
        this.maxTilt = 150f;
        this.myPhotoGO = GameObject.Find("walkerCamera/photo");
    }

    //~ public function resetCameraDirection():void
    //~ {
     //~ iTween.rotateTo(GameObject.Find("character"), {"y":0, "time":1, "delay":0});
     //~ curAngleV = 90.0;
     //~ curAngleH = 0.0;
    //~ }
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

    /*if (Global.isTutorialCanBe(3))
		{
			if (cameraDistance > 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("zoomin")) Global.allRequiredActionsForTutorialComplete_3.Add("zoomin", true); }
			if (cameraDistance < 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("zoomout")) Global.allRequiredActionsForTutorialComplete_3.Add("zoomout", true); }
			
			if (Global.isAllReauiredActionsForTutorialComplete_3())
			{
				Global.globalBus.SendMessage("GameTutorialManager_command_letsNextTutorial", 4);
				Global.globalTutorialCameraAndControlsAllow = false;
			}
		}*/    public virtual void zoomCamera(float cameraDistance)//Global._CameraPos = curentCameraPoint.y;
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

    public virtual void turnCamera(float tValue, float rValue, float zValue)
    {
        this.curAngleV = this.curAngleV + -rValue;
        this.curAngleH = this.curAngleH + -rValue;
        this.keepOneCircle();
        this.transform.Rotate(tValue, rValue, zValue);
    }

    /*if (!Global.isEarthMiniMapEnebled)
			{
				if (Input.GetAxisRaw("Mouse ScrollWheel")) {
					cameraDistance = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;
				} else if (Input.GetKey(KeyCode.PageUp)) {
					cameraDistance = 0.09 * zoomSpeed;
					//Global._CameraPos = curentCameraPoint.y;
				} else if (Input.GetKey(KeyCode.PageDown)) {
					cameraDistance = -0.09 * zoomSpeed;
					//Global._CameraPos = curentCameraPoint.y;
				}
				
				if (Global.isTutorialCanBe(3))
				{
					if (cameraDistance > 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("zoomin")) Global.allRequiredActionsForTutorialComplete_3.Add("zoomin", true); }
					if (cameraDistance < 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("zoomout")) Global.allRequiredActionsForTutorialComplete_3.Add("zoomout", true); }
					
					if (Global.isAllReauiredActionsForTutorialComplete_3())
					{
						Global.globalBus.SendMessage("GameTutorialManager_command_letsNextTutorial", 4);
						Global.globalTutorialCameraAndControlsAllow = false;
					}
				}

				if (cameraDistance) {
					if (((curentCameraPoint.y - cameraDistance) > minY)&&((curentCameraPoint.y - cameraDistance) < maxY)) {
						transform.Translate(Vector3.up * -cameraDistance);
						this.rotateSomeLerpCamera(curentCameraPoint.y);
					} else if ((curentCameraPoint.y - cameraDistance) < minY) {
						transform.position = new Vector3(transform.position.x, minY, transform.position.z);
						//this.rotateSomeLerpCamera(curentCameraPoint.y);
					} else if ((curentCameraPoint.y - cameraDistance) > maxY) {
						transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
						//this.rotateSomeLerpCamera(curentCameraPoint.y);
					}
					Global._CameraPos = curentCameraPoint.y;
				}
			}*/    public virtual void Update()// zooming the character
    {
        float deltaT = Time.deltaTime;
        float cameraDistance = 0;
        float rotationValue = 0;
        float tiltValue = 0; //Угол наклона камеры
        Vector3 curentCameraPoint = this.transform.position;
        Vector3 mPos = Input.mousePosition;
        //Global._CameraPos = curentCameraPoint.y;
        if ((((mPos.x >= 0) && (mPos.x <= Screen.width)) && ((mPos.y >= 0) && (mPos.y <= Screen.height))) && !Screen /*&& 
			(!Global.lockInterface) && 
			(!Global.isTutorialPanelActive) && 
			(!Global.isLevelUpPanelActive) && 
			(Global.isWeSeenFirstPanel)*/.lockCursor)
        {
             /*|| Global.globalTutorialCameraAndControlsAllow*/
             // rotation the character
            if (Input.GetMouseButton(1))
            {
                rotationValue = (Input.GetAxis("Mouse X") * this.turnSpeed) * this.mouseTurnMultiplier;
                this.curAngleV = this.curAngleV + -rotationValue;
                this.curAngleH = this.curAngleH + -rotationValue;
                this.keepOneCircle();
                this.transform.Rotate(0, rotationValue, 0);
            }
            else
            {
                if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))
                {
                    rotationValue = (Input.GetAxis("Horizontal") * this.turnSpeed) * 0.25f;
                    tiltValue = WalkerCamera.ClampAngle((Input.GetAxis("Vertical") * this.turnSpeed) * 0.25f, -15, 15);
                    this.curAngleV = this.curAngleV + -rotationValue;
                    this.curAngleH = this.curAngleH + -rotationValue;
                    this.keepOneCircle();
                }
            }
            //transform.Rotate(ClampAngle(tiltValue,-40,40), rotationValue, 0);
            this.transform.Rotate(WalkerCamera.ClampAngle(tiltValue, -40, 40), rotationValue, 0);

            {
                int _184 = 0;
                Vector3 _185 = this.transform.eulerAngles;
                _185.z = _184;
                this.transform.eulerAngles = _185;
            }
            if ((Input.GetAxis("Horizontal") != 0f) || (Input.GetAxis("Vertical") != 0f))
            {
                // moving the character
                float horizontalStep = (Input.GetAxis("Horizontal") * deltaT) * this.moveSpeed;
                float verticalStep = (Input.GetAxis("Vertical") * deltaT) * this.moveSpeed;
                /*if (Global.isTutorialCanBe(3))
				{
					if (horizontalStep > 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("w")) Global.allRequiredActionsForTutorialComplete_3.Add("w", true); }
					if (horizontalStep < 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("s")) Global.allRequiredActionsForTutorialComplete_3.Add("s", true); }
					
					if (verticalStep > 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("a")) Global.allRequiredActionsForTutorialComplete_3.Add("a", true); }
					if (verticalStep < 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("d")) Global.allRequiredActionsForTutorialComplete_3.Add("d", true); }
					
					if (Global.isAllReauiredActionsForTutorialComplete_3()) 
					{
						Global.globalTutorialCameraAndControlsAllow = false;
						Global.globalBus.SendMessage("GameTutorialManager_command_letsNextTutorial", 4);
					}
				}*/
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
                float verticalStepMouse = (((Input.GetAxis("Mouse Y") * deltaT) * this.moveSpeed) * -1) * 2;
                /*if (Global.isTutorialCanBe(3))
				{
					if (horizontalStepMouse > 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("w")) Global.allRequiredActionsForTutorialComplete_3.Add("w", true); }
					if (horizontalStepMouse < 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("s")) Global.allRequiredActionsForTutorialComplete_3.Add("s", true); }
					
					if (verticalStepMouse > 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("a")) Global.allRequiredActionsForTutorialComplete_3.Add("a", true); }
					if (verticalStepMouse < 0) { if (!Global.allRequiredActionsForTutorialComplete_3.ContainsKey("d")) Global.allRequiredActionsForTutorialComplete_3.Add("d", true); }
					
					if (Global.isAllReauiredActionsForTutorialComplete_3()) 
					{
						Global.globalTutorialCameraAndControlsAllow = false;
						Global.globalBus.SendMessage("GameTutorialManager_command_letsNextTutorial", 4);
					}
				}*/
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
                    float _186 = this.minZ;
                    Vector3 _187 = this.transform.position;
                    _187.z = _186;
                    this.transform.position = _187;
                }
                this.transform.Translate(this.dx, 0, 0, Space.World);
            }
            else
            {
                if ((((cameraPoint.z + this.dz) > this.maxZ) && ((cameraPoint.x + this.dx) >= this.minX)) && ((cameraPoint.x + this.dx) <= this.maxX))
                {

                    {
                        float _188 = this.maxZ;
                        Vector3 _189 = this.transform.position;
                        _189.z = _188;
                        this.transform.position = _189;
                    }
                    this.transform.Translate(this.dx, 0, 0, Space.World);
                }
                else
                {
                    if ((((cameraPoint.x + this.dx) < this.minX) && ((cameraPoint.z + this.dz) >= this.minZ)) && ((cameraPoint.z + this.dz) <= this.maxZ))
                    {

                        {
                            float _190 = this.minX;
                            Vector3 _191 = this.transform.position;
                            _191.x = _190;
                            this.transform.position = _191;
                        }
                        this.transform.Translate(0, 0, this.dz, Space.World);
                    }
                    else
                    {
                        if ((((cameraPoint.x + this.dx) > this.maxX) && ((cameraPoint.z + this.dz) >= this.minZ)) && ((cameraPoint.z + this.dz) <= this.maxZ))
                        {

                            {
                                float _192 = this.maxX;
                                Vector3 _193 = this.transform.position;
                                _193.x = _192;
                                this.transform.position = _193;
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

}