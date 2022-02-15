/*
 * By David Barrett, Microsoft Ltd. 2022. Use at your own risk.  No warranties are given.
 * 
 * DISCLAIMER:
 * THIS CODE IS SAMPLE CODE. THESE SAMPLES ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND.
 * MICROSOFT FURTHER DISCLAIMS ALL IMPLIED WARRANTIES INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OF MERCHANTABILITY OR OF FITNESS FOR
 * A PARTICULAR PURPOSE. THE ENTIRE RISK ARISING OUT OF THE USE OR PERFORMANCE OF THE SAMPLES REMAINS WITH YOU. IN NO EVENT SHALL
 * MICROSOFT OR ITS SUPPLIERS BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS,
 * BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR OTHER PECUNIARY LOSS) ARISING OUT OF THE USE OF OR INABILITY TO USE THE
 * SAMPLES, EVEN IF MICROSOFT HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. BECAUSE SOME STATES DO NOT ALLOW THE EXCLUSION OR LIMITATION
 * OF LIABILITY FOR CONSEQUENTIAL OR INCIDENTAL DAMAGES, THE ABOVE LIMITATION MAY NOT APPLY TO YOU.
 * */

using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace DragDropFromOutlookSample
{
    public partial class Form1 : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        public sealed class POINTL
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class SIZEL
        {
            public int cx;
            public int cy;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public sealed class FILEGROUPDESCRIPTORW
        {
            public uint cItems;
            public FILEDESCRIPTORW[] fgd;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public sealed class FILEDESCRIPTORW
        {
            public uint dwFlags;
            public Guid clsid;
            public SIZEL sizel;
            public POINTL pointl;
            public uint dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Activate the drop target
            groupBoxDropTarget.AllowDrop = true;
            groupBoxDropTarget.DragEnter += GroupBoxDropTarget_DragEnter;
            groupBoxDropTarget.DragDrop += GroupBoxDropTarget_DragDrop;
        }

        private void WriteItemToDisk(System.Runtime.InteropServices.ComTypes.IDataObject dataObject, int index)
        {
            // Retrieve the given item from the data object and write it to disk

            // Prepare FORMATETC for retrieving data
            FORMATETC formatetc = new FORMATETC();
            formatetc.cfFormat = (short)DataFormats.GetFormat("FileContents").Id;
            formatetc.dwAspect = DVASPECT.DVASPECT_CONTENT;
            formatetc.lindex = index;
            formatetc.ptd = new IntPtr(0);
            formatetc.tymed = TYMED.TYMED_ISTREAM | TYMED.TYMED_ISTORAGE | TYMED.TYMED_HGLOBAL;

            STGMEDIUM medium = new STGMEDIUM();
            dataObject.GetData(ref formatetc, out medium);

            // How we retrieve the data depends upon the returned data type
            // This is not implemented as it is currently beyond the scope of this sample
            // See https://docs.microsoft.com/en-us/windows/win32/com/data-transfer
        }

        private void GroupBoxDropTarget_DragDrop(object sender, DragEventArgs e)
        {
            // Check for Outlook data, which is presented as FileGroupDescriptorW
            if (e.Data.GetDataPresent("FileGroupDescriptorW"))
            {
                GCHandle? gch = null;
                try
                {
                    // Get FileGroupDescriptorW as a MemoryStream
                    MemoryStream fileGroupDescriptorStream = (MemoryStream)e.Data.GetData("FileGroupDescriptorW");
                    byte[] fileGroupDescriptorBytes = new byte[fileGroupDescriptorStream.Length];
                    fileGroupDescriptorStream.Read(fileGroupDescriptorBytes, 0, fileGroupDescriptorBytes.Length);
                    fileGroupDescriptorStream.Close();

                    // Marshal the byte array to FILEGROUPDESCRIPTORW struct
                    gch = GCHandle.Alloc(fileGroupDescriptorBytes, GCHandleType.Pinned);
                    IntPtr fileGroupDescriptorWPointer = ((GCHandle)gch).AddrOfPinnedObject();
                    object fileGroupDescriptorObject = Marshal.PtrToStructure(fileGroupDescriptorWPointer, typeof(FILEGROUPDESCRIPTORW));
                    FILEGROUPDESCRIPTORW fileGroupDescriptor = (FILEGROUPDESCRIPTORW)fileGroupDescriptorObject;

                    // Create an array to store file names
                    string[] fileNames = new string[fileGroupDescriptor.cItems];

                    // Get the pointer to the first file descriptor
                    IntPtr fileDescriptorPointer = (IntPtr)((int)fileGroupDescriptorWPointer + Marshal.SizeOf(fileGroupDescriptorWPointer));

                    // Loop through to retrieve the list of filenames
                    for (int fileDescriptorIndex = 0; fileDescriptorIndex < fileGroupDescriptor.cItems; fileDescriptorIndex++)
                    {
                        FILEDESCRIPTORW fileDescriptor = (FILEDESCRIPTORW)Marshal.PtrToStructure(fileDescriptorPointer, typeof(FILEDESCRIPTORW));
                        fileNames[fileDescriptorIndex] = fileDescriptor.cFileName;

                        fileDescriptorPointer = (IntPtr)((int)fileDescriptorPointer + Marshal.SizeOf(fileDescriptor));
                    }

                    // Display the filenames
                    textBoxItemInfo.Text = String.Join(Environment.NewLine, fileNames);

                    if (checkBoxSaveItems.Checked)
                    {
                        // Write each of the dropped files to disk
                        for (int i=0; i<fileNames.Length; i++)
                            WriteItemToDisk((System.Runtime.InteropServices.ComTypes.IDataObject)e.Data, i);
                    }
                    return;
                }
                finally
                {
                    // Free our GCHandle
                    gch?.Free();
                }
            }

            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                // Dump the text format of the dragged data
                textBoxItemInfo.Text = String.Join(Environment.NewLine, e.Data.GetData(DataFormats.Text, true));
                return;
            }

            // If we get here, we didn't know what to do with the data
            textBoxItemInfo.Text = "Unsupported drop format";
        }

        private void GroupBoxDropTarget_DragEnter(object sender, DragEventArgs e)
        {
            // Dump the available source formats
            textBoxItemInfo.Text = String.Join(Environment.NewLine, e.Data.GetFormats(false));

            // If we can do something with the data, we set the mouse pointer effect
            if (e.Data.GetDataPresent("FileGroupDescriptorW"))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
