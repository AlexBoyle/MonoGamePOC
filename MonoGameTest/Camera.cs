namespace MonoGameTest
{
    public class Camera
    {
        public float maxZoomOut = .1f;
        Vector2 centerLocation = Vector2.Zero;
        public float zoom { get; set; }

        public void updateZoomBy(float zoomUpdate) { 
            this.zoom += zoomUpdate; 
            if(this.zoom < maxZoomOut)
            {
                this.zoom = maxZoomOut;
            }
        }

        public Camera()
        {
            zoom = 1f;
        }

        public void centerCamera(Vector2 pos) { centerLocation = pos; }

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
