using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{

    public struct GameUtils // ilerde ise yarar illaki
    {

    }
    
    public class ParallelToGround
    {
        private GameObject gameObject;

        private float followSpeed,groundClearance;


        public ParallelToGround(string prefabName,float followSpeed,float groundClearance)
        {
            gameObject = GameObject.Instantiate(Resources.Load<GameObject>(prefabName), Vector3.zero, Quaternion.identity);
            
            this.followSpeed = followSpeed;
            this.groundClearance = groundClearance;
        }

        public void UpdateSlide()
        {
                Vector3 targetPos = GamePhysics.RayFromScreen.Hit.point;
                targetPos.y += groundClearance;

                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, followSpeed);

                gameObject.transform.forward = -GamePhysics.RayFromScreen.Hit.normal;

                Quaternion rot = gameObject.transform.rotation;
                Vector3 angle = rot.eulerAngles;

                angle.z = angle.y;

                rot.eulerAngles = angle;

                gameObject.transform.rotation = rot;
        }

        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }
        }

    }


}