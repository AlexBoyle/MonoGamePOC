using MonoGameTest.Core;

namespace MonoGameTest.Utils
{
    public class Camera
    {
        public float maxZoomOut = .5f;
        Vector2 centerLocation = Vector2.Zero;
        public float zoom { get; set; }

        public void updateZoomBy(float zoomUpdate)
        {
            zoom += zoomUpdate;
            if (zoom < maxZoomOut)
            {
                zoom = maxZoomOut;
            }
        }

        public Camera()
        {
            zoom = 3f;
        }

        //turn off clamp when we fix the issue
        public void centerCamera(Vector2 pos) { centerLocation = new(pos.X, pos.Y); }

        public Matrix getCameraMatrix()
        {
            Vector2 screenSize = Globals.getwindowScreenSize();
            Matrix cameraPosition = Matrix.Identity;
            cameraPosition =
                Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(-centerLocation.X * zoom, -centerLocation.Y * zoom, 0)) *
                Matrix.CreateTranslation(new Vector3(screenSize.X * .5f, screenSize.Y * .5f, 0));
            return cameraPosition;
        }


    }
}
