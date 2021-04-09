using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using CentralEngine.CentralEngine.Components;

namespace CentralEngine.CentralEngine
{
    class Window : Form
    {
        public Window()
        {
            this.DoubleBuffered = true;
        }
    }

    public abstract class CentralEngine
    {
        Vector2 WindowSize;
        string WindowCaption;
        Window Window;
        Thread GameLoopThread;

        private static List<string> PressedKeys = new List<string>();

        private static List<Object> AllObjects = new List<Object>();

        public Color BackgroundColor = Color.Black;

        public CentralEngine(Vector2 WindowSize, string WindowCaption)
        {
            this.WindowSize = WindowSize;
            this.WindowCaption = WindowCaption;

            Window = new Window();
            Window.Size = new Size((int)this.WindowSize.X, (int)this.WindowSize.Y);
            Window.Text = this.WindowCaption;

            Window.Paint += Renderer;
            Window.KeyDown += KeyDown;
            Window.KeyUp += KeyUp;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);

        }

        private void KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
            PressedKeys.Remove(e.KeyCode.ToString());
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
            if (!GetKey(e.KeyCode.ToString()))
            {
                PressedKeys.Add(e.KeyCode.ToString());
            }
        }

        public static void RegisterObject(Object obj)
        {
            AllObjects.Add(obj);
        }
        
        public static void UnregisterObject(Object obj)
        {
            AllObjects.Remove(obj);
        }

        public static bool GetKey(string KeyCode)
        {
            return PressedKeys.Contains(KeyCode);
        }

        void GameLoop()
        {
            OnLoad();
            bool loaded = false;
            bool running = true;

            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (running)
            {
                if (!loaded)
                {
                    loaded = true;
                    continue;
                }
                
                try
                {
                    Time.DeltaTime = (float)timer.Elapsed.TotalSeconds - Time.TotalElapsedSeconds;
                    Time.TotalElapsedSeconds = (float)timer.Elapsed.TotalSeconds;

                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(1);
                    OnLateUpdate();
                }
                catch
                {
                    running = false;
                }
            }
        }

        void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Clear the screen
            g.Clear(BackgroundColor);

            // Draw primative shapes

            foreach (Object obj in AllObjects)
            {
                //ImageRenderer imageRenderer = (ImageRenderer)obj.GetComponent("image_renderer");
                //if (imageRenderer != null)
                //{
               //     g.DrawImage(imageRenderer.Image, obj.ObjectPosition.X, obj.ObjectPosition.Y, obj.ObjectScale.X, obj.ObjectScale.Y);
               // }

                foreach(Component component in obj.Components)
                {
                    switch (component.ComponentID)
                    {
                        case "image_renderer":
                            {
                                ImageRenderer imageRenderer = (ImageRenderer)component;
                                g.DrawImage(imageRenderer.Image, obj.ObjectPosition.X, obj.ObjectPosition.Y, obj.ObjectScale.X, obj.ObjectScale.Y);
                                break;
                            }
                        case "shape_renderer":
                            {
                                ShapeRenderer shapeRenderer = (ShapeRenderer)component;
                                switch (shapeRenderer.ShapeType)
                                {
                                    case Shape.Rectangle:
                                        {
                                            g.FillRectangle(new SolidBrush(shapeRenderer.Color), obj.ObjectPosition.X, obj.ObjectPosition.Y, obj.ObjectScale.X, obj.ObjectScale.Y);
                                            break;
                                        }
                                }
                                break;
                            }
                    }
                }
            }
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnLateUpdate();
        public abstract void OnDraw();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
    }
}
