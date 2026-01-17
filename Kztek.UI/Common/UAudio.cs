using System.ComponentModel;
using System.Media;
using System.Security;
using System.Security.Permissions;

namespace Kztek.UI.Common
{
    /// <summary>
    /// Helper class for playing WAV sounds.
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static class WavPlayer
    {
        /// <summary>
        /// Indicates how to play the sound when calling audio methods.
        /// </summary>
        public enum AudioPlayMode
        {
            /// <summary>
            /// Play the sound and wait until it finishes before continuing the calling code.
            /// </summary>
            WaitToComplete,

            /// <summary>
            /// Play the sound in the background. The calling code continues to execute.
            /// </summary>
            Background,

            /// <summary>
            /// Play the background sound until the stop method is called. The calling code continues to execute.
            /// </summary>
            BackgroundLoop
        }

        private static SoundPlayer _soundPlayer;

        #region Methods

        /// <summary>
        /// Stop playing the sound.
        /// </summary>
        /// <param name="sound">SoundPlayer object.</param>
        private static void InternalStop(SoundPlayer sound)
        {
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
            try
            {
                sound.Stop();
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
        }

        /// <summary>
        /// Play a .wav sound file.
        /// </summary>
        /// <param name="location">A string containing the name of the sound file.</param>
        public static void Play(string location)
        {
            Play(location, AudioPlayMode.Background);
        }

        /// <summary>
        /// Play a .wav sound file.
        /// </summary>
        /// <param name="location">A string containing the name of the sound file.</param>
        /// <param name="playMode">AudioPlayMode enum indicating the play mode.</param>
        public static void Play(string location, AudioPlayMode playMode)
        {
            ValidateAudioPlayModeEnum(playMode, nameof(playMode));
            var validatedLocation = ValidateFilename(location);
            SoundPlayer player = new SoundPlayer(validatedLocation);
            Play(player, playMode);
        }

        /// <summary>
        /// Play the sound according to the play mode.
        /// </summary>
        /// <param name="sound">SoundPlayer object.</param>
        /// <param name="mode">AudioPlayMode enum indicating the play mode.</param>
        private static void Play(SoundPlayer sound, AudioPlayMode mode)
        {
            if (_soundPlayer != null)
            {
                InternalStop(_soundPlayer);
            }

            _soundPlayer = sound;
            switch (mode)
            {
                case AudioPlayMode.WaitToComplete:
                    _soundPlayer.PlaySync();
                    break;

                case AudioPlayMode.Background:
                    _soundPlayer.Play();
                    break;

                case AudioPlayMode.BackgroundLoop:
                    _soundPlayer.PlayLooping();
                    break;
            }
        }

        /// <summary>
        /// Play a system sound.
        /// </summary>
        /// <param name="systemSound">SystemSound object representing the system sound to play.</param>
        public static void PlaySystemSound(SystemSound systemSound)
        {
            if (systemSound == null)
            {
                throw new ArgumentNullException(nameof(systemSound));
            }

            systemSound.Play();
        }

        /// <summary>
        /// Stop playing the sound in the background.
        /// </summary>
        public static void Stop()
        {
            SoundPlayer player = new SoundPlayer();
            InternalStop(player);
        }

        /// <summary>
        /// Validate the AudioPlayMode enum value.
        /// </summary>
        /// <param name="value">AudioPlayMode enum value.</param>
        /// <param name="paramName">Parameter name.</param>
        private static void ValidateAudioPlayModeEnum(AudioPlayMode value, string paramName)
        {
            if (!Enum.IsDefined(typeof(AudioPlayMode), value))
            {
                throw new InvalidEnumArgumentException(paramName, (int)value, typeof(AudioPlayMode));
            }
        }

        /// <summary>
        /// Validate the file name.
        /// </summary>
        /// <param name="location">File name string.</param>
        /// <returns>Validated file name.</returns>
        private static string ValidateFilename(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            return location;
        }

        #endregion Methods
    }

    /// <summary>
    /// Helper class for MP3 file playback operations.
    /// </summary>
    public static class Mp3Player
    {
        /// <summary>
        /// Play an MP3 file.
        /// </summary>
        /// <param name="mp3FileName">File name.</param>
        /// <param name="repeat">Whether to play repeatedly.</param>
        public static void Play(string mp3FileName, bool repeat)
        {
            Sunny.UI.Win32.WinMM.mciSendString($"open \"{mp3FileName}\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            Sunny.UI.Win32.WinMM.mciSendString($"play MediaFile{(repeat ? " repeat" : string.Empty)}", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// Pause playback.
        /// </summary>
        public static void Pause()
        {
            Sunny.UI.Win32.WinMM.mciSendString("stop MediaFile", null, 0, IntPtr.Zero);
        }

        /// <summary>
        /// Stop playback.
        /// </summary>
        public static void Stop()
        {
            Sunny.UI.Win32.WinMM.mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }
    }
}
