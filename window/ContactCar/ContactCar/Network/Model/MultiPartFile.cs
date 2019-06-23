using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCar.Network.Model
{
    public class MultiPartFile
    {
        public byte[] File { get; set; }
        public string FileName { get; set; }
        public MultiPartFile(byte[] file) : this(file, null) { }
        public MultiPartFile(byte[] file, string filename)
        {
            this.File = file;
            this.FileName = filename;
        }
    }
}
