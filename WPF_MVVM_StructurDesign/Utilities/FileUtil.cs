using System;
using System.Linq;
using System.Collections.Generic;

using System.IO;
using System.Text;

namespace WPF_MVVM_StructurDesign.Utilities
{
    public class FileUtil 
    {   
    	public FileUtil()
      {
		//library lebih baru
    		BaseDirectory=AppContext.BaseDirectory;
    		
    		//no longer supported
    		BaseDirectory=AppDomain.CurrentDomain.BaseDirectory;
    	}
    	
    	private string baseDirectory;
    	public string BaseDirectory
    	{
    		get{return baseDirectory;}
    		set{baseDirectory=value;}
    	}
    	
        public string ReadTextFile(string file)
		{
			FileStream fileStream=new FileStream(file,FileMode.OpenOrCreate,FileAccess.Read);
			
			StreamReader reader=new StreamReader(fileStream,Encoding.UTF8);
			string str=null;
			StringBuilder strBuild=new StringBuilder();
			bool first=true;
			while((str=reader.ReadLine())!=null)
			{
				strBuild.Append(str+"\n");
			}
			reader.Close();
			fileStream.Close();
			return strBuild.ToString();
		}
		public void WriteTextFile(string file,string text="",bool append=false)
		{
			StringBuilder strBuild=new StringBuilder();
			if(append)strBuild.Append(ReadTextFile(file));
			FileStream fileStream=new FileStream(file,FileMode.Create,FileAccess.Write);
			if(!text.Equals(string.Empty))
			{
				StreamWriter writer=new StreamWriter(fileStream,Encoding.UTF8);
				strBuild.Append(text);
				writer.WriteLine(strBuild.ToString());
				writer.Close();
			}
			
			fileStream.Close();
		}
		
		public List<int> ReadListOfInt(string file)
		{
			var textInts = new List<int>();
			using (StreamReader reader = File.OpenText(file))
			{
    			string line=null;
    			while ((line = reader.ReadLine()) != null)
    			{
        			if (int.TryParse(line, out int textInt))
        			{
            			textInts.Add(textInt);
        			}
    			}
			}
			//return null if list isEmpty
			return textInts;
		}
		
		public List<string> ReadListOfString(string file)
		{
			var texts = new List<string>();
			using (StreamReader reader = File.OpenText(file))
			{
    			string line=null;
    			while ((line = reader.ReadLine()) != null)
    			{
            		texts.Add(line);
    			}
			}
			//return null if list isEmpty
			return texts;
		}
		
		
    }
}
