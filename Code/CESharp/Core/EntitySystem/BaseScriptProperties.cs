// Copyright 2001-2016 Crytek GmbH / Crytek Group. All rights reserved.

using CryEngine.Common;

namespace CryEngine.EntitySystem
{
	/*public class BaseScriptProperties
	{
		public IScriptTable EntityTable { get; set; }
		public IScriptTable EditorTable { get; set; }
		public IScriptTable PropertiesTable { get; set; }
		public IScriptTable InstancePropertiesTable { get; set; }
		
		public BaseScriptProperties ()
		{
			EntityTable = null;
			EditorTable = null;
			PropertiesTable = null;
			InstancePropertiesTable = null;
		}

		public void CreateTables(bool instanceProperties = false)
		{
			IScriptSystem sys = Global.gEnv.pScriptSystem;
			if (sys == null)
				return;

			EntityTable = sys.CreateTable ();
			EntityTable.AddRef();

			EditorTable = sys.CreateTable();
			EditorTable.AddRef();

			PropertiesTable = sys.CreateTable();
			PropertiesTable.AddRef();

			if (instanceProperties)
			{
				InstancePropertiesTable = sys.CreateTable ();
				InstancePropertiesTable.AddRef ();
			}
		}
	}*/
}

