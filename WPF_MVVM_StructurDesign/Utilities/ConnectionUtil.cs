using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WPF_MVVM_StructurDesign.Utilities
{

	public class ConnectionUtil
	{
		string udl = "";

		public ConnectionUtil()
		{
			StringConnectionList = new List<string>();
			udl = "data.udl";
			
		}

		private async Task DataLinkDialog()
        {
			await Task.Run(()=>
				{
					new FileUtil().WriteTextFile(udl);
					Process p = Process.Start(udl);
					p.WaitForExit();
				}
			);
		}

		private async Task <ConnectionUtil> ReadStringConnection()
        {
			await DataLinkDialog();
			await Task.Run(()=>
				{
					if (StringConnectionList != null)
					{
						StringConnectionList = new FileUtil().ReadListOfString(udl);
						if (StringConnectionList.Count > 1)
						{
							string s = "";
							for (int i = 2; i < StringConnectionList.Count; i++)
							{
								s += StringConnectionList[i];
								StringConnectionList[i - 2] = StringConnectionList[i];
							}
							StringConnection = s;
							StringConnectionList = StringConnectionList[0].Split(';').ToList();
							string[] strArr = StringConnectionList.ToArray();
							StringConnectionSqlServer = strArr[1]+";"+ strArr[2]+";" + strArr[3] + ";" + strArr[4];
						}
					}
				}
				
			);

			return this;
		}

		public async Task<ConnectionUtil> CreateStringConnectionAsync()
		{
			return await ReadStringConnection();
		}

		private string stringConnection;
		public string StringConnection
		{
			get { return stringConnection; }
			set { stringConnection = value; }
		}

		private List<string> stringConnectionList;
		public List<string> StringConnectionList
		{
			get { return stringConnectionList; }
			set { stringConnectionList = value; }
		}

		private string stringConnectionSqlServer;
		public string StringConnectionSqlServer
		{
			get { return stringConnectionSqlServer; }
			set { stringConnectionSqlServer = value; }
		}
	}

}
