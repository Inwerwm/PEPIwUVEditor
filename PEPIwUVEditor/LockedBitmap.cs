using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace IwUVEditor
{
    class LockedBitmap : IDisposable
    {
        private bool disposedValue;
        private int Length => Math.Abs(BitmapData.Stride) * Bitmap.Height;

        public Bitmap Bitmap { get; }
        public BitmapData BitmapData { get; }

        public byte[] Pixels
        {
            get
            {
                byte[] pixels = new byte[Length];
                System.Runtime.InteropServices.Marshal.Copy(BitmapData.Scan0, pixels, 0, Length);
                return pixels;
            }
            set
            {
                System.Runtime.InteropServices.Marshal.Copy(value, 0, BitmapData.Scan0, Length);
            }
        }

        public LockedBitmap(Bitmap bitmap)
        {
            Bitmap = bitmap;

            BitmapData = Bitmap.LockBits(
                new Rectangle(0, 0, Bitmap.Width, Bitmap.Height),
                ImageLockMode.ReadWrite,
                Bitmap.PixelFormat
            );
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Bitmap.UnlockBits(BitmapData);
                if (disposing)
                {
                    Bitmap?.Dispose();
                }

                disposedValue = true;
            }
        }

        // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        ~LockedBitmap()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
