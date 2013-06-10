using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BigMansStuff.NAudio.FLAC
{
    /// <summary>
    /// C# (.NET) Wrapper for the libFlac library (Written in C++) 
    /// </summary>
    /// <remarks>
    /// Based a .NET/C# Interop wrapper by Stanimir Stoyanov - 
    ///     http://stoyanoff.info/blog/2010/07/26/decoding-flac-audio-files-in-c/
    ///     and
    ///     http://stoyanoff.info/blog/2010/01/08/encoding-uncompressed-audio-with-flac-in-c/
    /// using libFlac - http://flac.sourceforge.net
    /// For a full description of libFlac Decoder API: http://flac.sourceforge.net/api/group__flac__stream__decoder.html
    /// For a full description of libFlac Encoder API: http://flac.sourceforge.net/api/group__flac__stream__encoder.html
    /// </remarks>
    public class LibFLACSharp
    {
        #region Constants

        const string DLLName = "LibFlac";

        public enum StreamDecoderState
        {
            SearchForMetadata = 0,
            ReadMetadata,
            SearchForFrameSync,
            ReadFrame,
            EndOfStream,
            OggError,
            SeekError,
            Aborted,
            MemoryAllocationError,
            Uninitialized
        }		        

        #endregion

        #region Decoder API

        [DllImport(DLLName)]
        public static extern IntPtr FLAC__stream_decoder_new();

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_decoder_finish(IntPtr context);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_decoder_delete(IntPtr context);

        [DllImport(DLLName)]
        public static extern int FLAC__stream_decoder_init_file(IntPtr context, string filename, Decoder_WriteCallback write, Decoder_MetadataCallback metadata, Decoder_ErrorCallback error, IntPtr userData);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_decoder_process_single(IntPtr context);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_decoder_process_until_end_of_metadata(IntPtr context);
        
        [DllImport(DLLName)]
        public static extern bool FLAC__stream_decoder_process_until_end_of_stream(IntPtr context);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_decoder_seek_absolute(IntPtr context, long newSamplePosition);
        
        [DllImport(DLLName)]
        public static extern bool FLAC__stream_decoder_get_decode_position(IntPtr context, ref long position);

        [DllImport(DLLName)]
        public static extern long FLAC__stream_decoder_get_total_samples(IntPtr context);

        [DllImport(DLLName)]
        public static extern int FLAC__stream_decoder_get_channels(IntPtr context);

        [DllImport(DLLName)]
        public static extern int FLAC__stream_decoder_get_bits_per_sample(IntPtr context);

        [DllImport(DLLName)]
        public static extern int FLAC__stream_decoder_get_sample_rate(IntPtr context);

        [DllImport(DLLName)]
        public static extern StreamDecoderState FLAC__stream_decoder_get_state(IntPtr context);

        // Callbacks
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Decoder_WriteCallback(IntPtr context, IntPtr frame, IntPtr buffer, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Decoder_ErrorCallback(IntPtr context, DecodeError status, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Decoder_MetadataCallback(IntPtr context, IntPtr metadata, IntPtr userData);

        private const int FlacMaxChannels = 8;

        public struct FlacFrame
        {
            public FrameHeader Header;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = FlacMaxChannels)]
            public FlacSubFrame[] Subframes;
            public FrameFooter Footer;
        }

        public struct FrameHeader
        {
            public int BlockSize;
            public int SampleRate;
            public int Channels;
            public int ChannelAssignment;
            public int BitsPerSample;
            public FrameNumberType NumberType;
            public long FrameOrSampleNumber;
            public byte Crc;
        }

        public struct FlacSubFrame
        {
            public SubframeType Type;
            public IntPtr Data;
            public int WastedBits;
        }

        public struct FrameFooter
        {
            public ushort Crc;
        }

        public enum FrameNumberType
        {
            Frame,
            Sample
        }

        public enum SubframeType
        {
            Constant,
            Verbatim,
            Fixed,
            LPC
        }

        public enum DecodeError
        {
            LostSync,
            BadHeader,
            FrameCrcMismatch,
            UnparsableStream
        }

        public enum FLACMetaDataType
        {
            StreamInfo,
            Padding,
            Application,
            Seekable,
            VorbisComment,
            CueSheet,
            Picture,
            Undefined
        }

        public struct FLACMetaData
        {
            // The type of the metadata block; used determine which member of the data union to dereference. If type >= FLAC__METADATA_TYPE_UNDEFINED then data.unknown must be used.
            public FLACMetaDataType MetaDataType;
            // true if this metadata block is the last, else false
            public bool IsLast;
 	        // Length, in bytes, of the block data as it appears in the stream.
            public int Length;
            // Polymorphic block data; use the type value to determine which to use.
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            public byte[] Data;
        }

        [StructLayout(LayoutKind.Explicit,Pack = 1,Size = 40)]
        public struct FLACStreamInfo
        {
            // Note Offsets 0..3 are the byte array length (header) - we just ingore these bytes and start with Offset 4
            [FieldOffset(4)]
            public Int32 MinBlocksize;
            [FieldOffset(8)]
            public Int32 MaxBlocksize;
            [FieldOffset(12)]
            public Int32 min_framesize;
            [FieldOffset(16)]
            public Int32 max_framesize;
            [FieldOffset(20)]
            public Int32 SampleRate;
            [FieldOffset(24)]
            public Int32 Channels;
            [FieldOffset(28)]
            public Int32 BitsPerSample;
            [FieldOffset(32)]
            public Int32 TotalSamplesHi;
            [FieldOffset(36)]
            public Int32 TotalSamplesLo;
           // [FieldOffset(40)]
           // public byte[] md5sum;
        }
        #endregion

        #region Encoder API

        [DllImport(DLLName)]
        public static extern IntPtr FLAC__stream_encoder_new();

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_finish(IntPtr context);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_delete(IntPtr context);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_set_channels(IntPtr context, int value);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_set_bits_per_sample(IntPtr context, int value);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_set_sample_rate(IntPtr context, int value);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_set_compression_level(IntPtr context, int value);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_set_blocksize(IntPtr context, int value);

        [DllImport(DLLName)]
        public static extern int FLAC__stream_encoder_init_stream(IntPtr context, Encoder_WriteCallback write, Encoder_SeekCallback seek, Encoder_TellCallback tell, Encoder_MetadataCallback metadata, IntPtr userData);

        [DllImport(DLLName)]
        public static extern int FLAC__stream_encoder_init_file(IntPtr context, string filename, IntPtr progress, IntPtr userData);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_process_interleaved(IntPtr context, IntPtr buffer, int samples);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_process(IntPtr context, IntPtr buffer, int samples);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_set_verify(IntPtr context, bool value);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_set_streamable_subset(IntPtr context, bool value);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_set_do_mid_side_stereo(IntPtr context, bool value);

        [DllImport(DLLName)]
        public static extern bool FLAC__stream_encoder_set_loose_mid_side_stereo(IntPtr context, bool value);

        [DllImport(DLLName)]
        public static extern int FLAC__stream_encoder_get_state(IntPtr context);

        // Callbacks
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Encoder_WriteCallback(IntPtr context, IntPtr buffer, int bytes, uint samples, uint current_frame, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Encoder_SeekCallback(IntPtr context, long absoluteOffset, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Encoder_TellCallback(IntPtr context, out long absoluteOffset, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Encoder_MetadataCallback(IntPtr context, IntPtr metadata, IntPtr userData);
        #endregion
    }
}
