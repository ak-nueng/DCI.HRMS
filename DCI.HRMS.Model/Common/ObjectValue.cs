using System.Collections;
using System;

namespace DCI.HRMS.Model.Common
{
	/// <summary>
	/// Name & Value.
	/// </summary>
    [Serializable]
	public class ObjectValue
	{
		private object _name;
		private object _value;
		
		public ObjectValue()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public ObjectValue(object name , object value)
		{
			this._name = name;
			this._value = value;
		}

		public object Name
		{
			get{ return this._name; }
			set{ this._name = value; }
		}
		public object Value
		{
			get{ return this._value; }
			set{ this._value = value; }
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}"
				, this.Value.ToString()
				, this.Name.ToString());
		}	
	}
	public class ObjectValueCollection : DictionaryBase
	{
		public object this[string key]
		{
			get{ return (ObjectValue)this.Dictionary[key];}
			set{ this.Dictionary[key] = value; }
		}
		public void Add(string key , object obj)
		{
			this.Dictionary.Add(key,obj);
		}
		public bool Contains(string key)
		{
			return this.Dictionary.Contains(key);
		}
		public ICollection Keys
		{
			get{ return this.Dictionary.Keys; }
		}
	}
}
