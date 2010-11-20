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

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using csvorbis;
using NAudio.Wave;

namespace BigMansStuff.NAudio.Ogg
{
    /// <summary>
    /// NAudio reader for Ogg Vorbis files
    /// </summary>
    /// <remarks>
    /// Written By Yuval Naveh - Code is using Vorbis# (csvorbis) and takes some ideas from DragonOgg
    /// </remarks>
    public class OggVorbisFileReader : WaveStream
    {
        #region Constructors

        /// <summary>Constructor - Supports opening an Ogg Vorbis file</summary>
        public OggVorbisFileReader(string oggFileName) 
        {
            m_vorbisFile = new VorbisFile(oggFileName);
            Info[] info = m_vorbisFile.getInfo();

            // TODO: 8 bit is hard coded!! need to figure out how to calculate it, Ogg tags do not seem to contain it
            int bitsPerSample = 8;
            m_waveFormat = new WaveFormat(info[0].rate, bitsPerSample, info[0].channels);
        }

        #endregion

        #region WaveStream Overrides - Implement logic which is specific to OggVorbis

        /// <summary>
        /// This is the length in bytes of data available to be read out from the Read method
        /// (i.e. the decompressed Ogg length)
        /// n.b. this may return 0 for files whose length is unknown
        /// </summary>
        public override long Length
        {
            get
            {
                const int ALL_SAMPLES = -1;
                int bytesPerSample = (m_waveFormat.BitsPerSample / 8);
                long length = m_vorbisFile.pcm_total(ALL_SAMPLES) * 
                             m_vorbisFile.getInfo(0).channels *
                             bytesPerSample;
                return length;
            }
        }

        /// <summary>
        /// <see cref="WaveStream.WaveFormat"/>
        /// </summary>
        public override WaveFormat WaveFormat
        {
            get { return m_waveFormat; }
        }

        /// <summary>
        /// <see cref="Stream.Position"/>
        /// </summary>
        public override long Position
        {
            get
            {
                return m_vorbisFile.pcm_tell();
            }
            set
            {
                lock (m_repositionLock)
                {
                    // Note: For some unknown reason setting the value to Zero (0) causes the vorbis file not to play correctly
                    m_vorbisFile.pcm_seek(value + 1);
                }
            }
        }

        /// <summary>
        /// Reads decompressed PCM data from our MP3 file.
        /// </summary>
        public override int Read(byte[] sampleBuffer, int offset, int numBytes)
        {
            int bytesRead = 0;
            lock (m_repositionLock)
            {
                // Read PCM bytes from the Ogg Vorbis File into the sample buffer
                bytesRead = m_vorbisFile.read(sampleBuffer, numBytes, _BIGENDIANREADMODE, _WORDREADMODE, _SGNEDREADMODE, null);

            }

            return bytesRead;
        }

        #endregion 

        #region Dispose

        /// <summary>
        /// Disposes this WaveStream
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_vorbisFile != null)
                {
                    m_vorbisFile.Dispose();
                    m_vorbisFile = null;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Private Members

        private WaveFormat m_waveFormat;
        private object m_repositionLock = new object();
        private VorbisFile m_vorbisFile;

        #endregion

        #region Constants

        private const int _BIGENDIANREADMODE = 0;		// Big Endian config for read operation: 0=LSB;1=MSB
        private const int _WORDREADMODE = 1;			// Word config for read operation: 1=Byte;2=16-bit Short
        private const int _SGNEDREADMODE = 0;			// Signed/Unsigned indicator for read operation: 0=Unsigned;1=Signed

        #endregion
    }
}
