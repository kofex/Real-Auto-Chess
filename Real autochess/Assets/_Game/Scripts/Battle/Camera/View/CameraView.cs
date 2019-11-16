using Scripts.Battle.Camera.Model;
using Scripts.Core.View;

namespace Scripts.Battle.Camera.View
{
    public class CameraView : ViewBase<CameraModel>
    {
        private UnityEngine.Camera _thisCamera;
        public UnityEngine.Camera ThisCamera => _thisCamera != null ? _thisCamera : (_thisCamera = GetComponent<UnityEngine.Camera>());

    }
}