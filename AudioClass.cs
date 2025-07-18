using MessangerClient.ServiceReference;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessangerClient
{
    public class AudioClass
    {
        WaveIn input;

        WaveOut output;
        BufferedWaveProvider bufferStream;

        public bool IsAudioSend = true;

        public string StreamAddress { get; set; }

        public AudioClass()
        {
            output = new WaveOut();
            bufferStream = new BufferedWaveProvider(new WaveFormat(44001, 1));
            output.Init(bufferStream);
            input = new WaveIn();
            input.WaveFormat = new WaveFormat(44001, 1);
            input.DataAvailable += Input_DataAvailable;
            App.ClientResponse.NewAudioCameOut += ClientResponse_NewAudioCameOut;
        }

        private void ClientResponse_NewAudioCameOut(byte[] data, bool isend)
        {
            AddBuff(data);
        }

        public void StartListening()
        {
            output.Play();
        }

        public void StopListening()
        {
            output.Stop();
            bufferStream.ClearBuffer();
            IsAudioSend = true;
        }

        public void AddBuff(byte[] data)
        {
            try
            {
                bufferStream.AddSamples(data, 0, data.Length);
            }
            catch(Exception ex)
            {
                bufferStream.ClearBuffer();
            }
        }

        public void StartRecord()
        {
            input.StartRecording();
        }

        public void StopRecord()
        {
            input.StopRecording();
            IsAudioSend = true;
        }

        private void Input_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (!App.IsConnecting && IsAudioSend) SendAudio(e.Buffer);
        }

        private async void SendAudio(byte[] buff)
        {
            IsAudioSend = false;
            await Task.Run(() => {
                try
                {
                    ResultCodes res = App.Client.SendAudio(StreamAddress, buff,App.CurrentUser.Username);
                    if (res != ResultCodes.Success) input.StopRecording();
                }
                catch (Exception ex)
                {
                    App.ConnectToServer(); 
                    input.StopRecording();
                }
                finally
                {
                    IsAudioSend = true;
                }
            });
        }

        public void Dispose()
        {
            output.Stop();
            input.StopRecording();
            output.Dispose();
            input.Dispose();
            output = null;
            input = null;
            bufferStream.ClearBuffer();
        }
    }
}
