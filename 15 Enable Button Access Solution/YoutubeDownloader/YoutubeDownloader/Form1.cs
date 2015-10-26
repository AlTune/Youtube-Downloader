﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExtractor;

namespace YoutubeDownloader
{
    public partial class frmYTDownloader : Form
    {
        public frmYTDownloader()
        {
            InitializeComponent();
            cboFileType.SelectedIndex = 0;
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folderBrowserDialog1.SelectedPath = folder;
            txtPath.Text = folder;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                txtPath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Tuple<bool, string> isGoodLink = ValidateLink();
            if (isGoodLink.Item1 == true)
            {
                RestrictAccessibility();
                //Pass the validated link into the download method so it can be assigned to a property in the YoutubeVideoModel or the YoutubeAudioModel
                //Download(isGoodLink.Item2);
                System.Threading.Thread.Sleep(2000);
                EnableAccessibility();
                MessageBox.Show("Is it a good link?" + isGoodLink.Item1 + "Link is: " + isGoodLink.Item2);
                
            }
        }
       
        ////Tuple returns true if the link is good, the string returned is the valid link to the video
        private Tuple<bool, string> ValidateLink()
        {
            string normalUrl;
            if (!Directory.Exists(txtPath.Text))
            {
                MessageBox.Show("Please enter a valid folder");
                return Tuple.Create(false, "");
            }

            else if (!(DownloadUrlResolver.TryNormalizeYoutubeUrl(txtLink.Text, out normalUrl)))
            {
                MessageBox.Show("Please enter a valid Youtube link");
                return Tuple.Create(false, "");
            }
            else
                return Tuple.Create(true, normalUrl);
        }

        //Disables buttons and textboxes so they can't be clicked during a download
        private void RestrictAccessibility()
        {
            btnDownload.Enabled = false;
            cboFileType.Enabled = false;
            btnFolder.Enabled = false;
            txtPath.Enabled = false;
            txtLink.Enabled = false;
        }

        //Enables buttons and textboxes after a download is complete
        private void EnableAccessibility()
        {
            lblUpdate.Text = "";
            txtLink.Text = "";
            btnDownload.Enabled = true;
            cboFileType.Enabled = true;
            btnFolder.Enabled = true;
            txtPath.Enabled = true;
            txtLink.Enabled = true;
            pgDownload.Value = 0;
        }
    }
}