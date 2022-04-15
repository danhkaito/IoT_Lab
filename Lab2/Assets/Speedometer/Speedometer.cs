/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Speedometer : MonoBehaviour {

    private const float MAX_SPEED_ANGLE = -25;
    private const float ZERO_SPEED_ANGLE = 205;

    private Transform needleTranform;
    private Transform speedLabelTemplateTransform;

    private float speedMax;
    public float speed;

    private void Awake() {
        needleTranform = transform.Find("needle");
        speedLabelTemplateTransform = transform.Find("speedLabelTemplate");
        speedLabelTemplateTransform.gameObject.SetActive(false);

        speed = 0f;
        speedMax = 80f;

        CreateSpeedLabels();
    }

    private void Update() {
        // HandlePlayerInput();

        //speed += 30f * Time.deltaTime;
        //if (speed > speedMax) speed = speedMax;

        needleTranform.DORotate(new Vector3(0, 0, GetSpeedRotation()), 0.5f);
    }

    // private void HandlePlayerInput() {
    //     if (Input.GetKey(KeyCode.UpArrow)) {
    //         float acceleration = 80f;
    //         speed += acceleration * Time.deltaTime;
    //     } else {
    //         float deceleration = 20f;
    //         speed -= deceleration * Time.deltaTime;
    //     }

    //     if (Input.GetKey(KeyCode.DownArrow)) {
    //         float brakeSpeed = 100f;
    //         speed -= brakeSpeed * Time.deltaTime;
    //     }

    //     speed = Mathf.Clamp(speed, 0f, speedMax);
    // }

    private void CreateSpeedLabels() {
        int labelAmount = 8;
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

        for (int i = 0; i <= labelAmount; i++) {
            Transform speedLabelTransform = Instantiate(speedLabelTemplateTransform, transform);
            float labelSpeedNormalized = (float)i / labelAmount;
            float speedLabelAngle = ZERO_SPEED_ANGLE - labelSpeedNormalized * totalAngleSize;
            speedLabelTransform.eulerAngles = new Vector3(0, 0, speedLabelAngle);
            speedLabelTransform.Find("speedText").GetComponent<Text>().text = Mathf.RoundToInt(labelSpeedNormalized * speedMax).ToString();
            speedLabelTransform.Find("speedText").eulerAngles = Vector3.zero;
            speedLabelTransform.gameObject.SetActive(true);
        }

        needleTranform.SetAsLastSibling();
    }

    private float GetSpeedRotation() {
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

        float speedNormalized = speed / speedMax;

        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
    }
}
