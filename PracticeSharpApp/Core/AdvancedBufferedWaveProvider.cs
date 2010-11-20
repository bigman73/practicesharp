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
using System.Collections.Generic;
using System.Text;
using System.Collections;
using NAudio.Wave;

namespace BigMansStuff.PracticeSharp.Core
{
    /// <summary>
    /// Provides a buffered store of samples
    /// Read method will return queued samples or fill buffer with zeroes
    /// based on code from trentdevers (http://naudio.codeplex.com/Thread/View.aspx?ThreadId=54133)
    /// </summary>
    public class AdvancedBufferedWaveProvider : IWaveProvider
    {
        private Queue<AudioBuffer> m_queue;
        private WaveFormat m_waveFormat;

        /// <summary>
        /// Creates a new buffered WaveProvider
        /// </summary>
        /// <param name="waveFormat">WaveFormat</param>
        public AdvancedBufferedWaveProvider(WaveFormat waveFormat)
        {
            this.m_waveFormat = waveFormat;
            this.m_queue = new Queue<AudioBuffer>();
            this.MaxQueuedBuffers = 100;
        }

        /// <summary>
        /// Maximum number of queued buffers
        /// </summary>
        public int MaxQueuedBuffers { get; set; }

        /// <summary>
        /// Gets the WaveFormat
        /// </summary>
        public WaveFormat WaveFormat
        {
            get { return m_waveFormat; }
        }

        /// <summary>
        /// Adds samples. Takes a copy of buffer, so that buffer can be reused if necessary
        /// </summary>
        public void AddSamples(byte[] buffer, int offset, int count, TimeSpan currentTime, int averageBytesPerSec)
        {
            byte[] nbuffer = new byte[count];
            Buffer.BlockCopy(buffer, offset, nbuffer, 0, count);
            lock (this.m_queue)
            {
                if (this.m_queue.Count >= this.MaxQueuedBuffers)
                {
                    throw new InvalidOperationException("Too many queued buffers");
                }
                this.m_queue.Enqueue(new AudioBuffer(nbuffer, currentTime, averageBytesPerSec));
            }
        }

        public event EventHandler PlayPositionChanged;

        /// <summary>
        /// Reads from this WaveProvider
        /// Will always return count bytes, since we will zero-fill the buffer if not enough available
        /// </summary>
        public int Read(byte[] buffer, int offset, int count) 
        {
            int read = 0;
            while (read < count) 
            {
                int required = count - read;
                AudioBuffer audioBuffer = null;
                lock (this.m_queue)
                {
                    if (this.m_queue.Count > 0)
                    {
                        audioBuffer = this.m_queue.Peek();
                    }
                }

                if (audioBuffer == null) 
                {
                    // Return a zero filled buffer
                    for (int n = 0; n < required; n++)
                        buffer[offset + n] = 0;
                    read += required;
                } 
                else // There is an audio buffer - let's play it
                {
                    int nread = audioBuffer.Buffer.Length - audioBuffer.Position;

                    // Console.WriteLine("Now playing: " + audioBuffer.CurrentTime.ToString() /*+ audioBuffer.Position / au*/ );
                    PlayPositionChanged(this, new BufferedPlayEventArgs(audioBuffer.CurrentTime));

                    // If this buffer must be read in it's entirety
                    if (nread <= required) 
                    {
                        // Read entire buffer
                        Buffer.BlockCopy(audioBuffer.Buffer, audioBuffer.Position, buffer, offset + read, nread);
                        read += nread;

                        lock (this.m_queue)
                        {
                            this.m_queue.Dequeue();
                        }
                    }
                    else // the number of bytes that can be read is greater than that required
                    {
                        Buffer.BlockCopy(audioBuffer.Buffer, audioBuffer.Position, buffer, offset + read, required);
                        audioBuffer.Position += required;
                        read += required;
                    }
                }
            }
            return read;
        }

        /// <summary>
        /// Clears the queue of any remaining buffers
        /// </summary>
        public void ClearQueue()
        {
            lock (m_queue)
            {
                m_queue.Clear();
            }
        }

        /// <summary>
        /// Gets the current number of buffers in the queue
        /// </summary>
        public int GetQueueCount()
        {
            int queueCount = 0;
            lock (m_queue)
            {
                queueCount = m_queue.Count;
            }

            return queueCount;
        }

        /// <summary>
        /// Internal helper class for a stored buffer
        /// </summary>
        private class AudioBuffer
        {
            /// <summary>
            /// Constructs a new AudioBuffer
            /// </summary>
            public AudioBuffer(byte[] buffer, TimeSpan currentTime, int averageBytesPerSec )
            {
                this.Buffer = buffer;
                this.CurrentTime = currentTime;
                m_averageBytesPerSec = averageBytesPerSec;
            }

            /// <summary>
            /// Gets the Buffer
            /// </summary>
            public byte[] Buffer { get; private set; }

            /// <summary>
            /// Gets or sets the position within the buffer we have read up to so far
            /// </summary>
            public int Position { get; set; }

            /// <summary>
            /// CurrentTime of original file - used for calculating actual position within played buffer
            /// </summary>
            public TimeSpan CurrentTime { get; set; }

            public int AverageBytesPerSec { get { return m_averageBytesPerSec; }}

            private int m_averageBytesPerSec;
        }
    }

    public class BufferedPlayEventArgs : EventArgs
    {
        public BufferedPlayEventArgs(TimeSpan playTime)
        {
            this.PlayTime = playTime;
        }

        public TimeSpan PlayTime;
    }
}
