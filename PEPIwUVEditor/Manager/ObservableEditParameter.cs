using IwUVEditor.Tool;
using SlimDX;

namespace IwUVEditor.Manager
{
    class ObservableEditParameter : IEditParameter
    {
        private Vector3 moveOffset;

        private Vector3 rotationCenter;
        private float rotationAngle;

        private Vector3 scaleCenter;
        private Vector3 scaleRatio;

        public event Vector3EventHandler RotationCenterChanged;
        public event Vector3EventHandler ScaleCenterChanged;

        public bool EnableEvent { get; set; } = true;

        public Vector3 MoveOffset
        {
            get => moveOffset;
            set
            {
                moveOffset = value;
            }
        }

        public Vector3 RotationCenter
        {
            get => rotationCenter;
            set
            {
                rotationCenter = value;
                if (EnableEvent)
                    RotationCenterChanged?.Invoke(value);
            }
        }
        public float RotationAngle
        {
            get => rotationAngle;
            set
            {
                rotationAngle = value;
            }
        }

        public Vector3 ScaleCenter
        {
            get => scaleCenter;
            set
            {
                scaleCenter = value;
                if (EnableEvent)
                    ScaleCenterChanged?.Invoke(value);
            }
        }
        public Vector3 ScaleRatio
        {
            get => scaleRatio;
            set
            {
                scaleRatio = value;
            }
        }
    }
}
