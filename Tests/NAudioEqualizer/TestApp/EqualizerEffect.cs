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
    /// Three Band Equalizer based on ThreeBasedEq class written by Mark Heath, SkypeFX
    /// </summary>
    public class EqualizerEffect : DSPEffect
    {
        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        public EqualizerEffect()
        {
            // Add the Equalizer DSP Effect Factors (Lo, Mid, Hi Drive & Gain, Lo-Mi frequencey & Mid-High frequency" );
            // Note: Drive in this context means mix ratio
            AddFactor(75, 0, 100, 0.1f, "lo drive%"); 
            AddFactor(0, -12, 12, 0.1f, "lo gain");
            AddFactor(40, 0, 100, 0.1f, "mid drive%");
            AddFactor(0, -12, 12, 0.1f, "mid gain");
            AddFactor(30, 0, 100, 0.1f, "hi drive%");
            AddFactor(0, -12, 12, 0.1f, "hi gain");
            AddFactor(200, 60, 680, 1, "low-mid freq");
            AddFactor(2400, 720, 12000, 10, "mid-high freq");
        }

        #endregion

        #region DSP Effect Factors

        public DSPEffectFactor LoDriveFactor { get { return m_factors[0]; } }
        public DSPEffectFactor LoGainFactor { get { return m_factors[1]; } }
        public DSPEffectFactor MedDriveFactor { get { return m_factors[2]; } }
        public DSPEffectFactor MedGainFactor { get { return m_factors[3]; } }
        public DSPEffectFactor HiDriveFactor { get { return m_factors[4]; } }
        public DSPEffectFactor HiGainFactor { get { return m_factors[5]; } }
        public DSPEffectFactor LoMedFrequencyFactor { get { return m_factors[6]; } }
        public DSPEffectFactor MedHiFrequencyFactor { get { return m_factors[7]; } }
        #endregion

        #region Overrides
        public override string Name
        {
            get { return "3 Band EQ"; }
        }

        public override void OnFactorChanges()
        {
            // Low Mix
            mixl = LoDriveFactor.Value / 100;
            // Med Mix
            mixm = MedDriveFactor.Value / 100;
            // High Mix
            mixh = HiDriveFactor.Value / 100;
            
            // Complement Mixes (useful for performance optimization as less calculations are made)
            mixl1 = 1 - mixl;
            mixm1 = 1 - mixm;
            mixh1 = 1 - mixh;

            // Low frequency
            al = Min(LoMedFrequencyFactor.Value, SampleRate) / SampleRate;
            // High frequency
            ah = Max(Min(MedHiFrequencyFactor.Value, SampleRate) / SampleRate, al);

            gainl = Exp(LoGainFactor.Value * Db2log);
            gainm = Exp(MedGainFactor.Value * Db2log);
            gainh = Exp(HiGainFactor.Value * Db2log);
            // Calculate combined Mix/Gain parameters
            mixlg = mixl * gainl;
            mixmg = mixm * gainm;
            mixhg = mixh * gainh;
            mixlg1 = mixl1 * gainl;
            mixmg1 = mixm1 * gainm;
            mixhg1 = mixh1 * gainh;
        }
        
        /// <summary>
        /// Applies the effect to a single sample
        /// </summary>
        /// <param name="spl0"></param>
        /// <param name="spl1"></param>
        public override void Sample(ref float spl0, ref float spl1)
        {
            float dry0 = spl0;
            float dry1 = spl1;

            float lf1h = lfh;
            lfh = dry0 + lfh - ah * lf1h;
            float high_l = dry0 - lfh * ah;

            float lf1l = lfl;
            lfl = dry0 + lfl - al * lf1l;
            float low_l = lfl * al;

            float mid_l = dry0 - low_l - high_l;

            float rf1h = rfh;
            rfh = dry1 + rfh - ah * rf1h;
            float high_r = dry1 - rfh * ah;

            float rf1l = rfl;
            rfl = dry1 + rfl - al * rf1l;
            float low_r = rfl * al;

            float mid_r = dry1 - low_r - high_r;

            float wet0_l = mixlg * Sin(low_l * HalfPiScaled);
            float wet0_m = mixmg * Sin(mid_l * HalfPiScaled);
            float wet0_h = mixhg * Sin(high_l * HalfPiScaled);
            float wet0 = (wet0_l + wet0_m + wet0_h);

            float dry0_l = low_l * mixlg1;
            float dry0_m = mid_l * mixmg1;
            float dry0_h = high_l * mixhg1;
            dry0 = (dry0_l + dry0_m + dry0_h);

            float wet1_l = mixlg * Sin(low_r * HalfPiScaled);
            float wet1_m = mixmg * Sin(mid_r * HalfPiScaled);
            float wet1_h = mixhg * Sin(high_r * HalfPiScaled);
            float wet1 = (wet1_l + wet1_m + wet1_h);

            float dry1_l = low_r * mixlg1;
            float dry1_m = mid_r * mixmg1;
            float dry1_h = high_r * mixhg1;
            dry1 = (dry1_l + dry1_m + dry1_h);

            spl0 = dry0 + wet0;
            spl1 = dry1 + wet1;
        }
        #endregion

        #region Private Members
        float lfl;
        float lfh;
        float rfh;
        float rfl;

        float mixl;
        float mixm;
        float mixh;
        float al;
        float ah;
        float mixl1;
        float mixm1;
        float mixh1;
        float gainl;
        float gainm;
        float gainh;
        float mixlg;
        float mixmg;
        float mixhg;
        float mixlg1;
        float mixmg1;
        float mixhg1;
        #endregion
    }
}
