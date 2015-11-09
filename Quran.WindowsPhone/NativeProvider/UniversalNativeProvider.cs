﻿using System;
using System.Collections.Generic;
using Windows.Storage;
using Quran.Core.Interfaces;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.ApplicationModel.Email;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Quran.WindowsPhone.NativeProvider
{
    public class UniversalNativeProvider : INativeProvider
    {
        private IDownloadManager downloadManager;
        public IDownloadManager DownloadManager
        {
            get
            {
                if (downloadManager == null)
                    downloadManager = new UniversalDownloadManager();
                return downloadManager;
            }
        }

        private ISettingsProvider settingsProvider;
        public ISettingsProvider SettingsProvider
        {
            get
            {
                if (settingsProvider == null)
                    settingsProvider = new UniversalSettingsProvider();
                return settingsProvider;
            }
        }

        private IAudioProvider audioProvider;
        public IAudioProvider AudioProvider
        {
            get
            {
                if (audioProvider == null)
                    audioProvider = new UniversalAudioProvider();
                return audioProvider;
            }
        }

        public ICollection<string> SplitLongText(string value, double fontSize, string fontWeight)
        {
            return TextBlockSplitter.Instance.Split(value, fontSize, PhoneUtils.FontWeightsConverter(fontWeight));
        }

        public void ExtractZip(string source, string baseFolder)
        {
            ZipHelper.Unzip(source, baseFolder);
        }

        public void CopyToClipboard(string text)
        {
            DataPackage dp = new DataPackage();
            dp.SetText(text);
            Clipboard.SetContent(dp);
        }

        public async Task ComposeEmail(string to, string subject)
        {
            EmailMessage email = new EmailMessage();
            email.To.Add(new EmailRecipient(to));
            email.Subject = subject;
            await EmailManager.ShowComposeNewEmailAsync(email);
        }

        public async Task LaunchWebBrowser(string url)
        {
            await Launcher.LaunchUriAsync(new Uri(url));
        }

        public async Task ToggleDeviceSleep(bool enable)
        {
            throw new NotImplementedException();
        }

        public async Task ShowInfoMessageBox(string text)
        {
            var dialog = new MessageDialog(text);
            await dialog.ShowAsync();
        }

        public async Task ShowInfoMessageBox(string text, string title)
        {
            var dialog = new MessageDialog(text, title);
            dialog.Commands.Add(new UICommand { Label = "OK", Id = 0 });
            await dialog.ShowAsync();
        }

        public async Task<bool> ShowQuestionMessageBox(string text)
        {
            var dialog = new MessageDialog(text);
            dialog.Commands.Add(new UICommand { Label = "OK", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            IUICommand result = await dialog.ShowAsync();
            return (int)result.Id == 0;
        }

        public async Task<bool> ShowQuestionMessageBox(string text, string title)
        {
            var dialog = new MessageDialog(text, title);
            dialog.Commands.Add(new UICommand { Label = "OK", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            IUICommand result = await dialog.ShowAsync();
            return (int)result.Id == 0;
        }

        public Task ShowErrorMessageBox(string text)
        {
            return ShowInfoMessageBox(text);
        }

        public void Log(string text)
        {
            // TODO: Implement
        }

        public string NativePath { get { return ApplicationData.Current.LocalFolder.Path; } }
    }
}
