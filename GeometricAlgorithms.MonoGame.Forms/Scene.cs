﻿using GeometricAlgorithms.MonoGame.Forms.Cameras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MonoGame.Forms
{
    public class Scene
    {
        public List<Drawables.IDrawable> Drawables { get; set; }
        public ICamera Camera { get; set; }
    }
}
