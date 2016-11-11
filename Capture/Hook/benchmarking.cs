using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Capture.Hook.DX11;
using Capture.Hook;
using Capture;

namespace Capture.Hook
{
    public static class benchmarking
    {
        public static bool IsBenchmarking;
        public static System.Drawing.Color benchColour = System.Drawing.Color.Yellow;
        private static DX11.DXOverlayEngine overlayEngine;
        public static SharpDX.Direct3D11.Texture2D bench2D;
        public static SharpDX.Direct3D11.Device benchDevice;
        public static SharpDX.DXGI.SwapChain benchChain;

        internal static DXOverlayEngine OverlayEngine
        {
            get
            {
                return overlayEngine;
            }

            set
            {
                overlayEngine = value;
            }
        }

        public static void IfTrue()
        {
            if (IsBenchmarking)
            {
                benchColour = System.Drawing.Color.Red;

                Overlay();
            }
            else
            {
                benchColour = System.Drawing.Color.Yellow;

                Overlay();
            }

            
        }
        private static void Overlay()
        {
            if (OverlayEngine != null)
                OverlayEngine.Dispose();

            OverlayEngine = new DX11.DXOverlayEngine();
            OverlayEngine.Overlays.Add(new Capture.Hook.Common.Overlay
            {
                Elements =
                            {
                                new Capture.Hook.Common.FramesPerSecond(new System.Drawing.Font("Arial", 20)) { Location = new System.Drawing.Point(5,5), Color = benchmarking.benchColour, AntiAliased = true },
                                new Capture.Hook.Common.TextElement(new System.Drawing.Font("Times New Roman", 22)) { Text = "Testing", Location = new System.Drawing.Point(5, 200), Color = System.Drawing.Color.Yellow, AntiAliased = false},
                            }
            });
            OverlayEngine.Initialise(benchChain);

            foreach (var overlay in OverlayEngine.Overlays)
                overlay.Frame();
            OverlayEngine.Draw();

        }
    }
}
