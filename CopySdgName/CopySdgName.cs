using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LSEXT;
using LSSERVICEPROVIDERLib;
using Patholab_DAL_V1;
using System.Runtime.InteropServices;
using Patholab_Common;

namespace CopySdgName
{
    [ComVisible(true)]
    [ProgId("CopySdgName.CopySdgName")]
    public partial class CopySdgName : UserControl, IEntityExtension
    {
        INautilusServiceProvider sp;
        INautilusDBConnection ntlCon;
        DataLayer dal;

        public CopySdgName()
        {
            InitializeComponent();
        }

        public ExecuteExtension CanExecute(ref IExtensionParameters Parameters)
        {
            return ExecuteExtension.exEnabled;
        }

        public void Execute(ref LSExtensionParameters Parameters)
        {
            sp = Parameters["SERVICE_PROVIDER"] as INautilusServiceProvider;
            var records = Parameters["RECORDS"];
            ntlCon = Utils.GetNtlsCon(sp);
            Utils.CreateConstring(ntlCon);
            dal = new DataLayer();
            dal.Connect(ntlCon);

            long sdgId = Convert.ToInt64(records[0].Value);
            string sdgName = dal.FindBy<SDG>(sdg => sdg.SDG_ID == sdgId).FirstOrDefault().NAME;
            Clipboard.SetText(sdgName);            
        }
    }
}
