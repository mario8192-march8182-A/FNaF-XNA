using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FNaF_XNA.Systems
{
    /// <summary>
    /// Manages camera system for viewing different areas of the facility.
    /// </summary>
    public class CameraSystem
    {
        private GraphicsDeviceManager graphics;
        private Vector2 cameraPosition;
        private float zoomLevel;
        private Matrix viewMatrix;

        public CameraSystem(GraphicsDeviceManager graphicsDeviceManager)
        {
            graphics = graphicsDeviceManager;
            cameraPosition = Vector2.Zero;
            zoomLevel = 1f;
        }

        public void Initialize()
        {
            // Initialize camera to center of screen
            cameraPosition = new Vector2(
                graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight / 2
            );
        }

        public void Update(GameTime gameTime)
        {
            // Update camera matrices
            UpdateViewMatrix();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Camera drawing would be handled by rendering system
        }

        private void UpdateViewMatrix()
        {
            viewMatrix = Matrix.CreateTranslation(
                new Vector3(-cameraPosition, 0)) *
                Matrix.CreateScale(new Vector3(zoomLevel, zoomLevel, 1f)) *
                Matrix.CreateTranslation(new Vector3(
                    graphics.PreferredBackBufferWidth / 2,
                    graphics.PreferredBackBufferHeight / 2, 0));
        }

        public void Pan(Vector2 direction, float speed)
        {
            cameraPosition += direction * speed;
        }

        public void Zoom(float amount)
        {
            zoomLevel = MathHelper.Clamp(zoomLevel + amount, 0.5f, 2f);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(viewMatrix));
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, viewMatrix);
        }

        public Vector2 Position => cameraPosition;
        public float Zoom => zoomLevel;
        public Matrix ViewMatrix => viewMatrix;
    }
}
