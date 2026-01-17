using System.Collections;
using System.Collections.Generic;

namespace Kztek.Cameras
{
    public class CameraCollection : CollectionBase
    {
        // constructor
        public CameraCollection()
        {

        }

        public CameraCollection(List<Camera> cameras)
        {
            this.InnerList.Clear();
            foreach (var item in cameras)
            {
                this.InnerList.Add(item);
            }
        }

        // Get came at the specified index
        public Camera this[int index]
        {
            get
            {
                return ((Camera)InnerList[index]);
            }
        }

        // Add new camera to the collection
        public void Add(Camera camera)
        {
            InnerList.Add(camera);
        }

        // Remove camera from the collection
        public void Remove(Camera camera)
        {
            InnerList.Remove(camera);
        }

        // Get camera with specified ID
        public Camera GetCamera_ByID(string cameraID)
        {
            // find the camera
            foreach (Camera camera in InnerList)
            {
                if (camera.ID == cameraID)
                    return camera;
            }
            return null;
        }

        // Get camera with specified Name
        public Camera GetCamera_ByName(string cameraName)
        {
            // find the camera
            foreach (Camera camera in InnerList)
            {
                if (camera.Name == cameraName)
                    return camera;
            }
            return null;
        }
    }
}
