using SlimDX;

namespace IwUVEditor.Manager
{
    delegate void Vector3EventHandler(Vector3 value);

    class ObservableEditParameter : Tool.IEditParameter
    {
        private Vector3 moveCenter;
        private Vector3 moveOffset;

        private Vector3 rotationCenter;
        private float rotationAngle;

        private Vector3 scaleCenter;
        private Vector3 scaleRatio;

        event Vector3EventHandler MoveCenterChanged;
        event Vector3EventHandler RotationCenterChanged;
        event Vector3EventHandler ScaleCenterChanged;

        public bool EnableEvent { get; set; } = true;

        public Vector3 MoveCenter
        {
            get => moveCenter;
            set
            {
                moveCenter = value;
                if (EnableEvent)
                    MoveCenterChanged?.Invoke(value);
            }
        }
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
