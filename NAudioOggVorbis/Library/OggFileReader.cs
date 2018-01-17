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
        /// Reads decompressed PCM data from our Ogg Vorbis file.
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
