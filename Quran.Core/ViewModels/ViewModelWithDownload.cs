﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quran.Core.ViewModels
{
    public class ViewModelWithDownload : BaseViewModel
    {
        private static readonly DownloadableViewModelBase activeDownload = new DownloadableViewModelBase();

        public DownloadableViewModelBase ActiveDownload
        {
            get { return activeDownload; }
        }

        public override Task Initialize()
        {
            return ActiveDownload.Initialize();
        }

        public override Task Refresh()
        {
            return ActiveDownload.Refresh();
        }
    }
}
