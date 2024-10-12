using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace AudioRecordingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string selectedFilePath;
        private const string bucketName = "audio-recording-5647";
        private static readonly string? accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
        private static readonly string? secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
        private static readonly string region = "ap-southeast-2"; // e.g., "us-east-1"

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files|*.mp3;*.wav;*.flac";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                ResultTextBox.Text = selectedFilePath;
            }
        }

        private async void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please select an audio file first.");
                return;
            }

            try
            {
                // Create an S3 client
                var s3Client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.GetBySystemName(region));

                // Create a TransferUtility instance to upload files
                var fileTransferUtility = new TransferUtility(s3Client);

                // Get the file name to use as the key in S3
                string fileName = System.IO.Path.GetFileName(selectedFilePath);

                // Upload the file
                await fileTransferUtility.UploadAsync(selectedFilePath, bucketName);

                // Generate the file's URL in S3
                string url = $"https://{bucketName}.s3.{region}.amazonaws.com/{fileName}";

                // Display the URL in the TextBox
                URLTextBox.Text = url;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error uploading file: " + ex.Message);
            }
        }
    }
}