#region © Copyright 2010 Yuval Naveh, Practice Sharp. LGPL.
/* Practice Sharp
 
    © Copyright 2010, Yuval Naveh.
     All rights reserved.
 
    This file is part of Practice Sharp.

    Practice Sharp is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Practice Sharp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser Public License for more details.

    You should have received a copy of the GNU Lesser Public License
    along with Practice Sharp.  If not, see <http://www.gnu.org/licenses/>.
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

namespace BigMansStuff.PracticeSharp.Core
{

    /// <summary>
    /// Three Band Equalizer based on ThreeBasedEq class written by Mark Heath, SkypeFX
    /// </summary>
    public class EqualizerEffect : DSPEffect
    {

        public EqualizerEffect()
        {
            AddFactor(80, 0, 100, 0.1f, "lo drive%");
            AddFactor(0, -12, 12, 0.1f, "lo gain");
            AddFactor(0, 0, 100, 0.1f, "mid drive%");
            AddFactor(0, -12, 12, 0.1f, "mid gain");
            AddFactor(0, 0, 100, 0.1f, "hi drive%");
            AddFactor(0, -12, 12, 0.1f, "hi gain");
            AddFactor(240, 60, 680, 1, "low-mid freq");
            AddFactor(2400, 720, 12000, 10, "mid-high freq");
        }

        public DSPEffectFactor LoDriveFactor { get { return m_factors[0]; } }
        public DSPEffectFactor LoGainFactor { get { return m_factors[1]; } }
        public DSPEffectFactor MedDriveFactor { get { return m_factors[2]; } }
        public DSPEffectFactor MedGainFactor { get { return m_factors[3]; } }
        public DSPEffectFactor HiDriveFactor { get { return m_factors[4]; } }
        public DSPEffectFactor HiGainFactor { get { return m_factors[5]; } }

        public override string Name
        {
            get { return "3 Band EQ"; }
        }

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

        public override void OnFactorChanges()
        {
            mixl = Factor1 / 100;
            mixm = Factor3 / 100;
            mixh = Factor5 / 100;
            al = Min(Factor7, SampleRate) / SampleRate;
            ah = Max(Min(Factor8, SampleRate) / SampleRate, al);
            mixl1 = 1 - mixl;
            mixm1 = 1 - mixm;
            mixh1 = 1 - mixh;
            gainl = Exp(Factor2 * Db2log);
            gainm = Exp(Factor4 * Db2log);
            gainh = Exp(Factor6 * Db2log);
            mixlg = mixl * gainl;
            mixmg = mixm * gainm;
            mixhg = mixh * gainh;
            mixlg1 = mixl1 * gainl;
            mixmg1 = mixm1 * gainm;
            mixhg1 = mixh1 * gainh;
        }
        
        float lfl;
        float lfh;
        float rfh;
        float rfl;

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
    }
}
