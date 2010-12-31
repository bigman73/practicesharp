#region © Copyright 2010 Yuval Naveh. MIT.
/* Copyright (c) 2010, Yuval Naveh

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion

#region Original License
// Copyright 2006, Thomas Scott Stillwell
// All rights reserved.
//
//Redistribution and use in source and binary forms, with or without modification, are permitted 
//provided that the following conditions are met:
//
//Redistributions of source code must retain the above copyright notice, this list of conditions 
//and the following disclaimer. 
//
//Redistributions in binary form must reproduce the above copyright notice, this list of conditions 
//and the following disclaimer in the documentation and/or other materials provided with the distribution. 
//
//The name of Thomas Scott Stillwell may not be used to endorse or 
//promote products derived from this software without specific prior written permission. 
//
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
//IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND 
//FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS 
//BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES 
//(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
//STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF 
//THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

// ++ ported to .NET by Mark Heath ++

#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace BigMansStuff.NAudio.Tests
{
    /// <summary>
    /// Base class for all DSP effects
    /// Based on the Slider class written by Mark Heath, SkypeFX
    /// </summary>
    public abstract class DSPEffect
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DSPEffect()
        {
            m_factors = new List<DSPEffectFactor>();
            Enabled = true;
            SampleRate = 44100;
        }

        /// <summary>
        /// Builder method - Adds a DSP effect factor to the DSP effect
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="increment"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public DSPEffectFactor AddFactor(float defaultValue, float minimum, float maximum, float increment, string description)
        {
            DSPEffectFactor factor = new DSPEffectFactor(defaultValue, minimum, maximum, increment, description);
            m_factors.Add(factor);
            return factor;
        }

        public abstract string Name { get; }
        public IList<DSPEffectFactor> Factors { get { return m_factors; } }
        public float SampleRate { get; set; }
        public bool Enabled { get; set; }
        
        // helper base methods
        // these are primarily to enable derived classes to use a similar
        // syntax to JS effects
        protected float Factor1 { get { return m_factors[0].Value; } }
        protected float Factor2 { get { return m_factors[1].Value; } }
        protected float Factor3 { get { return m_factors[2].Value; } }
        protected float Factor4 { get { return m_factors[3].Value; } }
        protected float Factor5 { get { return m_factors[4].Value; } }
        protected float Factor6 { get { return m_factors[5].Value; } }
        protected float Factor7 { get { return m_factors[6].Value; } }
        protected float Factor8 { get { return m_factors[7].Value; } }
        protected float Min(float a, float b) { return Math.Min(a, b); }
        protected float Max(float a, float b) { return Math.Max(a, b); }
        protected float Abs(float a) { return Math.Abs(a); }
        protected float Exp(float a) { return (float)Math.Exp(a); }
        protected float Sqrt(float a) { return (float)Math.Sqrt(a); }
        protected float Sin(float a) { return (float)Math.Sin(a); }
        protected float Tan(float a) { return (float)Math.Tan(a); }
        protected float Cos(float a) { return (float)Math.Cos(a); }
        protected float Pow(float a, float b) { return (float)Math.Pow(a, b); }
        protected float Sign(float a) { return Math.Sign(a); }
        protected float Log(float a) { return (float)Math.Log(a); }

        protected const float Db2log = 0.11512925464970228420089957273422f; // ln(10) / 20 
        protected const float PI = 3.1415926535f;
        protected const float HalfPi = 1.57079632675f; // pi / 2;
        protected const float HalfPiScaled = 2.218812643387445f; // halfpi * 1.41254f;

        /// <summary>
        /// Performs a convolution operations between buffer 1 and buffer 2
        /// </summary>
        /// <param name="buffer1"></param>
        /// <param name="offset1"></param>
        /// <param name="buffer2"></param>
        /// <param name="offset2"></param>
        /// <param name="count"></param>
        protected void Convolve(float[] buffer1, int offset1, float[] buffer2, int offset2, int count)
        {
            for (int sampleIndex = 0; sampleIndex < count * 2; sampleIndex += 2)
            {
                float r = buffer1[offset1 + sampleIndex];
                float im = buffer1[offset1 + sampleIndex + 1];
                float cr = buffer2[offset2 + sampleIndex];
                float ci = buffer2[offset2 + sampleIndex + 1];
                buffer1[offset1 + sampleIndex] = r * cr - im * ci;
                buffer1[offset1 + sampleIndex + 1] = r * ci + im * cr;
            }
        }

        /// <summary>
        /// Should be called on effect load, sample rate changes, and start of playback
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// will be called when a factor value has been changed
        /// </summary>
        public abstract void OnFactorChanges();

        /// <summary>
        /// called before each block is processed
        /// </summary>
        /// <param name="samplesblock">number of samples in this block</param>
        public virtual void Block(int samplesblock)
        { 
        }

        /// <summary>
        /// Processed a single sample - should be called for each sample
        /// </summary>        
        public abstract void Sample(ref float spl0, ref float spl1);

        public override string ToString()
        {
            return Name;
        }

        protected List<DSPEffectFactor> m_factors;

        public const int DefaultSampleRate = 44100;        
    }
}
